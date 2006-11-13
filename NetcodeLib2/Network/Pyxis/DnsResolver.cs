using System;
using System.Net;

namespace Netcode.Network.Pyxis.Networking
{
	public class DnsResolver
	{
		public DnsResolver(){}

		public IPHostEntry reslove(string host)
		{
			try
			{

				return Dns.Resolve(host);
			}
			catch
			{
				return null;
			}
		}
	}
}
