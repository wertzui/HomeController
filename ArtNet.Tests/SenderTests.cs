using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtNet;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Sockets;
using System.Net;
using ArtNet.Common;
namespace ArtNet.Tests
{
    [TestClass()]
    public class SenderTests
    {

        [TestMethod()]
        public async Task SendTest()
        {
            // Arrange
            var package = new ArtDmxPackage(new byte[] { 1, 2 });
            var expected = package.GetBytes();
            expected[12] = 2;
            var receiver = new UdpClient(Sender.ArtNetDefaultPort);
            var sender = new Sender("127.0.0.1");

            // Act
            var t = receiver.ReceiveAsync();
            await sender.SendAsync(package);
            var actual = (await t).Buffer;

            // Assert
            Assert.IsNotNull(actual);
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
