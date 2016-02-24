using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Codepic.Types.Tests
{
    [TestClass]
    public class BinarySeriesTests
    {
        private static TestContext _context;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            _context = context;
        }

        [TestMethod]
        public void ShouldShiftLeftWithOperator()
        {
            BinarySeries actual = new BinarySeries(1);
            actual = actual << 1;
            Assert.AreEqual(2, actual);
            actual = actual << 1;
            Assert.AreEqual(4, actual);
        }

        [TestMethod]
        public void ShouldShiftRightWithOperator()
        {
            BinarySeries value = new BinarySeries(4);
            value = value >> 1;
            Assert.AreEqual(2, value);
            value = value >> 1;
            Assert.AreEqual(1, value);
        }

        [TestMethod]
        public void ShouldShiftLeftWithMethod()
        {
            BinarySeries actual = new BinarySeries(1);
            actual.LeftShift(1);
            Assert.AreEqual(2, actual);
            actual.LeftShift(1);
            Assert.AreEqual(4, actual);
        }

        [TestMethod]
        public void ShouldShiftRightWithMethod()
        {
            BinarySeries actual = new BinarySeries(4);
            actual.RightShift(1);
            Assert.AreEqual(2, actual);
            actual.RightShift(1);
            Assert.AreEqual(1, actual);
        }
        
        [TestMethod]
        public void ShouldCastToInt32()
        {
            int expected = 2;
            int actual = new BinarySeries(2);
            Assert.AreEqual(expected, actual, "BLong should implicitly cast to Int32");
        }
        [TestMethod]
        public void ShouldCastToUlong()
        {
            ulong expected = BinarySeries.MaxValue;
            ulong actual = new BinarySeries(BinarySeries.MaxValue);
            Assert.AreEqual(expected, actual, "BLong should implicitly cast to Int32");
        }
        [TestMethod]
        public void ShouldCastToLong()
        {
            long expected = long.MaxValue;
            long actual = BinarySeries.FromLong(long.MaxValue);
            Assert.AreEqual(expected, actual, "BLong should implicitly cast to Int32");
        }

        [TestMethod]
        public void ShouldConstructFromLong()
        {
            string expected = "0000000000000000000000000000000000000000000000000000000000000001";
            Assert.AreEqual(64, expected.Length);
            long longValue = 1;
            BinarySeries original = new BinarySeries(longValue);
            string actual = original.ToString();
            Assert.AreEqual(expected, actual, "BLong should implicitly cast to string");
        }

        [TestMethod]
        public void ShouldImplicitCastToString()
        {
            string expected = "0000000000000000000000000000000000000000000000000000000000000001";
            Assert.AreEqual(64, expected.Length);
            BinarySeries original = new BinarySeries(1);

            string actual = original.ToString();
            Assert.AreEqual(expected, actual, "BLong should implicitly cast to string");

            original.LeftShift(1);
            actual = original.ToString();
            expected = "0000000000000000000000000000000000000000000000000000000000000010";
            Assert.AreEqual(64, expected.Length);
            Assert.AreEqual(expected, actual, "BLong should implicitly cast to string");

            original.LeftShift(1);
            actual = original.ToString();
            expected = "0000000000000000000000000000000000000000000000000000000000000100";
            Assert.AreEqual(64, expected.Length);
            Assert.AreEqual(expected, actual, "BLong should implicitly cast to string");
        }

        [TestMethod]
        public void ShouldReturnCorrectMaxValue()
        {
            Assert.AreEqual(ulong.MinValue, BinarySeries.MinValue);
            Assert.AreEqual(ulong.MaxValue, BinarySeries.MaxValue);
        }

        [TestMethod]
        public void ShouldReturnCorrectNumber()
        {
            BinarySeries actual = ulong.MaxValue;
            Assert.AreEqual(ulong.MaxValue, actual.Value);
            actual = ulong.MinValue;
            Assert.AreEqual(ulong.MinValue, actual.Value);
        }

        [TestMethod]
        public void ShouldReturnSetSize()
        {
            int expected = 3;
            BinarySeries series = new BinarySeries(7);
            int actual = series.SetSize();
            _context.WriteLine(series.ToString());
            Assert.AreEqual(expected, actual, $"Should return growth size of {expected}");

            expected = 10;

            series = new BinarySeries(1023);
            actual = series.SetSize();
            _context.WriteLine(series.ToString());
            Assert.AreEqual(expected, actual, $"Should return growth size of {expected}");

            expected = 63;
            series = new BinarySeries(9223372036854775807);
            actual = series.SetSize();
            _context.WriteLine(series.ToString());
            Assert.AreEqual(expected, actual, $"Should return growth size of {expected}");

            expected = 64;
            series = new BinarySeries(18446744073709551615);
            actual = series.SetSize();
            _context.WriteLine(series.ToString());
            Assert.AreEqual(expected, actual, $"Should return growth size of {expected}");

            expected = 3;
            series = new BinarySeries(1234567);
            actual = series.SetSize();
            _context.WriteLine(series.ToString());
            Assert.AreEqual(expected, actual, $"Should return growth size of {expected}");
        }

        [TestMethod]
        public void ShouldCreateFromString()
        {
            string initial = "001";
            BinarySeries original = BinarySeries.FromString(initial);
            Assert.AreEqual("0000000000000000000000000000000000000000000000000000000000000001", original.ToString());
        }
        [TestMethod]
        public void ShouldIgnoreIncompatibleCharacters()
        {
            string initial = "I am number 1";
            BinarySeries original = BinarySeries.FromString(initial);
            Assert.AreEqual("0000000000000000000000000000000000000000000000000000000000000001", original.ToString());
        }

        [TestMethod]
        public void ShouldCreateFromUlong()
        {
            var original = BinarySeries.FromUlong(ulong.MaxValue);
            ulong expected = ulong.MaxValue;

            Assert.AreEqual(expected, original.Value);
        }

        [TestMethod]
        public void ShouldReturnBitSet()
        {
            BinarySeries original = BinarySeries.FromUlong(355436500265);
            string initial = original.ToString();

            _context.WriteLine(initial);

            for (int i = 0; i < initial.Length; i++)
            {
                char elementAt = initial.Reverse().ElementAt(i);
                var expected = elementAt == '1';
                var actual = original.IsBitSet(i);
                _context.WriteLine($"{i} : {elementAt} - {expected} == {actual}");
                Assert.AreEqual(expected,actual);
            }
        }

        [TestMethod]
        public void ShouldConvertBackAndForth()
        {
            Random random = new Random(int.MaxValue);
            Random64 random64 = new Random64(random);
            for (ulong i = 0; i < 10000; i++)
            {
                ulong expected = random64.Next();
                ulong actual = BinarySeries.FromUlong(expected);
                Assert.AreEqual(expected, actual);
            }
        }

    }
}
