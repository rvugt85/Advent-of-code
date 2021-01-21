using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AOC_2020
{
    public class Day13
    {
        public List<string> Buses;

        public Day13()
        {
            var calibrationData = new Dictionary<int, int> { { 7, 945 }, { 13, 949 }, { 59, 944 }, { 31, 930 }, { 19, 931 } };

            var realData = "17,x,x,x,x,x,x,x,x,x,x,37,x,x,x,x,x,907,x,x,x,x,x,x,x,x,x,x,x,19,x,x,x,x,x,x,x,x,x,x,23,x,x,x,x,x,29,x,653,x,x,x,x,x,x,x,x,x,41,x,x,13";
            Buses = realData.Split(',').ToList();

            var zeroPoint = CalculateZeroPoint(calibrationData);
        }

        private int CalculateZeroPoint(Dictionary<int, int> calibrationData)
        {
            while (calibrationData.Where(x => x.Value == calibrationData.Values.Max()).Count() != calibrationData.Count())
            {
                var maxValue = calibrationData.Values.Max();
                var keyForMaxValue = calibrationData.Where(x => x.Value == maxValue).First().Key;
                calibrationData[keyForMaxValue] = maxValue - keyForMaxValue;
            }

            return calibrationData.Values.Max();
        }

        public void RunDay13Part1()
        {
            var realTime = 1000186;

            var answer = IdOfEarliestBusMultipliedByWaitingMinutes(realTime, Buses);

            Console.WriteLine($"The id of the bus multiplied by the number of minutes to wait is {answer}");
        }

        public void RunDay13Part2()
        {
            long answer = CheckSpecificTimeOfBus(Buses);

            Console.WriteLine($"The earliest timestamp of all the listed buses in order is {answer}");
        }

        public long CheckSpecificTimeOfBus(List<string> buses)
        {
            var buslines = new List<long>();
            var minutes = new List<long>();

            foreach (var bus in buses)
            {
                if(bus != "x")
                {
                    var busline = int.Parse(bus);
                    buslines.Add(busline);
                    minutes.Add(busline - buses.IndexOf(bus));
                }
            }
            var answer = SolveChineseRemainderTheorem(buslines.ToArray(), minutes.ToArray());
            return answer;
        }

        public long SolveChineseRemainderTheorem(long[] n, long[] a)
        {
            long prod = n.Aggregate((long)1, (i, j) => i * j);
            long p;
            long sm = 0;
            for (long i = 0; i < n.Length; i++)
            {
                p = prod / n[i];
                sm += a[i] * ModularMultiplicativeInverse(p, n[i]) * p;
            }
            return sm % prod;
        }

        private long ModularMultiplicativeInverse(long a, long mod)
        {
            long b = a % mod;
            for (long x = 1; x < mod; x++)
            {
                if ((b * x) % mod == 1)
                {
                    return x;
                }
            }
            return 1;
        }

        public int IdOfEarliestBusMultipliedByWaitingMinutes(int realTime, List<string> buses)
        {
            int closestTime = 0;
            int busnumber = 0;
            List<int> realBuses = new List<int>();

            foreach (var bus in buses)
            {
                if (int.TryParse(bus, out var realBus))
                {
                    realBuses.Add(realBus);
                }
            }

            foreach (var bus in realBuses)
            {
                int timeCurrentBus = 0;
                while (timeCurrentBus < realTime)
                {
                    timeCurrentBus += bus;
                }

                if (closestTime == 0 || timeCurrentBus < closestTime)
                {
                    closestTime = timeCurrentBus;
                    busnumber = bus;
                }
            }

            return busnumber * (closestTime - realTime);
        }


    }
}
