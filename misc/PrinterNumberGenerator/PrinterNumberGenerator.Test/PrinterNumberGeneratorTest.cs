using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace PrinterNumberGenerator
{
    [TestFixture]
    public class PrinterNumberGeneratorTest
    {
        [Test]
        [ExpectedException(ExceptionType = typeof(ArgumentException))]
        public void LongMinusOneTest()
        {
            long minusOneLong = -1;
            FourSlotPrinterNumber minusOne = new FourSlotPrinterNumber(minusOneLong);
        }

        [Test]
        [ExpectedException(ExceptionType = typeof(ArgumentException))]
        public void LongMinusMaxTest()
        {
            long minusMaxLong = long.MinValue;
            FourSlotPrinterNumber minusOne = new FourSlotPrinterNumber(minusMaxLong);
        }

        [Test]
        public void LongZeroTest()
        {
            long zeroLong = 0;
            string zeroString = "1000";
            FourSlotPrinterNumber zero = new FourSlotPrinterNumber(zeroLong);
            Assert.That(zero.ToLong(), Is.EqualTo(zeroLong));
            Assert.That(zero.ToString(), Is.EqualTo(zeroString));
        }

        [Test]
        public void LongOneTest()
        {
            long oneLong = 1;
            string oneString = "1001";
            FourSlotPrinterNumber zero = new FourSlotPrinterNumber(oneLong);
            Assert.That(zero.ToLong(), Is.EqualTo(oneLong));
            Assert.That(zero.ToString(), Is.EqualTo(oneString));
        }

        [Test]
        public void LongNineTest()
        {
            long nineLong = 9;
            string nineString = "1009";
            FourSlotPrinterNumber zero = new FourSlotPrinterNumber(nineLong);
            Assert.That(zero.ToLong(), Is.EqualTo(nineLong));
            Assert.That(zero.ToString(), Is.EqualTo(nineString));
        }

        [Test]
        public void ConvertToCharTest()
        {
            Assert.That(FourSlotPrinterNumber.ConvertToChar(0), Is.EqualTo('0'));
            Assert.That(FourSlotPrinterNumber.ConvertToChar(9), Is.EqualTo('9'));
            Assert.That(FourSlotPrinterNumber.ConvertToChar(10), Is.EqualTo('a'));
            Assert.That(FourSlotPrinterNumber.ConvertToChar(35), Is.EqualTo('z'));
        }

        [Test]
        public void ConvertToIntTest()
        {
            Assert.That(FourSlotPrinterNumber.ConvertToInt('0'), Is.EqualTo(0));
            Assert.That(FourSlotPrinterNumber.ConvertToInt('9'), Is.EqualTo(9));
            Assert.That(FourSlotPrinterNumber.ConvertToInt('a'), Is.EqualTo(10));
            Assert.That(FourSlotPrinterNumber.ConvertToInt('z'), Is.EqualTo(35));
        }

        [Test]
        public void ConvertToIntAndBackTest()
        {
            for (int i = 0; i < 36; i++)
            {
                Assert.That(FourSlotPrinterNumber.ConvertToInt(FourSlotPrinterNumber.ConvertToChar(i)), Is.EqualTo(i));
            }
        }

        [Test]
        public void DefaultBase36Test()
        {
            Assert.That(FourSlotPrinterNumber.ConvertToBase36(0), Is.EqualTo("0"));
            Assert.That(FourSlotPrinterNumber.ConvertToBase36(9), Is.EqualTo("9"));
            Assert.That(FourSlotPrinterNumber.ConvertToBase36(10), Is.EqualTo("a"));
            Assert.That(FourSlotPrinterNumber.ConvertToBase36(35), Is.EqualTo("z"));
            Assert.That(FourSlotPrinterNumber.ConvertToBase36(36), Is.EqualTo("10"));
        }
    }
}
