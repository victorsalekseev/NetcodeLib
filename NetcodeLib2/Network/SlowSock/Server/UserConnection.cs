//Asynchronous server socket class
//(C) SLoW

using System;
using System.Net.Sockets;
//using System.Text;
using System.IO;

namespace Netcode.Network.SlowSock.Server
{
    // The UserConnection class encapsulates the functionality of a TcpClient connection
    // with streaming for a single user.

    public class UserConnection
    {
        public delegate void OnSocketEvent(EventType type, UserConnection sender, SocketState state, object param);
        public event OnSocketEvent SocketEvent;
        public Socket Socket { get { return client.Client; } }

        public bool FActive = true;

        public UserConnection uc = null;

        public struct SocketState
        {
            public SocketStatus Status;
            public bool Receiving, Sending;
        }

        protected SocketState SockState;
        public enum SocketStatus { ssNotConnected, ssConnected }
        public enum EventType { etConnected, etDisconnected, etDataReceived, etDataSended, etSocketError }

        const int READ_BUFFER_SIZE = 5000000;
        // Overload the new operator to set up a read thread.
        public UserConnection(TcpClient client)
        {
            SockState.Status = SocketStatus.ssConnected;
            this.client = client;

            Name = client.Client.RemoteEndPoint.ToString();

            // This starts the asynchronous read thread.  The data will be saved into
            // readBuffer.
            try
            {
                this.client.GetStream().BeginRead(readBuffer, 0, 4, new AsyncCallback(StreamReceiver), null);
            }
            catch (SocketException ex)
            {
                //WSAECONNRESET, the other side closed impolitely
                if (ex.ErrorCode == 10054)
                {
                    Close();
                    SocketEvent.Invoke(EventType.etDisconnected, this, SockState, null);
                    return;
                }
                SocketEvent.Invoke(EventType.etSocketError, this, SockState, ex);
                ContinueReading();
            }
            catch (Exception ex)
            {
                SocketEvent.Invoke(EventType.etSocketError, this, SockState, ex);
            }
        }

        private TcpClient client;
        private byte[] readBuffer = new byte[READ_BUFFER_SIZE];
        private int length = 0;
        private string strName;

        public class Packet
        {
            public MemoryStream data = new MemoryStream();
            public int ToRead;

            public Packet(int Length)
            {
                ToRead = Length;
            }
        }

        // The Name property uniquely identifies the user connection.
        public string Name
        {
            get
            {
                return strName;
            }
            set
            {
                strName = value;
            }
        }

        // This subroutine uses a StreamWriter to send a message to the user.
        public void SendData(byte[] Data)
        {
            if (!FActive) return;
            if (!client.Connected) return;
            //lock ensure that no other threads try to use the stream at the same time.
            lock (client.GetStream())
            {
                try
                {
                    MemoryStream ms = new MemoryStream();
                    BinaryWriter bw = new BinaryWriter(ms);
                    bw.Write(Data.Length);
                    bw.Write(Data);

                    ms.Position = 0;
                    byte[] d = new byte[ms.Length];
                    ms.Read(d, 0, d.Length);

                    client.GetStream().BeginWrite(d, 0, d.Length, new AsyncCallback(SendCallback), null);
                }
                catch (SocketException ex)
                {
                    //WSAECONNRESET, the other side closed impolitely
                    if (ex.ErrorCode == 10054)
                    {
                        Close();
                        SocketEvent.Invoke(EventType.etDisconnected, this, SockState, null);
                        return;
                    }
                    SocketEvent.Invoke(EventType.etSocketError, this, SockState, ex);
                    ContinueReading();
                }
                catch (Exception ex)
                {
                    SocketEvent.Invoke(EventType.etSocketError, this, SockState, ex);
                }
            }
        }

        public void SendCallback(IAsyncResult ir)
        {
            try
            {
                SockState.Sending = false;
                client.GetStream().EndWrite(ir);
                SocketEvent.Invoke(EventType.etDataSended, this, SockState, null);
            }
            catch (SocketException ex)
            {
                //WSAECONNRESET, the other side closed impolitely
                if (ex.ErrorCode == 10054)
                {
                    Close();
                    SocketEvent.Invoke(EventType.etDisconnected, this, SockState, null);
                    return;
                }
                SocketEvent.Invoke(EventType.etSocketError, this, SockState, ex);
                ContinueReading();
            }
            catch (Exception ex)
            {
                SocketEvent.Invoke(EventType.etSocketError, this, SockState, ex);
            }
        }

        // This is the callback function for TcpClient.GetStream.Begin. It begins an 
        // asynchronous read from a stream.
        private void StreamReceiver(IAsyncResult ar)
        {
            if (!FActive) return;
            int BytesRead = 0;

            // Ensure that no other threads try to use the stream at the same time.
            lock (client.GetStream())
            {
                try
                {
                    // Finish asynchronous read into readBuffer and get number of bytes read.
                    BytesRead = client.GetStream().EndRead(ar);
                }
                catch (SocketException ex)
                {
                    //WSAECONNRESET, the other side closed impolitely
                    if (ex.ErrorCode == 10054)
                    {
                        Close();
                        SocketEvent.Invoke(EventType.etDisconnected, this, SockState, null);
                        return;
                    }
                    SocketEvent.Invoke(EventType.etSocketError, this, SockState, ex);
                    ContinueReading();
                }
                catch (Exception ex)
                {
                    SocketEvent.Invoke(EventType.etSocketError, this, SockState, ex);
                }

                if (BytesRead == 0)
                {
                    // client disconnected
                    Close();
                }
                else
                {
                    MemoryStream data = new MemoryStream();
                    data.Write(readBuffer, 0, BytesRead);

                    data.Position = 0;

                    BinaryReader br = new BinaryReader(data);
                    length = br.ReadInt32();

                    Packet packet = new Packet(length);

                    try
                    {
                        // asynchronously read data into readBuffer.
                        client.GetStream().BeginRead(readBuffer, 0, length, new AsyncCallback(DoReadData), packet);
                    }
                    catch (SocketException ex)
                    {
                        //WSAECONNRESET, the other side closed impolitely
                        if (ex.ErrorCode == 10054)
                        {
                            Close();
                            SocketEvent.Invoke(EventType.etDisconnected, this, SockState, null);
                            return;
                        }
                        SocketEvent.Invoke(EventType.etSocketError, this, SockState, ex);
                        ContinueReading();
                    }
                    catch (Exception ex)
                    {
                        SocketEvent.Invoke(EventType.etSocketError, this, SockState, ex);
                        ContinueReading();
                    }
                }
            }        
        }

        private void DoReadData(IAsyncResult ar)
        {
            if (!FActive) return;
            // Resolve how many bytes we have to read
            Packet packet = (Packet)ar.AsyncState;
            int BytesRead;

            lock (client.GetStream())
            {
                try
                {
                    // Finish asynchronous read into readBuffer and return number of bytes read.
                    BytesRead = client.GetStream().EndRead(ar);

                    packet.data.Write(readBuffer, 0, BytesRead);
                    packet.ToRead -= BytesRead;

                    if (packet.ToRead == 0)
                    {
                        ContinueReading();
                        SocketEvent.Invoke(EventType.etDataReceived, this, SockState, packet.data.ToArray());
                    }
                    else
                    {
                        // Read the data
                        client.GetStream().BeginRead(readBuffer, 0, packet.ToRead, new AsyncCallback(DoReadData), packet);
                    }

                }
                catch (SocketException ex)
                {
                    //WSAECONNRESET, the other side closed impolitely
                    if (ex.ErrorCode == 10054)
                    {
                        Close();
                        SocketEvent.Invoke(EventType.etDisconnected, this, SockState, null);
                        return;
                    }
                    SocketEvent.Invoke(EventType.etSocketError, this, SockState, ex);
                    ContinueReading();
                }
                catch (Exception e)
                {
                    SocketEvent.Invoke(EventType.etSocketError, this, SockState, e);
                    SockState.Receiving = false;
                    ContinueReading();
                }
            }
        }

        public void ContinueReading()
        {
            if (!FActive) return;

            // Start a new asynchronous read into readBuffer.
            try
            {
                client.GetStream().BeginRead(readBuffer, 0, 4, new AsyncCallback(StreamReceiver), null);
            }
            catch (SocketException socketException)
            {
                //WSAECONNRESET, the other side closed impolitely
                if (socketException.ErrorCode == 10054)
                {
                    Close();
                    SocketEvent.Invoke(EventType.etDisconnected, this, SockState, null);
                    return;
                }
                SocketEvent.Invoke(EventType.etSocketError, this, SockState, socketException);
                ContinueReading();
            }
        }

        public void Close()
        {
            if (!FActive) return;
            FActive = false;

            try
            {
                client.Client.Close();
                SocketEvent.Invoke(EventType.etDisconnected, this, SockState, null);
            }
            catch
            {
            }
        }
    }
}