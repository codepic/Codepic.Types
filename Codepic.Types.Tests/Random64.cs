using System;

namespace Codepic.Types.Tests
{
    public class Random64
    {
        private readonly Random _random;

        public Random64(Random random)
        {
            _random = random;
        }

        public ulong Next()
        {
            return Next(ulong.MaxValue);
        }

        public ulong Next(ulong maxValue)
        {
            return Next(0, maxValue);
        }

        public ulong Next(ulong minValue, ulong maxValue)
        {
            if (maxValue < minValue)
                throw new ArgumentException();

            return (ulong)(_random.NextDouble() * (maxValue - minValue)) + minValue;
        }
    }
}
