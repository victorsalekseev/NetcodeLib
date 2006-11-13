using System;
using System.Text.RegularExpressions;

namespace Netcode.Network.Pyxis.Networking
{
	/// <summary>
	/// Summary description for check.
	/// </summary>
	public class Check
	{
		public static bool isValidIPv4(string ip)
		{
			Regex ipv4 = new Regex(@"\d{1,3}\.\d{1,3}\.\d{1,3}");
			return ipv4.Match(ip).Success; 
		}
	}
}
