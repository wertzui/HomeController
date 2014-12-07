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
    public class ByteHelperTests
    {
        [TestMethod()]
        public void LoByteWithZero()
        {
            // Arrange
            var value = (short)0;
            var expected = (byte)0;

            // Act
            var actual = ByteHelper.LoByte(value);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void LoByteWithOne()
        {
            // Arrange
            var value = (short)1;
            var expected = (byte)1;

            // Act
            var actual = ByteHelper.LoByte(value);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void LoByteWithByteMaxValue()
        {
            // Arrange
            var value = (short)byte.MaxValue;
            var expected = (byte)byte.MaxValue;

            // Act
            var actual = ByteHelper.LoByte(value);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void LoByteWithByteMaxValuePlus1()
        {
            // Arrange
            var value = (short)(byte.MaxValue + 1);
            var expected = (byte)0;

            // Act
            var actual = ByteHelper.LoByte(value);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void LoByteWithShortMaxValue()
        {
            // Arrange
            var value = (short)short.MaxValue;
            var expected = (byte)byte.MaxValue;

            // Act
            var actual = ByteHelper.LoByte(value);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void HiByteWithZero()
        {
            // Arrange
            var value = (short)0;
            var expected = (byte)0;

            // Act
            var actual = ByteHelper.HiByte(value);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void HiByteWithOne()
        {
            // Arrange
            var value = (short)1;
            var expected = (byte)0;

            // Act
            var actual = ByteHelper.HiByte(value);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void HiByteWithByteMaxValue()
        {
            // Arrange
            var value = (short)byte.MaxValue;
            var expected = (byte)0;

            // Act
            var actual = ByteHelper.HiByte(value);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void HiByteWithByteMaxValuePlus1()
        {
            // Arrange
            var value = (short)(byte.MaxValue + 1);
            var expected = (byte)1;

            // Act
            var actual = ByteHelper.HiByte(value);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void HiByteWithShortMaxValue()
        {
            // Arrange
            var value = (short)short.MaxValue;
            var expected = (byte)(byte.MaxValue / 2);

            // Act
            var actual = ByteHelper.HiByte(value);

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
