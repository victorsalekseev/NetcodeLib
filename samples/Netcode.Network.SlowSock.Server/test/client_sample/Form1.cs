using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Net;
using Netcode.Network.SlowSock.Client;

namespace client_sample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // Establish the local endpoint for the socket.
            IPHostEntry ipHost = Dns.Resolve(Dns.GetHostName());
            foreach (IPAddress ipAddr in ipHost.AddressList)
            {
                 textBox1.Items.Add(ipAddr.ToString());
            }

            client.SocketEvent += new ClientSocket.OnSocketEvent(client_SocketEvent);
        }

        ClientSocket client = new ClientSocket();

        private void button1_Click(object sender, EventArgs e)
        {
            client.Connect(textBox1.Text, 10000);
        }

        void client_SocketEvent(ClientSocket.EventType type, ClientSocket.SocketState state, object param)
        {
            switch (type)
            {
                case ClientSocket.EventType.etConnected:
                    {
                        label4.Text = "Connected.";
                        break;
                    }
                case ClientSocket.EventType.etDisconnected:
                    {
                        label4.Text = "Disconnected.";
                        break;
                    }
                case ClientSocket.EventType.etDataReceived:
                    {
                        byte[] data = (byte[])param;

                        pictureBox1.Image = new Bitmap(new MemoryStream(data));
                        client.SendData(data);
                        break;
                    }
                case ClientSocket.EventType.etDataSended:
                    {
                        MessageBox.Show("Sended.");
                        break;
                    }
                case ClientSocket.EventType.etSocketError:
                    {
                        Exception ex = (Exception) param;

                        MessageBox.Show((IWin32Window)null, ex.Message, "client");
                        break;
                    }
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            client.Disconnect();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MemoryStream stream = new MemoryStream();

            CaptureRect(Screen.PrimaryScreen.Bounds).Save(stream, ImageFormat.Jpeg);
            client.SendData(stream.ToArray());

            stream.Close();
        }

        /// <summary>
        /// Captures a rectangle from desktop
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        private Bitmap CaptureRect(Rectangle rect)
        {
            IntPtr hdcSrc = GetDC(IntPtr.Zero);

            IntPtr hdcDest = CreateCompatibleDC(hdcSrc);

            IntPtr hBitmap = IntPtr.Zero;

            hBitmap = CreateCompatibleBitmap(hdcSrc, rect.Width, rect.Height);

            SelectObject(hdcDest, hBitmap);

            BitBlt(hdcDest, 0, 0, rect.Width, rect.Height,
                hdcSrc, rect.Left, rect.Top, (uint)TernaryRasterOperations.SRCCOPY);

            Bitmap bmp = Image.FromHbitmap(hBitmap); ;
            DeleteObject(hBitmap);

            DeleteDC(hdcDest);

            return bmp;
        }

        /// <summary>
        ///     Specifies a raster-operation code. These codes define how the color data for the
        ///     source rectangle is to be combined with the color data for the destination
        ///     rectangle to achieve the final color.
        /// </summary>
        public enum TernaryRasterOperations : uint
        {
            /// <summary>dest = source</summary>
            SRCCOPY = 0x00CC0020,
            /// <summary>dest = source OR dest</summary>
            SRCPAINT = 0x00EE0086,
            /// <summary>dest = source AND dest</summary>
            SRCAND = 0x008800C6,
            /// <summary>dest = source XOR dest</summary>
            SRCINVERT = 0x00660046,
            /// <summary>dest = source AND (NOT dest)</summary>
            SRCERASE = 0x00440328,
            /// <summary>dest = (NOT source)</summary>
            NOTSRCCOPY = 0x00330008,
            /// <summary>dest = (NOT src) AND (NOT dest)</summary>
            NOTSRCERASE = 0x001100A6,
            /// <summary>dest = (source AND pattern)</summary>
            MERGECOPY = 0x00C000CA,
            /// <summary>dest = (NOT source) OR dest</summary>
            MERGEPAINT = 0x00BB0226,
            /// <summary>dest = pattern</summary>
            PATCOPY = 0x00F00021,
            /// <summary>dest = DPSnoo</summary>
            PATPAINT = 0x00FB0A09,
            /// <summary>dest = pattern XOR dest</summary>
            PATINVERT = 0x005A0049,
            /// <summary>dest = (NOT dest)</summary>
            DSTINVERT = 0x00550009,
            /// <summary>dest = BLACK</summary>
            BLACKNESS = 0x00000042,
            /// <summary>dest = WHITE</summary>
            WHITENESS = 0x00FF0062
        }

        /// <summary>
        ///    Performs a bit-block transfer of the color data corresponding to a
        ///    rectangle of pixels from the specified source device context into
        ///    a destination device context.
        /// </summary>
        /// <param name="hdc">Handle to the destination device context.</param>
        /// <param name="nXDest">The leftmost x-coordinate of the destination rectangle (in pixels).</param>
        /// <param name="nYDest">The topmost y-coordinate of the destination rectangle (in pixels).</param>
        /// <param name="nWidth">The width of the source and destination rectangles (in pixels).</param>
        /// <param name="nHeight">The height of the source and the destination rectangles (in pixels).</param>
        /// <param name="hdcSrc">Handle to the source device context.</param>
        /// <param name="nXSrc">The leftmost x-coordinate of the source rectangle (in pixels).</param>
        /// <param name="nYSrc">The topmost y-coordinate of the source rectangle (in pixels).</param>
        /// <param name="dwRop">A raster-operation code.</param>
        /// <returns>
        ///    <c>true</c> if the operation succeeded, <c>false</c> otherwise.
        /// </returns>
        [DllImport("gdi32.dll")]
        static extern bool BitBlt(IntPtr hdc, int nXDest, int nYDest, int nWidth,
           int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, uint dwRop);

        [DllImport("gdi32.dll")]
        static extern IntPtr CreateDC(string lpszDriver, string lpszDevice,
           string lpszOutput, IntPtr lpInitData);

        [DllImport("user32.dll")]
        static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("Gdi32.dll")]
        public static extern IntPtr CreateCompatibleBitmap(IntPtr hDC, int nWidth, int nHeight);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        static extern bool DeleteDC(IntPtr hdc);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        static extern bool DeleteObject(IntPtr hObject);

        private void Form1_Load(object sender, EventArgs e)
        {

        }

    }
}