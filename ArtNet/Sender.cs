using ArtNet.Common;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace ArtNet
{
    public class Sender
    {
        private readonly UdpClient client;
        public const int ArtNetDefaultPort = 6454;
        private readonly int port;
        readonly ISet<string> hosts;

        public Sender(ISet<string> hosts, int port = ArtNetDefaultPort)
        {
            client = new UdpClient
            {
                EnableBroadcast = true
            };
            this.port = port;
            this.hosts = hosts;
        }

        public async Task SendAsync(IArtNetPackage package)
        {
            var bytes = package.GetBytes();
            foreach (var host in hosts)
            {
                Console.WriteLine("ArtNet: sending package to host {0}", host);
                await client.SendAsync(bytes, bytes.Length, host, port);
            }
        }
    }
}