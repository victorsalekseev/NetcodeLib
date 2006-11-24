using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using System.Threading;
using Netcode.Network.SlowSock.Server;

namespace server_sample
{
    public partial class Form1 : Form
    {
        ServerSocket sock = new ServerSocket(10000);

        public bool isBound { get { return sock.IsBound; } }

        public Form1()
        {
            Thread.CurrentThread.Priority = ThreadPriority.Lowest;
            InitializeComponent();

            sock.SocketEvent += new ServerSocket.OnSocketEvent(sock_SocketEvent);
            sock.ClientEvent += new ServerSocket.OnClientEvent(sock_ClientEvent);

            sock.Start();
        }

        void sock_ClientEvent(UserConnection client, UserConnection.EventType type, UserConnection.SocketState state, object param)
        {
            switch (type)
            {
                case UserConnection.EventType.etDataReceived:
                    {
                        byte[] data = (byte[])param;
                        MemoryStream ms = new MemoryStream(data);

                        pictureBox2.Image = Bitmap.FromStream(ms);
                        break;
                    }
                case UserConnection.EventType.etSocketError:
                    {
                        MessageBox.Show(((Exception)param).Message);
                        break;
                    }
            }
        }

        void sock_SocketEvent(ServerSocket.EventType type, ServerSocket.SocketState state, object param)
        {
            switch (type)
            {
                case ServerSocket.EventType.etClientConnected:
                    {
                        UserConnection client = (UserConnection)param;
                        MemoryStream ms = new MemoryStream();

                        pictureBox1.Image.Save(ms, ImageFormat.Gif);

                        byte[] data = new byte[ms.Length];

                        ms.Position = 0;
                        ms.Read(data, 0, data.Length);

                        client.SendData(data);
                        break;
                    }
                case ServerSocket.EventType.etClientDisconnected:
                    {
                        MessageBox.Show("client disconnected");
                        break;
                    }
                case ServerSocket.EventType.etServerError:
                    {
                        MessageBox.Show(((Exception)param).Message);
                        break;
                    }
                case ServerSocket.EventType.etServerStopped:
                    {
                        MessageBox.Show("Stopped.", "server");
                        break;
                    }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            sock.SocketEvent -= new ServerSocket.OnSocketEvent(sock_SocketEvent);
            sock.ClientEvent -= new ServerSocket.OnClientEvent(sock_ClientEvent);
            sock.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}