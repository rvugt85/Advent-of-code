using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AOC_2020
{
    public class Day9
    {
        public string path = "\\AOC 2020\\Day9\\InputDay9.txt";

        public MainProgram mainProgram = new MainProgram();

        public void RunDay9Part1()
        {
            var lines = mainProgram.ConvertFileToLines(path);

            var longArray = mainProgram.ConvertLinesToLongArray(lines);

            var answer = GetFirstInvalidNumber(longArray, 25);

            Console.WriteLine($"The first number without the preamble property is {answer}");
        }

        public void RunDay9Part2()
        {
            var lines = mainProgram.ConvertFileToLines(path);

            var longArray = mainProgram.ConvertLinesToLongArray(lines);

            var answer = GetFirstAndLastNumberOfContiguousRange(longArray, 105950735);

            Console.WriteLine($"The sum of the first and last number of the contiguous set is {answer}");
        }

        public long GetFirstInvalidNumber(long[] longArray, int preamble)
        {
            bool numberFound = false;

            for(int i=preamble; !numberFound; i++)
            {
                var numberToCheckIsValid = false;
                var numberToCheck = longArray[i];
                var listForSum = longArray.Where((n, index) => (index >= i - preamble) && (index < i)).ToList();
                for (var j = 0; j < listForSum.Count; j++)
                {
                    var numberNeededInPreamble = numberToCheck - listForSum[j];
                    
                    if(listForSum.Contains(numberNeededInPreamble) && numberNeededInPreamble != listForSum[j])
                    {
                        numberToCheckIsValid = true;
                    }
                }

                if( numberToCheckIsValid == false)
                {
                    return numberToCheck;
                }
                
            }

            throw new ArgumentException("No result reached!");
        }

        public long GetFirstAndLastNumberOfContiguousRange(long[] lines, long totalNeeded)
        {
            for(int i = 0; i < lines.Length; i++)
            {
                long total = 0;
                var range = new List<long>();
                for (int j = i; total < totalNeeded; j++)
                {
                    var current = lines[j];
                    total += current;
                    range.Add(current);
                    
                }
                if(total == totalNeeded)
                {
                    return range.Max() + range.Min();
                }
            }

            throw new ArgumentException("Something went wrong");
        }
    }
}
