using System;
using System.Net;
using System.Net.Sockets;
using System.Globalization;

namespace Netcode.Network.Pyxis.Networking
{

	public class Wol:UdpClient    
	{
		public Wol():base()
		{ }
		//this is needed to send broadcast packet
		private void SetClientToBroadcastMode()    
		{
			if(this.Active)
			{
				this.Client.SetSocketOption(SocketOptionLevel.Socket,SocketOptionName.Broadcast,0);
			}
		}

		public static void WakeUp(string MAC_ADDRESS)   
		{
		}
	}
}