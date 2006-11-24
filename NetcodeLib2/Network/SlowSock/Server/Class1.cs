using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Collections;

namespace SLoW.Sockets
{
    public class ServerSocket
    {
        private int PORT_NUM = 10000;
        private Hashtable clients = new Hashtable();
        private TcpListener listener;
        private Thread listenerThread;

        public delegate void OnDataReceive(UserConnection client, byte[] data);
        public event OnDataReceive OnDataReceived;

        public ServerSocket(int port)
        {
            PORT_NUM = port;

            listenerThread = new Thread(new ThreadStart(DoListen));
            listenerThread.Start();
        }

        // This subroutine is used a background listener thread to allow reading incoming
        // messages without lagging the user interface.
        private void DoListen()
        {
            try
            {
                // Listen for new connections.
                listener = new TcpListener(System.Net.IPAddress.Any, PORT_NUM);
                listener.Start();

                do
                {
                    // Create a new user connection using TcpClient returned by
                    // TcpListener.AcceptTcpClient()
                    UserConnection client = new UserConnection(listener.AcceptTcpClient());
                    // Create an event handler to allow the UserConnection to communicate
                    // with the window.
                    client.LineReceived += new LineReceive(client_LineReceived);

                } while (true);
            }
            catch
            {
            }
        }

        void client_LineReceived(UserConnection sender, byte[] Data)
        {
            OnDataReceived.Invoke(sender, Data);
        }


    }
}
