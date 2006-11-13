using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Netcode.Network.Pyxis.Networking.Sockets.Tcp
{
	public class Server
	{
		protected Sockets.States states = Sockets.States.closed;

		protected Thread tListening = null;

		protected TcpListener tcpListener = null;

		protected bool run = false;

		public event Networking.Sockets.SocketHandler connectionRequest;
		public event Networking.Sockets.SocketErrorHandler error;

		~Server()
		{
			this.stop();
		}

		public void start(int port)
		{
			this.run = true;

			this.tcpListener = new TcpListener(IPAddress.Any,port);
			this.tcpListener.Start();

			this.tListening = new Thread(new ThreadStart(this.pListener));
			this.tListening.IsBackground = true;
			this.tListening.Start();

			this.states = Sockets.States.listening;

		}

		public void stop()
		{
			this.states = Sockets.States.closing;

			try
			{
				this.run = false;

				if (this.tcpListener != null)
				{
					this.tcpListener.Stop();
					this.tcpListener = null;
				}
				
				if (this.tListening != null)
				{
					this.tListening.Suspend();
					this.tListening = null;
				}
			}
			finally
			{

				this.states = Sockets.States.closed;

			}
			
		}

		protected void pListener()
		{
			try
			{
				do
				{
					if (this.connectionRequest != null)
					{
						this.connectionRequest(this,new SocketEventArg(tcpListener.AcceptSocket()));
					}
				}while(this.run);
			}
			catch(SocketException e)
			{
				if (this.error != null)
				{
					this.error(this,new SocketErrorArg(e.Message));
				}
			}
		}
	}
}
