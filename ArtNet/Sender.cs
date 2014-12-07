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
        private readonly UdpClient client;
        public const int ArtNetDefaultPort = 6454;
        private readonly string host;
        private readonly int port;

        public Sender(string host, int port = ArtNetDefaultPort)
        {
            client = new UdpClient();
            client.EnableBroadcast = true;
            this.host = host;
            this.port = port;
        }

        public async Task SendAsync(IArtNetPackage package)
        {
            var bytes = package.GetBytes();
            await client.SendAsync(bytes, bytes.Length, host, port);
        }
    }
}
