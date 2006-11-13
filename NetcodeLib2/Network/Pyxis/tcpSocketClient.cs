using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace Netcode.Network.Pyxis.Networking.Sockets.Tcp
{

	public class Client
	{

		protected Socket clientSocket = null;
		protected TcpClient clientStream = null;

		protected Thread tInboundMessages = null;

		protected Networking.Sockets.States state = Networking.Sockets.States.closed;

		protected bool run = false;

		protected int buf = 1024;

		public event SocketErrorHandler error;
		public event SocketHandler connected;
		public event SocketDataReceivedHandler dataReceived;
		public event SocketHandler dataSent;
		public event SocketHandler closed;

		public Client(int buffersize)
		{
			this.buf = buffersize;
		}

		public Client()
		{
		}


		public void accept(Socket socket)
		{
			if (this.state == Networking.Sockets.States.closed)
			{
				this.state = Networking.Sockets.States.connecting;
				
				this.clientSocket = socket;

				this.state = Networking.Sockets.States.connected;

				this.run = true;

				this.tInboundMessages = new Thread(new ThreadStart(pInboundS));
				this.tInboundMessages.IsBackground = true;
				this.tInboundMessages.Start();

				if (this.connected != null)
				{
					this.connected(this,new SocketEventArg(this.clientSocket));
				}
			}
			else
			{
				if (this.error != null)
				{
					this.error(this,new SocketErrorArg("Client already connected."));
				}
			}
		}

		public bool connect(string host, int port)
		{
			if (this.state == Networking.Sockets.States.closed)
			{
				this.state = Networking.Sockets.States.connecting;
				
				DnsResolver dnsr = new DnsResolver();
				IPHostEntry ip = dnsr.reslove(host);
				if (ip != null)
				{
					this.clientStream = new TcpClient();

					try
					{
						this.clientStream.Connect(host,port);
					}
					catch
					{
						this.state = Networking.Sockets.States.error;
						if (this.error != null)
						{
							this.error(this,new SocketErrorArg("Couldn't connect to host",null));
						}
						return false;
					}
					
					this.state = Networking.Sockets.States.connected;

					this.run = true;

					this.tInboundMessages = new Thread(new ThreadStart(pInboundC));
					this.tInboundMessages.Start();

					if (this.connected != null)
					{
						this.connected(this,new SocketEventArg(this.clientSocket));
					}

				}
				else
				{
					if (this.error != null)
					{
						this.error(this,new SocketErrorArg("Couldn't find host",null));
					}
				}
				
			}
			else
			{
				if (this.error != null)
				{
					this.error(this,new SocketErrorArg("Client already connected."));
				}
			}

			this.state = Networking.Sockets.States.connected;
			return true;
		}

		public void close()
		{
			//
			//TODO: Kill the running threads
			//
			//
			this.state = Networking.Sockets.States.closing;

			this.run = false;

			if (this.tInboundMessages != null)
			{
				if (this.tInboundMessages.ThreadState != ThreadState.Suspended)
				{
					this.tInboundMessages.Suspend();
					this.tInboundMessages = null;
				}
			}

			if (this.clientSocket != null)
			{
				this.clientSocket.Close();
				this.clientSocket = null;
			}

			if (this.clientStream != null)
			{
				this.clientStream.Close();
				this.clientStream = null;
			}

			this.state = Networking.Sockets.States.closed;
		}

		private void pInboundS()
		{
			try
			{
				byte[] rd;
				do
				{
					rd = this.trimbuffer(receiveData(this.clientSocket));
					if (dataReceived != null)
					{
						dataReceived(this,new SocketDataEventArg(this.clientSocket,rd));
					}
					
				}while (rd.Length > 0 && this.run);

				if (this.closed != null)
				{
					this.closed(this,new SocketEventArg(null));
				}
	
			}
			catch
			{
				this.close();
			}
		}

		private void pInboundC()
		{
			try
			{
				byte[] rd;
				do
				{
					rd = this.trimbuffer(receiveData(this.clientStream.GetStream()));
					if (dataReceived != null)
					{
						dataReceived(this,new SocketDataEventArg(rd));
					}
					
				}while (rd.Length > 0 && this.run);
	
			}
			catch(SocketException e)
			{
				if (this.error != null)
				{
					this.error(this,new SocketErrorArg(e.Message + "Error listenig for incomming messages",this.clientSocket));
				}
			}
		}


		private byte[] receiveData(Stream client)
		{
			byte[] data = null;

			byte[] buffer = new byte[this.buf];

			int bytes = 0;
			int totalBytes = 0;

			try
			{

				while( (bytes = client.Read( buffer, 0, buffer.Length )) == buffer.Length )
				{
					if (data == null)
					{
						data = new byte[buffer.Length];
						Array.Copy(buffer,data,buffer.Length);
					}
					else
					{
						byte[] tmp = new byte[data.Length];
						tmp = data;
						data = new byte[tmp.Length + buffer.Length];

						Array.Copy(tmp,0,data,0,tmp.Length);
						Array.Copy(buffer,0,data,tmp.Length,buffer.Length);
					}	
					totalBytes += bytes;
				}
			

				if( bytes > 0  && bytes < this.buf )
				{
					if (data == null)
					{
						data = new byte[buffer.Length];
						Array.Copy(buffer,data,buffer.Length);
					}
					else
					{
						byte[] tmp = new byte[data.Length];
						tmp = data;
						data = new byte[tmp.Length + buffer.Length];

						Array.Copy(tmp,0,data,0,tmp.Length);
						Array.Copy(buffer,0,data,tmp.Length,buffer.Length);
					}	
					totalBytes += bytes;
				}

			}
			catch
			{
				if (this.error != null)
				{
					this.error(this,new SocketErrorArg("Connection lost with remote host",null));
				}

				this.state = Networking.Sockets.States.error;

				this.close();

				data = new byte[1] {0};
			}

			return data;

		}

		private byte[] receiveData(Socket client)
		{
			int bytesRecvd = 0;
			int totalBytes = 0;

			byte[] data = null;

			byte[] buffer = new Byte[this.buf];

			try
			{
				do
				{
					bytesRecvd = client.Receive(buffer,0,this.buf,SocketFlags.None);

					if (bytesRecvd > 0)
					{
						if (data == null)
						{
							data = new byte[buffer.Length];
							Array.Copy(buffer,data,buffer.Length);
						}
						else
						{
							byte[] tmp = new byte[data.Length];
							tmp = data;
							data = new byte[tmp.Length + buffer.Length];

							Array.Copy(tmp,0,data,0,tmp.Length);
							Array.Copy(buffer,0,data,tmp.Length,buffer.Length);
						}
						totalBytes += bytesRecvd;
					}
				}while (bytesRecvd == this.buf);
			}
			catch(SocketException se)
			{

				if (se.NativeErrorCode != 10054)
				{
					if (this.error != null)
					{
						Console.WriteLine(se.NativeErrorCode);
						this.error(this,new SocketErrorArg(se.Message,client));
					}
				}
				else
				{
					if (this.closed != null)
					{
						this.closed(this,new SocketEventArg(null));
					}
				}
			}
			
			return data;
					
		}

		private byte[] trimbuffer(byte[] buffer)
		{
			if (buffer == null || buffer[buffer.GetUpperBound(0)] != 0)
			{
				return buffer;
			}
			else
			{
				byte[] tmp;
				int ub = buffer.GetUpperBound(0)-1;
				int lb = buffer.GetLowerBound(0);

				for (;ub>=lb;ub--)
				{
					if (buffer[ub] != 0)
					{
						break;
					}
				}

				tmp = new byte[++ub];
				Array.Copy(buffer,0,tmp,0,ub);

				return tmp;
			}
			
		}

		public void sendData(byte[] data)
		{
			if (this.clientSocket != null)	
			{
				try
				{
					this.clientSocket.SendTo(data,data.Length,SocketFlags.None,this.clientSocket.RemoteEndPoint);
					if (this.dataSent != null)
					{
						this.dataSent(this,new SocketEventArg(this.clientSocket));
					}
				}
				catch
				{
					if (this.error != null)
					{
						this.error(this,new SocketErrorArg("Failed to send data",this.clientSocket));
					}
				}
			}
			else
			{
				this.clientStream.GetStream().Write(data,0,data.Length);
			}
		}
	}
}
