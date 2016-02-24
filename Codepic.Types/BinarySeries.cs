using System;
using System.Diagnostics.Contracts;

namespace Codepic.Types
{
    /// <summary>
    /// Manages a 64-bit series
    /// </summary>
    public struct BinarySeries
    {
        private ulong _value;

        /// <summary>
        /// Instantiates a new series with an unsigned long value
        /// </summary>
        /// <param name="initialValue"></param>
        public BinarySeries(ulong initialValue)
        {
            _value = initialValue;
        }

        /// <summary>
        /// Instantiates a new series with a long value
        /// </summary>
        /// <param name="initialValue"></param>
        public BinarySeries(long initialValue)
        {
            _value = (ulong)initialValue;
        }

        /// <summary>
        /// Returns the undelying unsigned long value of the seriesss
        /// </summary>
        public ulong Value => _value;
        /// <summary>
        /// Gets the minimum value.
        /// </summary>
        /// <value>
        /// The minimum value.
        /// </value>
        public static ulong MinValue => ulong.MinValue;
        /// <summary>
        /// Gets the maximum value.
        /// </summary>
        /// <value>
        /// The maximum value.
        /// </value>
        public static ulong MaxValue => ulong.MaxValue;
        /// <summary>
        /// Pushes the specified boolean into the end of the series while shifting the other values left.
        /// </summary>
        /// <param name="truthy">true indicates that the pushed bit is on (1) and false indicates the bit is off (0).</param>
        public void Push(bool truthy)
        {
            _value = (_value << 1);
            _value |= (truthy ? (ulong)1 : 0);
        }

        /// <summary>
        /// Converts the value to binary string representationss
        /// </summary>
        /// <returns>Value as binary strings</returns>
        public override string ToString()
        {
            return Convert.ToString((long)_value, 2).PadLeft(64, '0');
        }

        /// <summary>
        /// Shifts the bits left and sets the lowest order bit to 0
        /// </summary>
        /// <param name="bitsToShift">The bits to shift.</param>
        public void LeftShift(int bitsToShift)
        {
            _value = _value << bitsToShift;
        }
        /// <summary>
        /// Shifts the bits right and sets the highest order bit to 0
        /// </summary>
        /// <param name="bitsToShift">The bits to shift.</param>
        public void RightShift(int bitsToShift)
        {
            _value = _value >> bitsToShift;
        }

        #region Operator Overloading

        /// <summary>
        /// Implicitly converts the Binary Series to unsigned long value
        /// </summary>
        /// <param name="value">Binary series value</param>
        /// <returns>Unsigned long</returns>
        public static implicit operator ulong(BinarySeries value)
        {
            return value.Value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator long(BinarySeries value)
        {
            Contract.Requires(value.Value <= long.MaxValue);
            return (long)value.Value;
        }

        /// <summary>
        /// Implicitly converts the binary series to an integer
        /// </summary>
        /// <param name="value">Binary series value</param>
        /// <returns>value as integer</returns>
        public static implicit operator int(BinarySeries value)
        {
            return (int)value.Value;
        }

        /// <summary>
        /// Implicitly converts an ulong value to binary series
        /// </summary>
        /// <param name="value">Unsigned long to convert</param>
        /// <returns>A new binary series</returns>
        public static implicit operator BinarySeries(ulong value)
        {
            return new BinarySeries(value);
        }

        /// <summary>
        /// Shifts first operand's bits left by the number specified by the second operand
        /// </summary>
        /// <param name="a">First operand</param>
        /// <param name="bitsToShift">Number of places to shift</param>
        /// <returns></returns>
        public static BinarySeries operator <<(BinarySeries a, int bitsToShift)
        {
            a.LeftShift(bitsToShift);
            return a;
        }
        /// <summary>
        /// Shifts first operand's bits right by the number specified by the second operand
        /// </summary>
        /// <param name="a">First operand</param>
        /// <param name="bitsToShift">Number of places to shift</param>
        /// <returns></returns>
        public static BinarySeries operator >>(BinarySeries a, int bitsToShift)
        {
            a.RightShift(bitsToShift);
            return a;
        }
        #endregion

        /// <summary>
        /// Creates an binary series from unsigned long value
        /// </summary>
        /// <param name="initialValue">The value to create a series from</param>
        /// <returns>Binary series</returns>
        public static BinarySeries FromUlong(ulong initialValue)
        {
            return new BinarySeries(initialValue);
        }

        /// <summary>
        /// Initializes a new binary series from a string
        /// </summary>
        /// <param name="binarySeries">Series of 0 and 1 as string passed in</param>
        /// <returns></returns>
        public static BinarySeries FromString(string binarySeries)
        {
            binarySeries = binarySeries.PadLeft(64, '0');
            BinarySeries series = new BinarySeries();
            foreach (char c in binarySeries)
                series.Push(c == '1');
            return series;
        }

        /// <summary>
        /// Checks how many consequtive bits counted from least significant bit are set
        /// </summary>
        /// <returns>Number of consequtive bits set from right</returns>
        public int SetSize()
        {
            Contract.Ensures(Contract.Result<int>() <= 64);
            Contract.Ensures(Contract.Result<int>() >= 0);
            const ulong one = 1;
            for (int i = 0; i < 64; i++)
            {
                if((this & one << i) == 0)
                    return i;
            }
            return 64;
        }

        /// <summary>
        /// Checks whether a bit in given position is set (1) or not (0)
        /// </summary>
        /// <param name="pos">zero based position counted from least significant bit</param>
        /// <returns>True if the given bit is set(1)</returns>
        public bool IsBitSet(int pos)
        {
            long one = 1;
            return (this & (one << pos)) != 0;
        }

        /// <summary>
        /// Initializes a new BinarySeries structure containing the bits represented by a long value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static long FromLong(long value)
        {
            return new BinarySeries(value);
        }
    }
}
