using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AOC_2020
{
    public class Day10
    {
        private string path = "\\AOC 2020\\Day10\\InputDay10.txt";

        private MainProgram mainProgram = new MainProgram();

        private Dictionary<int, long> cache = new Dictionary<int, long>();

        public void RunDay10Part1()
        {
            var numbers = mainProgram.MakeSortedListFromFile(path);

            numbers.Add(numbers.Max() + 3);

            var combinations = GetCombinationBetween1And3JoltDifferences(numbers);

            Console.WriteLine($"The multiplication of the 1-jolt differences with the 3-jolt differences is {combinations}");
        }

        public void RunDay10Part2()
        {
            var numbers = mainProgram.MakeSortedListFromFile(path);

            numbers.Add(numbers.Max() + 3);

            var ways = GetPossibleWayToArrange(numbers, -1, 0, 0);

            Console.WriteLine($"There are {ways} ways to combine these adapters");
        }

        public int GetCombinationBetween1And3JoltDifferences(List<int> adapters)
        {
            var current = 0;
            var oneJoltDifference = 0;
            var twoJoltDifference = 0;
            var threeJoltDifference = 0;

            for (int i = 0; i < adapters.Count; i++)
            {
                if (adapters[i] == current + 1)
                {
                    oneJoltDifference++;
                }
                else if (adapters[i] == current + 3)
                {
                    threeJoltDifference++;
                }
                else if (adapters[i] == current + 2)
                {
                    twoJoltDifference++;
                }

                else
                {
                    throw new ArgumentException("Something went wrong!");
                }

                current = adapters[i];
            }

            return oneJoltDifference * threeJoltDifference;
        }

        public long GetPossibleWayToArrange(List<int> adapters, int index, long current, long countOfOptions)
        {
            if (!cache.ContainsKey(index))
            {
                var extra = 3;
                if (index >= adapters.Count() - 3)
                {
                    extra = adapters.Count() - adapters.IndexOf(adapters.Max());
                }

                var range = adapters.GetRange(index + 1, extra);

                foreach (var possibleConnection in range)
                {
                    var currentIndex = adapters.IndexOf(possibleConnection);


                    if (possibleConnection <= current + 3)
                    {
                        if (possibleConnection == adapters.Max())
                        {
                            countOfOptions++;
                        }
                        else
                        {
                            var possibleConnections = GetPossibleWayToArrange(adapters, currentIndex, possibleConnection, countOfOptions);
                            countOfOptions += possibleConnections;
                        }
                    }
                }
                cache[index] = countOfOptions;
            }
            return cache[index];
        }
    }
}
