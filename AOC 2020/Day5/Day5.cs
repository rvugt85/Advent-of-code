using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AOC_2020
{
    public class Day5
    {
        static string path = "\\AOC 2020\\Day5\\InputDay5.txt";

        private MainProgram mainProgram = new MainProgram();

        public void RunDay5Part1()
        {
            var seatcodes = mainProgram.ConvertFileToLines(path);

            int answer = GetHighestSeatId(seatcodes);

            Console.WriteLine($"The highest seat ID = {answer}");
        }

        public void RunDay5Part2()
        {
            var seatcodes = mainProgram.ConvertFileToLines(path);

            var possibleSeats = Enumerable.Range(0, GetHighestSeatId(seatcodes)).ToList(); 

            foreach (var seatcode in seatcodes)
            {
                possibleSeats.Remove(CreateSeatId(seatcode));
            }

            foreach(var possibleSeat in possibleSeats)
            {
                if (!possibleSeats.Contains(possibleSeat - 1) && !possibleSeats.Contains(possibleSeat + 1))
                {
                    Console.WriteLine($"Your seat is {possibleSeat}");
                    return;
                }
            }
            

            


        }

        public int GetHighestSeatId(List<string> seatcodes)
        {
            List<int> seatIds = new List<int>();
            
            foreach(var code in seatcodes)
            {
                seatIds.Add(CreateSeatId(code));
            }

            return seatIds.Max();
        }

        public int CreateSeatId(string code)
        { 
            var binaryCode = Regex.Replace(code, @"[F,L]", "0");
            binaryCode = Regex.Replace(binaryCode, @"[B,R]", "1");

            var row = CreateNumber(binaryCode.Substring(0, 7));
            var seat = CreateNumber(binaryCode.Substring(7, 3));

            return (row * 8) + seat;
        }

        public int CreateNumber(string binaryCode)
        {
            var possibilities = Convert.ToInt32(Math.Pow(2.00, Convert.ToDouble(binaryCode.Length)));
            var possibleNumbers = Enumerable.Range(0, possibilities).ToList();
            
            var binaryCodeSeparated = binaryCode.ToCharArray();

            foreach(var character in binaryCodeSeparated)
            {
                int splitPosition = possibilities / 2;

                if (character == '0')
                {
                    possibleNumbers.RemoveRange(splitPosition, splitPosition);
                }
                else if (character == '1')
                {
                    possibleNumbers.RemoveRange(0, splitPosition);
                }
                else
                {
                    throw new ArgumentException($"Something went wrong, {character} is not a binary code");
                }
                possibilities = possibilities / 2;
            }

            if(possibleNumbers.Count == 1)
            {
                return possibleNumbers.First();
            }
            else
            {
                throw new ArgumentException("There are still multiple numbers left");
            }
        }
    }
}
