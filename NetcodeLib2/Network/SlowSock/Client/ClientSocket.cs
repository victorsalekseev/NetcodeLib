//Asynchronous client socket class
//(C) SLoW

using System;
using System.IO;
using System.Net.Sockets;
using System.Net;

namespace Netcode.Network.SlowSock.Client
{
    public class ClientSocket
    {
        const int READ_BUFFER_SIZE = 5000000;
        private int PORT_NUM = 10000;
        private TcpClient client;
        private byte[] readBuffer = new byte[READ_BUFFER_SIZE];
        int length = 0;
        private int TimeOut = 500;

        public delegate void OnSocketEvent(EventType type, SocketState state, object param);
        public event OnSocketEvent SocketEvent;

        public struct SocketState
        {
            public SocketStatus Status;
            public bool Receiving, Sending;
        }

        public class Packet
        {
            public MemoryStream data = new MemoryStream();
            public int ToRead;

            public Packet(int Length)
            {
                ToRead = Length;
            }
        }

        protected SocketState SockState;

        public enum EventType { etConnected, etDisconnected, etDataReceived, etDataSended, etSocketError }
        public enum SocketStatus {ssNotConnected, ssConnected }

        public ClientSocket()
        {
            // set socket state
            SockState.Receiving = false;
            SockState.Sending = false;
        }

        public void Connect(string IPAddr, int port)
        {
            try
            {
                PORT_NUM = port;
                // The TcpClient is a subclass of Socket, providing higher level 
                // functionality like streaming.
                client = new TcpClient(IPAddr, PORT_NUM);
                
                // Set status
                SockState.Status = SocketStatus.ssConnected;

                // Start an asynchronous read invoking DoRead to avoid lagging the user
                // interface.
                client.GetStream().BeginRead(readBuffer, 0, 4, new AsyncCallback(DoRead), null);
                SockState.Receiving = true;
            }
            catch (Exception Ex)
            {
                SockState.Status = SocketStatus.ssNotConnected;
                SocketEvent.Invoke(EventType.etSocketError, SockState, Ex);
            }

            SocketEvent.Invoke(EventType.etConnected, SockState, null);
        }



        /// <summary>
        /// Starts asynchronous disconnect; on the end of this OnDisconnect would be invoked
        /// </summary>
        public void Disconnect()
        {
            try
            {
                client.Client.Close(TimeOut);
                SockState.Status = SocketStatus.ssNotConnected;
                SocketEvent.Invoke(EventType.etDisconnected, SockState, null);
            }
            catch (Exception e)
            {
                SocketEvent.Invoke(EventType.etSocketError, SockState, e);
            }
        }

        // This is the callback function for TcpClient.GetStream.Begin to get an
        // asynchronous read.
        private void DoRead(IAsyncResult ar)
        {
            int BytesRead;

            try
            {
                // Finish asynchronous read into readBuffer and return number of bytes read.
                BytesRead = client.GetStream().EndRead(ar);
                if (BytesRead < 1)
                {
                    // if no bytes were read server has close.
                    
                    return;
                }

                MemoryStream data = new MemoryStream();
                data.Write(readBuffer, 0, BytesRead);

                data.Position = 0;

                BinaryReader br = new BinaryReader(data);
                length = br.ReadInt32();

                Packet packet = new Packet(length);

                // Read the data
                client.GetStream().BeginRead(readBuffer, 0, length, new AsyncCallback(DoReadData), packet);

            }
            catch (SocketException socketException)
            {
                //WSAECONNRESET, the other side closed impolitely
                if (socketException.ErrorCode == 10054)
                {
                    Disconnect();
                }
            }
            catch (Exception e)
            {
                SocketEvent.Invoke(EventType.etSocketError, SockState, e);
                SockState.Receiving = false;
            }
        }

        private void DoReadData(IAsyncResult ar)
        {
            // Resolve how many bytes we have to read
            Packet packet = (Packet)ar.AsyncState;
            int BytesRead;

            try
            {
                // Finish asynchronous read into readBuffer and return number of bytes read.
                BytesRead = client.GetStream().EndRead(ar);
                if (BytesRead < 1)
                {
                    // if no bytes were read server has close.

                    return;
                }

                packet.data.Write(readBuffer, 0, BytesRead);
                packet.ToRead -= BytesRead;

                if (packet.ToRead == 0)
                {
                    ContinueReading();
                    SocketEvent.Invoke(EventType.etDataReceived, SockState, packet.data.ToArray());
                }
                else
                {
                    // Read the data
                    client.GetStream().BeginRead(readBuffer, 0, packet.ToRead, new AsyncCallback(DoReadData), packet);
                }

            }
            catch (SocketException socketException)
            {
                //WSAECONNRESET, the other side closed impolitely
                if (socketException.ErrorCode == 10054)
                {
                    Disconnect();
                }
            }
            catch (Exception e)
            {
                SocketEvent.Invoke(EventType.etSocketError, SockState, e);
                SockState.Receiving = false;
            }

        }

        public void ContinueReading()
        {
            // Start a new asynchronous read into readBuffer.
            try
            {
                client.GetStream().BeginRead(readBuffer, 0, 4, new AsyncCallback(DoRead), null);
            }
            catch (SocketException socketException)
            {
                //WSAECONNRESET, the other side closed impolitely
                if (socketException.ErrorCode == 10054)
                {
                    Disconnect();
                }
            }
        }

        public void SendData(byte[] data)
        {
            SockState.Sending = true;

            MemoryStream ms = new MemoryStream();
            BinaryWriter bw = new BinaryWriter(ms);
            
            bw.Write(data.Length);
            bw.Write(data);

            ms.Position = 0;
            byte[] d = new byte[ms.Length];
            ms.Read(d, 0, d.Length);

            try
            {
                client.GetStream().BeginWrite(d, 0, d.Length, new AsyncCallback(SendCallback), null);
            }
            catch (SocketException socketException)
            {
                //WSAECONNRESET, the other side closed impolitely
                if (socketException.ErrorCode == 10054)
                {
                    Disconnect();
                }
            }
            catch (Exception ex)
            {
                SocketEvent.Invoke(EventType.etSocketError, SockState, ex);
                SockState.Sending = false;
            }
        }

        public void SendCallback(IAsyncResult ir)
        {
            try
            {
                SockState.Sending = false;
                client.GetStream().EndWrite(ir);
                SocketEvent.Invoke(EventType.etDataSended, SockState, null);

            }
            catch (SocketException socketException)
            {
                //WSAECONNRESET, the other side closed impolitely
                if (socketException.ErrorCode == 10054)
                {
                    Disconnect();
                }
            }
            catch (Exception ex)
            {
                SocketEvent.Invoke(EventType.etSocketError, SockState, ex);
            }
        }
    }
}
