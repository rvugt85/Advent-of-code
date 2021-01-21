using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC_2020
{
    public class Day23
    {
        public void RunDay23Part1()
        {
            string input = "653427918";

            var cupOrder = PlayRounds(input, 9, 100);

            var result = GetCupOrderTotal(cupOrder);

            Console.WriteLine($"The labels on the cups after cup 1 are {result}");
        }

        public string GetCupOrderTotal(Dictionary<int, int> cupOrder)
        {
            string result = string.Empty;
            var previousResult = 1;
            for (int i = 1; i <= cupOrder.Count - 1; i++)
            {
                result = string.Concat(result, cupOrder[previousResult]);
                previousResult = cupOrder[previousResult];
            }

            return result;
        }

        public void RunDay23Part2()
        {
            string input = "653427918";

            var cupOrder = PlayRounds(input, 1000000, 10000000);

            var result = (long)cupOrder[1] * (long)cupOrder[cupOrder[1]];

            Console.WriteLine($"The 2 labels after 1 mulitplied leads to {result}");
        }

        public Dictionary<int,int> PlayRounds(string input, int length, int roundCounts)
        {
            var currentCupValue = int.Parse(input[0].ToString());
            var dictionary = TransformInput(input, length);
            string result = string.Empty;

            var destinationCupValue = 0;

            for (int i = 0; i < roundCounts; i++)
            {
                // Removed cups
                List<int> removedCups = new List<int>();
                var cupRemoved = currentCupValue;
                for (int j = 1; j <= 3; j++)
                {
                    var removedCupValue = dictionary[cupRemoved];
                    removedCups.Add(removedCupValue);
                    cupRemoved = removedCupValue;
                }

                var x = 1;

                while (removedCups.Contains(currentCupValue - x))
                    x++;
                if (currentCupValue - x != 0)
                {
                    destinationCupValue = currentCupValue - x;
                }
                else
                {
                    var y = 0;
                    while (removedCups.Contains(dictionary.Keys.Max() - y))
                        y++;
                    destinationCupValue = dictionary.Keys.Max() - y;
                }

                dictionary[currentCupValue] = dictionary[removedCups.Last()];

                // Add cups
                dictionary[removedCups.Last()] = dictionary[destinationCupValue];
                dictionary[destinationCupValue] = removedCups.First();

                currentCupValue = dictionary[currentCupValue];
            }
  
            return dictionary;
        }

        private Dictionary<int, int> TransformInput(string input, int length)
        {
            var nextValues = new Dictionary<int, int>();

            for(int i = 0; i <= input.Length - 1; i++)
            {
                if (i == input.Length - 1)
                    nextValues.Add(int.Parse(input[i].ToString()), int.Parse(input[0].ToString()));
                else
                {
                    nextValues.Add(int.Parse(input[i].ToString()), int.Parse(input[i + 1].ToString()));
                }
                
            }
            if (nextValues.Count < length)
            {
                nextValues[int.Parse(input.Last().ToString())] = nextValues.Count + 1;
                for(int i = nextValues.Count + 1; i < length; i++)
                {
                    nextValues.Add(i, i + 1);
                }
                nextValues.Add(nextValues.Values.Max(), int.Parse(input[0].ToString()));
            }
            var test = nextValues.Values.Max();
            var test2 = nextValues.Keys.Max();

            return nextValues;
        }
    }
}
