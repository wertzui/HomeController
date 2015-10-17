using ArtNet.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ArtNet
{
    public class Sender
    {
        readonly UdpClient client;
        public const int ArtNetDefaultPort = 6454;
        readonly string host;
        readonly int port;

        public Sender(string host, int port = ArtNetDefaultPort)
        {
            client = new UdpClient
            {
                EnableBroadcast = true
            };
            this.host = host;
            this.port = port;
        }

        public async Task SendAsync(IArtNetPackage package)
        {
            var bytes = package.GetBytes();
            Console.WriteLine("ArtNet: sending package to host {0}", host);
            await client.SendAsync(bytes, bytes.Length, host, port);
        }
    }
}
