using ArtNet.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ArtNet.ConsoleListener
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new UdpClient(Sender.ArtNetDefaultPort);

            Console.WriteLine("Waiting for data.");

            while (true)
            {
                var bytes = client.ReceiveAsync().Result.Buffer;
                Console.WriteLine();
                Console.WriteLine(BitConverter.ToString(bytes));
                var pairs = ByteHelper.GetAddressesAndValuesFromMessage(bytes);
                foreach (var pair in pairs)
                {
                    Console.WriteLine("{0:000} = {1:000}", pair.Address, pair.Value);
                }
            }
        }
    }
}
