using ArtNet.Common;
using System;
using System.Net.Sockets;

namespace ArtNet.ConsoleListener
{
    /// <summary>
    /// This is just a small helper programm to debug ArtNet related problems.
    /// It just prints out all received ArtNet values.
    /// </summary>
    internal class Program
    {
        private static void Main()
        {
            using (var client = new UdpClient(Sender.ArtNetDefaultPort))
            {
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
}