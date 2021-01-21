using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AOC_2020
{
    public class Day1
    {
        List<int> numbers = new List<int>();
        bool answered = false;
        string path = "\\AOC 2020\\Day1\\InputDay1.txt";

        private MainProgram mainProgram = new MainProgram();

        public void RunDay1()
        {
            var numbers = mainProgram.MakeSortedListFromFile(path);

            while (!answered)
            {
                var a = numbers.First();
                numbers.Remove(a);
                foreach (var b in numbers)
                {
                    if (answered)
                        break;
                    var numbersExcludedB = numbers.ToList();
                    numbersExcludedB.Remove(b);
                    if (a + b > 2020)
                    {
                        // Console.WriteLine($"2 numbers already higher than 2020 {a} + {b} = {a + b}");
                        break;
                    }
                    foreach (var c in numbersExcludedB)
                    {
                        var solution = a + b + c;
                        if (solution == 2020)
                        {
                            Console.WriteLine($"The correct answer is {a} + {b} + {c} = {solution}");
                            Console.WriteLine($"Multiplication gives {a} x {b} x {c} = {a * b * c}");
                            answered = true;
                            break;
                        }
                        else if (solution > 2020)
                        {
                            // Console.WriteLine($"The wrong answer is {a} + {b} + {c} = {solution}. Continue to next number");
                            break;
                        }
                        else
                        {
                            // Console.WriteLine($"The wrong answer is {a} + {b} + {c} = {solution}. Try again");
                        }
                    }
                }

            }
        }
    }
}
