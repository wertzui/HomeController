using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtNet;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ArtNet.Common;
namespace ArtNet.Tests
{
    [TestClass()]
    public class ArtDmxPackageTests
    {

        [TestMethod()]
        public void ArtDmxPackageTestForCorrectHeader()
        {
            // Arrange
            var data = new byte[0];

            // Act
            var actual = new ArtDmxPackage(data);
            var actualBytes = actual.GetBytes();

            // Assert
            Assert.AreEqual(65, actualBytes[0], "A");
            Assert.AreEqual(114, actualBytes[1], "r");
            Assert.AreEqual(116, actualBytes[2], "t");
            Assert.AreEqual(45, actualBytes[3], "-");
            Assert.AreEqual(78, actualBytes[4], "N");
            Assert.AreEqual(101, actualBytes[5], "e");
            Assert.AreEqual(116, actualBytes[6], "t");
            Assert.AreEqual(0, actualBytes[7], "\0");
            Assert.AreEqual(0, actualBytes[8], "OpCodeLo");
            Assert.AreEqual(80, actualBytes[9], "OpCodeHi");
            Assert.AreEqual(0, actualBytes[10], "ProtVerHi");
            Assert.AreEqual(14, actualBytes[11], "ProtVerLo");
            Assert.AreEqual(1, actualBytes[12], "Sequence");
            Assert.AreEqual(0, actualBytes[13], "Physical");
            Assert.AreEqual(0, actualBytes[14], "SubUni");
            Assert.AreEqual(0, actualBytes[15], "Net");
            Assert.AreEqual(2, actualBytes[16], "LengthHi");
            Assert.AreEqual(0, actualBytes[17], "Length");
        }

        [TestMethod()]
        public void ArtDmxPackageCtorByteArrayEmpty()
        {
            // Arrange
            var data = new byte[0];

            // Act
            var actual = new ArtDmxPackage(data);
            var actualBytes = actual.GetBytes();

            // Assert
            Assert.AreEqual(0, actual.Universe);
            Assert.AreEqual(0, actualBytes[18]);
            Assert.AreEqual(0, actualBytes[19]);
        }

        [TestMethod()]
        public void ArtDmxPackageCtorByteArray1Element()
        {
            // Arrange
            var data = new byte[] { 1 };

            // Act
            var actual = new ArtDmxPackage(data);
            var actualBytes = actual.GetBytes();

            // Assert
            Assert.AreEqual(0, actual.Universe);
            Assert.AreEqual(1, actualBytes[18]);
            Assert.AreEqual(0, actualBytes[19]);
        }

        [TestMethod()]
        public void ArtDmxPackageCtorByteArray2Elements()
        {
            // Arrange
            var data = new byte[] { 1, 2 };

            // Act
            var actual = new ArtDmxPackage(data);
            var actualBytes = actual.GetBytes();

            // Assert
            Assert.AreEqual(0, actual.Universe);
            Assert.AreEqual(1, actualBytes[18]);
            Assert.AreEqual(2, actualBytes[19]);
        }

        [TestMethod()]
        public void ArtDmxPackageCtorByteArray3Elements()
        {
            // Arrange
            var data = new byte[] { 1, 2, 3 };

            // Act
            var actual = new ArtDmxPackage(data);
            var actualBytes = actual.GetBytes();

            // Assert
            Assert.AreEqual(0, actual.Universe);
            Assert.AreEqual(1, actualBytes[18]);
            Assert.AreEqual(2, actualBytes[19]);
            Assert.AreEqual(3, actualBytes[20]);
            Assert.AreEqual(0, actualBytes[21]);
        }

        [TestMethod()]
        public void ArtDmxPackageCtorByteArray512Elements()
        {
            // Arrange
            var data = Enumerable.Range(0, 512).Select(i => (byte)i).ToArray();

            // Act
            var actual = new ArtDmxPackage(data);
            var actualBytes = actual.GetBytes();

            // Assert
            Assert.AreEqual(512, actual.Length);
            Assert.AreEqual(0, actual.Universe);
            Assert.AreEqual(530, actualBytes.Length);
            for (int i = 18; i < 530; i++)
            {
                Assert.AreEqual(data[i - 18], actualBytes[i], "i = " + i);
            }
        }

        [TestMethod()]
        public void ArtDmxPackageCtorByteArray513Elements()
        {
            // Arrange
            var data = Enumerable.Range(0, 513).Select(i => (byte)i).ToArray();

            // Act
            var actual = new ArtDmxPackage(data);
            var actualBytes = actual.GetBytes();

            // Assert
            Assert.AreEqual(0, actual.Universe);
            for (int i = 18; i < 530; i++)
            {
                Assert.AreEqual(data[i - 18], actualBytes[i], "i = " + i);
            }
        }

        [TestMethod()]
        public void ArtDmxPackageCtorPairs1ExplicitEmpty()
        {
            // Arrange
            var data = new byte[0];

            // Act
            var actual = new ArtDmxPackage(0, 0);
            var actualBytes = actual.GetBytes();

            // Assert
            Assert.AreEqual(0, actual.Universe);
            Assert.AreEqual(0, actualBytes[18]);
            Assert.AreEqual(0, actualBytes[19]);
        }

        [TestMethod()]
        public void ArtDmxPackageCtorPairs1Explicit()
        {
            // Arrange
            var data = new byte[0];

            // Act
            var actual = new ArtDmxPackage(1, 1);
            var actualBytes = actual.GetBytes();

            // Assert
            Assert.AreEqual(0, actual.Universe);
            Assert.AreEqual(1, actualBytes[18]);
            Assert.AreEqual(0, actualBytes[19]);
        }

        [TestMethod()]
        public void ArtDmxPackageCtorPairs1ExplicitMaxValue()
        {
            // Arrange
            var data = new byte[0];

            // Act
            var actual = new ArtDmxPackage(512, 1);
            var actualBytes = actual.GetBytes();

            // Assert
            Assert.AreEqual(512, actual.Length);
            Assert.AreEqual(0, actual.Universe);
            Assert.AreEqual(530, actualBytes.Length);
            for (int i = 18; i < 529; i++)
            {
                Assert.AreEqual(0, actualBytes[i], "i = " + i);
            }

            Assert.AreEqual(1, actualBytes[529]);
        }

        [TestMethod()]
        public void ArtDmxPackageCtorPairs2Explicit()
        {
            // Arrange

            // Act
            var actual = new ArtDmxPackage(1, 1, 2, 2);
            var actualBytes = actual.GetBytes();

            // Assert
            Assert.AreEqual(0, actual.Universe);
            Assert.AreEqual(1, actualBytes[18]);
            Assert.AreEqual(2, actualBytes[19]);
        }

        [TestMethod()]
        public void ArtDmxPackageCtorPairs3Explicit()
        {
            // Arrange

            // Act
            var actual = new ArtDmxPackage(1, 1, 2, 2, 3, 3);
            var actualBytes = actual.GetBytes();

            // Assert
            Assert.AreEqual(0, actual.Universe);
            Assert.AreEqual(1, actualBytes[18]);
            Assert.AreEqual(2, actualBytes[19]);
            Assert.AreEqual(3, actualBytes[20]);
        }

        [TestMethod()]
        public void ArtDmxPackageCtorPairs4Explicit()
        {
            // Arrange

            // Act
            var actual = new ArtDmxPackage(1, 1, 2, 2, 3, 3, 4, 4);
            var actualBytes = actual.GetBytes();

            // Assert
            Assert.AreEqual(0, actual.Universe);
            Assert.AreEqual(1, actualBytes[18]);
            Assert.AreEqual(2, actualBytes[19]);
            Assert.AreEqual(3, actualBytes[20]);
            Assert.AreEqual(4, actualBytes[21]);
        }

        [TestMethod()]
        public void ArtDmxPackageCtorPairs512Elements()
        {
            // Arrange
            var data = Enumerable.Range(1, 513).Select(i => (byte)i).ToArray();
            var pairs = Enumerable.Range(1, 513).Select(i => new AddressValuePair((short)i,(byte)i)).ToArray();

            // Act
            var actual = new ArtDmxPackage(pairs);
            var actualBytes = actual.GetBytes();

            // Assert
            Assert.AreEqual(512, actual.Length);
            Assert.AreEqual(0, actual.Universe);
            Assert.AreEqual(530, actualBytes.Length);
            for (int i = 18; i < 530; i++)
            {
                Assert.AreEqual(data[i - 18], actualBytes[i], "i = " + i);
            }
        }

        [TestMethod()]
        public void ArtDmxPackageCtorPairs0Elements()
        {
            // Arrange
            var pairs = new AddressValuePair[0];

            // Act
            var actual = new ArtDmxPackage(pairs);
            var actualBytes = actual.GetBytes();

            // Assert
            Assert.AreEqual(0, actual.Universe);
            Assert.AreEqual(0, actualBytes[18]);
            Assert.AreEqual(0, actualBytes[19]);
        }

        [TestMethod()]
        public void ArtDmxPackageCtorPairs513Elements()
        {
            // Arrange
            var data = Enumerable.Range(1, 513).Select(i => (byte)i).ToArray();
            var pairs = Enumerable.Range(1, 514).Select(i => new AddressValuePair((short)i, (byte)i)).ToArray();

            // Act
            var actual = new ArtDmxPackage(pairs);
            var actualBytes = actual.GetBytes();

            // Assert
            Assert.AreEqual(512, actual.Length);
            Assert.AreEqual(0, actual.Universe);
            Assert.AreEqual(530, actualBytes.Length);
            for (int i = 18; i < 530; i++)
            {
                Assert.AreEqual(data[i - 18], actualBytes[i], "i = " + i);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ArtDmxPackageCtorByteArrayThrowsNullException()
        {
            // Arrange
            byte[] pairs = null;

            // Act
            new ArtDmxPackage(pairs);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ArtDmxPackageCtorPairsThrowsNullException()
        {
            // Arrange
            AddressValuePair[] pairs = null;

            // Act
            new ArtDmxPackage(pairs);
        }
    }
}
