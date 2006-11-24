//Asynchronous server socket class
//(C) SLoW

using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Collections;
using System.Net;

namespace Netcode.Network.SlowSock.Server
{
    public class ServerSocket
    {
        private int PORT_NUM = 10000;
        public ArrayList clients = new ArrayList();
        private TcpListener listener;
        private Thread listenerThread;

        public bool IsBound { get { return listener.Server.IsBound; } }

        public delegate void OnSocketEvent(EventType type, SocketState state, object param);
        public event OnSocketEvent SocketEvent;

        public delegate void OnClientEvent(UserConnection client, UserConnection.EventType type, UserConnection.SocketState state, object param);
        public event OnClientEvent ClientEvent;

        public struct SocketState
        {
            public SocketStatus Status;
        }

        protected SocketState SockState;

        public enum EventType { etServerStarted, etServerStopped, etClientConnected, etClientDisconnected, etServerError }
        public enum SocketStatus { ssNotBinded, ssBinded }

        private bool Connected = false;

        public ServerSocket(int port)
        {
            PORT_NUM = port;
        }

        private bool CanRecieveData = false;
        ~ServerSocket()
        {
            CanRecieveData = false;
        }

        public bool Start()
        {
            bool isOK = false;

            if (Connected)
            {
                return false;
            }

            try
            {
                listenerThread = new Thread(new ThreadStart(DoListen));
                listenerThread.Start();

                isOK = true;
            }
            catch (Exception ex)
            {
                SocketEvent.Invoke(EventType.etServerError, SockState, ex);
            }

            return isOK;
        }

        // This subroutine is used a background listener thread to allow reading incoming
        // messages without lagging the user interface.
        private void DoListen()
        {
            CanRecieveData = true;
            try
            {
                // Listen for new connections.
                listener = new TcpListener(System.Net.IPAddress.Any, PORT_NUM);
                listener.Start();

                SockState.Status = SocketStatus.ssBinded;
                SocketEvent.Invoke(EventType.etServerStarted, SockState, null);
            }
            catch (Exception ex)
            {
                SocketEvent.Invoke(EventType.etServerError, SockState, ex);
                return;
            }

            while (listener.Server.IsBound)
            {
                try
                {
                    // Create a new user connection using TcpClient returned by
                    // TcpListener.AcceptTcpClient()
                    TcpClient cli = listener.AcceptTcpClient();
                    //if (cli.Connected && CanRecieveData)
                    {
                        UserConnection client = new UserConnection(cli);
                        clients.Add(client);
                        client.SocketEvent += new UserConnection.OnSocketEvent(client_SocketEvent);
                        // Raise an event
                        SocketEvent.Invoke(EventType.etClientConnected, SockState, client);
                    }
                }
                catch
                {
                    //SocketEvent.Invoke(EventType.etServerError, SockState, ex);
                }
            }
        }

        /// <summary>
        /// This delegate would be invoked when any client event occurs
        /// </summary>
        /// <param name="type"></param>
        /// <param name="sender"></param>
        /// <param name="state"></param>
        /// <param name="param"></param>
        void  client_SocketEvent(UserConnection.EventType type, UserConnection sender, UserConnection.SocketState state, object param)
        {
            if (type == UserConnection.EventType.etDisconnected)
            {
                clients.Remove(sender);
            }

            try
            {
                ClientEvent.Invoke(sender, type, state, param);
            }
            catch (Exception)
            {
                //TODO: 
            }
        }

        public UserConnection FindClient(string EndPoint)
        {
            foreach (UserConnection uc in clients)
            {
                if (uc.Name == EndPoint)
                    return uc;
            }

            return null;
        }

        public UserConnection FindClient2(string IPAddress)
        {
            foreach (UserConnection uc in clients)
            {
                string IPAddr = ((IPEndPoint)uc.Socket.RemoteEndPoint).Address.ToString();

                if (IPAddr.CompareTo(IPAddress) == 0)
                    return uc;
            }

            return null;
        }

        /// <summary>
        /// Tries to stop server
        /// </summary>
        /// <returns></returns>
        public bool Close()
        {
            bool isOK = false;

            if (SockState.Status != SocketStatus.ssBinded) return false;

            try
            {
                listener.Stop();
                isOK = true;
            }
            catch (Exception ex)
            {
                isOK = false;
                SocketEvent.Invoke(EventType.etServerError, SockState, ex);
            }

            return isOK;
        }
    }
}
