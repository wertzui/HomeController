using ArtNet;
using ArtNet.Common;
using ArtNet.Fascade;
using Fixtures;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artnet.ConsoleSender
{
    class Program
    {
        static void Main(string[] args)
        {
            var fascade = new ArtNetFascade(0, "255.255.255.255");
            var fixtures = new Dictionary<short, ArtNetWhiteFixture>();

            while (true)
            {
                Console.WriteLine("Write addresses and values seperated by comma. (1,1,2,2 to set address 1 to value 1 and address 2 to value 2)");
                var s = Console.ReadLine();
                var array = s.Split(',');
                if (array.Length % 2 != 0)
                    Console.WriteLine("Odd number of values.");
                else
                {
                    //var pairs = new List<AddressValuePair>();
                    for (int i = 0; i < array.Length - 1; i += 2)
                    {
                        var address = short.Parse(array[i], CultureInfo.InvariantCulture);
                        var value = double.Parse(array[i + 1], CultureInfo.InvariantCulture);
                        //pairs.Add(new AddressValuePair(address, value));
                        ArtNetWhiteFixture fixture;
                        if (!fixtures.TryGetValue(address, out fixture))
                        {
                            fixture = new ArtNetWhiteFixture(fascade, address, address.ToString());
                            fixtures[address] = fixture;
                        }
                        fixture.Fade(value, 1000);
                    }

                    //fascade.SendAsync(pairs.ToArray()).Wait();
                    Console.WriteLine("Package sent.");
                    Console.WriteLine();
                }
            }
        }
    }
}
