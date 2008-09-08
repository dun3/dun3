using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Com.Hertkorn.WakeOnLan.DO
{
    [TestFixture]
    public class MacAddressTest
    {
        private static readonly string ADDRESS_MINUS_FORMAT = "00-30-05-FD-8D-C1";
        private static readonly string ADDRESS_COLON_FORMAT = "00:30:05:FD:8D:C1";
        private static readonly byte[] ADDRESS = new byte[] { 0, 48, 5, 253, 141, 193 };

        [Test]
        public void StringConvertMinusFormatTest()
        {
            MacAddress addressMinus = new MacAddress(ADDRESS_MINUS_FORMAT);

            Assert.That(addressMinus.Address[0], Is.EqualTo(ADDRESS[0]));
            Assert.That(addressMinus.Address[1], Is.EqualTo(ADDRESS[1]));
            Assert.That(addressMinus.Address[2], Is.EqualTo(ADDRESS[2]));
            Assert.That(addressMinus.Address[3], Is.EqualTo(ADDRESS[3]));
            Assert.That(addressMinus.Address[4], Is.EqualTo(ADDRESS[4]));
            Assert.That(addressMinus.Address[5], Is.EqualTo(ADDRESS[5]));
        }

        [Test]
        public void StringConvertColonFormatTest()
        {
            MacAddress addressColon = new MacAddress(ADDRESS_COLON_FORMAT);

            Assert.That(addressColon.Address[0], Is.EqualTo(ADDRESS[0]));
            Assert.That(addressColon.Address[1], Is.EqualTo(ADDRESS[1]));
            Assert.That(addressColon.Address[2], Is.EqualTo(ADDRESS[2]));
            Assert.That(addressColon.Address[3], Is.EqualTo(ADDRESS[3]));
            Assert.That(addressColon.Address[4], Is.EqualTo(ADDRESS[4]));
            Assert.That(addressColon.Address[5], Is.EqualTo(ADDRESS[5]));
        }

        [Test]
        public void ToStringTest()
        {
            MacAddress address = new MacAddress(ADDRESS_MINUS_FORMAT);

            Assert.That(address.ToString(), Is.EqualTo(ADDRESS_MINUS_FORMAT));
        }
    }
}
