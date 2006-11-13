using System;
using System.Net;
using System.Net.Sockets;

namespace Netcode.Network.Pyxis.Networking.Sockets
{
	public enum States : byte
	{
		closed, 
		closing,
		connected,
		connecting,
		connectionPending,
		error,
		hostResolved,
		listening,
		resolvingHost
	}

	public delegate void SocketHandler(object sender,SocketEventArg e);

	public delegate void SocketErrorHandler(object sender,SocketErrorArg e);

	public delegate void SocketDataReceivedHandler(object sender,SocketDataEventArg e);

	public class SocketEventArg : EventArgs
	{
		private Socket eSocket = null;

		public SocketEventArg(Socket socket)
		{
			this.eSocket = socket;
		}

		public System.Net.Sockets.Socket socket
		{
			get
			{
				return this.eSocket;
			}
		}
	}

	public class SocketDataEventArg : EventArgs
	{
		private Socket eSocket = null;
		private byte[] edata = null;

		public SocketDataEventArg(byte[] data)
		{
			this.edata = data;
		}

		public SocketDataEventArg(Socket socket,byte[] data)
		{
			this.eSocket = socket;
			this.edata = data;
		}

		public System.Net.Sockets.Socket socket
		{
			get
			{
				return this.eSocket;
			}
		}

		public byte[] data
		{
			get
			{
				return this.edata;
			}
		}
	}

	public class SocketErrorArg : EventArgs
	{
		private Socket eSocket = null;
		private string eMessage = "";

		public SocketErrorArg(string message, Socket socket)
		{
			this.eSocket = socket;
			this.eMessage = message;
		}

		public SocketErrorArg(string message)
		{
			this.eMessage = message;
		}

		public System.Net.Sockets.Socket socket
		{
			get
			{
				return this.eSocket;
			}
		}

		public string message
		{
			get
			{
				return this.eMessage;
			}
		}
	}
}
