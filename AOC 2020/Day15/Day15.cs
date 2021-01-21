using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AOC_2020
{
    public class Day15
    {
        

        public void RunDay15Part1()
        {
            var input = new int[] { 15, 5, 1, 4, 7, 0 };

            var number = FindSpecificNumber(input, 2020);

            Console.WriteLine($"The 2020th number spoken is {number}");
        }

        public void RunDay15Part2()
        {
            var input = new int[] { 15, 5, 1, 4, 7, 0 };

            var number = FindSpecificNumber(input, 30000000);

            Console.WriteLine($"The 30000000th number spoken is {number}");
        }

        public int FindSpecificNumber(int[] input, int target)
        {
            var numberList = new Dictionary<int, int>();

            for(int i = 0; i < input.Length - 1; i++)
            {
                numberList.Add(input[i], i+1);
            }
            int lastNumber = input[input.Length - 1];

            for(int i = input.Length + 1; i <= target ; i++)
            {
                if (!numberList.ContainsKey(lastNumber))
                {
                    numberList[lastNumber] = i - 1;
                    lastNumber = 0;
                }
                else
                {
                    int newValue = i - 1 - numberList[lastNumber];
                    numberList[lastNumber] = i - 1;
                    lastNumber = newValue;
                } 
            }

            return lastNumber;

    }
    }
}
