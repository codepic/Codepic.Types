using System;
using System.Diagnostics;
using Codepic.Types;

namespace ConsoleApplication4
{
    static class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random(int.MinValue);

            BinarySeries series = new BinarySeries(0);

            Stopwatch watch = new Stopwatch();
            watch.Start();
            int i = 0;
            while (true)
            {
                bool next = random.Next(0, 2) == 1;
                series.Push(next);
                if (watch.ElapsedMilliseconds >= 1000)
                {
                    Console.WriteLine($"{series}\t({series.Value.ToString().PadLeft(ulong.MaxValue.ToString().Length, ' ')})\t{i.ToString().PadLeft(10, ' ')}");
                    watch.Restart();
                    i = 0;
                }
                i++;
            }
        }
    }
}
