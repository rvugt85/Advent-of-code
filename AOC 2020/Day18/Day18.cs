using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC_2020
{
    public class Day18
    {
        MainProgram mainProgram = new MainProgram();

        private int locationInString;
        private List<string> Lines;

        public Day18()
        {
            Lines = mainProgram.ConvertFileToLines("\\AOC 2020\\Day18\\InputDay18.txt");
        }

        public void RunDay18Part1()
        {
            long total = 0;

            foreach(var line in Lines)
            {
                locationInString = 0;
                var lineWithoutSpaces = Regex.Replace(line, @"\s+", "");
                total += GetSumResultEasy(lineWithoutSpaces);
            }

            Console.WriteLine($"The sum of all the sums is {total}");
        }

        public long GetSumResultEasy(string line)
        {
            var stringLength = line.Length;
            var lineAsArray = line.ToCharArray();
            long result = 0;
            Operators op = Operators.None;

            for (int i = locationInString; locationInString < stringLength; locationInString++)
            {
                var value = line[locationInString].ToString();

                if (int.TryParse(value, out int number))
                {
                    result = ProcessOperator(result, op, number);
                }

                if (value == "*")
                    op = Operators.Multiplication;
                else if (value == "+")
                    op = Operators.Addition;
                else if (value == "(")
                {
                    locationInString++;
                    result = ProcessOperator(result, op, GetSumResultEasy(line));
                }
                else if (value == ")")
                    return result;
            }

            return result;
        }

        private long ProcessOperator(long result, Operators op, long number)
        {
            switch (op)
            {
                case Operators.None:
                    result = number;
                    break;
                case Operators.Multiplication:
                    result = result * number;
                    break;
                case Operators.Addition:
                    result = result + number;
                    break;
            }
            

            return result;
        }

        public enum Operators
        {
            None,
            Multiplication,
            Addition
        }

        public void RunDay18Part2()
        {
            long total = 0;

            foreach (var line in Lines)
            {
                //locationInString = 0;
                var lineWithoutSpaces = Regex.Replace(line, @"\s+", "");
                total += GetSumResultHard(lineWithoutSpaces, 0);
            }

            Console.WriteLine($"The sum of all the sums is {total}");
        }

        public long GetSumResultHard(string line, long result)
        {
            var stringLength = line.Length;
            var lineAsArray = line.ToCharArray();
            Operators op = Operators.None;

            for (int i = 0; i < stringLength; i++)
            {
                var value = line[i].ToString();

                if (int.TryParse(value, out int number))
                {
                    result = ProcessOperatorDifferentPrecedence(result, op, line.Substring(i+1), number);
                    if (op == Operators.Multiplication)
                        return result;
                }

                if (value == "*")
                    op = Operators.Multiplication;
                else if (value == "+")
                    op = Operators.Addition;
                else if (value == "(")
                {
                    string betweenBrackets;
                    int end = i + 1;
                    int nextOpening = i;

                    while (nextOpening < end && nextOpening != -1)
                    {
                        nextOpening = line.IndexOf('(', nextOpening + 1);
                        end = line.IndexOf(')', end + 1);
                    }
                    betweenBrackets = line.Substring(i + 1, end - i);
                    
                    var test = GetSumResultHard(betweenBrackets, 0);
                    if (op == Operators.Multiplication)
                    {
                        var followUp = GetSumResultHard(betweenBrackets, number);
                        result = result * GetSumResultHard(line.Substring(end + 1), followUp);
                        return result;
                    }
                    else
                    {
                        result = ProcessOperatorDifferentPrecedence(result, op, betweenBrackets, test);
                    }
                    i = end;
                }
                else if (value == ")")
                {
                    return result;
                }
            }
            return result;
        }

        private long ProcessOperatorDifferentPrecedence(long result, Operators op, string line, long number)
        {
            switch (op)
            {
                case Operators.None:
                    result = number;
                    break;
                case Operators.Multiplication:
                    var followUp = GetSumResultHard(line, number);
                    if (followUp == 0)
                    {
                        result = result * number;
                    }
                    else
                    {
                        result = result * followUp;
                    }
                    break;
                case Operators.Addition:
                    result = result + number;
                    break;
            }

            return result;
        }
    }              
}
