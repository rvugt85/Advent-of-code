using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC_2020
{
    public class MainProgram
    {
        private string BaseUrl = "C:\\Users\\Ruben\\Documents\\Git\\aoc\\2020\\Ruben\\AOC 2020";

        public static void Main()
        {
            //var day1 = new Day1();
            //day1.RunDay1();

            //var day2 = new Day2();
            //day2.RunDay2Part1();
            //day2.RunDay2Part2();

            //var day3 = new Day3();
            //day3.RunDay3Part1();
            //day3.RunDay3Part2();

            //var day4 = new Day4();
            //day4.RunDay4();

            //var day5 = new Day5();
            //day5.RunDay5Part1();
            //day5.RunDay5Part2();

            //var day6 = new Day6();
            //day6.RunDay6Part1();
            //day6.RunDay6Part2();

            //var day7 = new Day7();
            //day7.RunDay7Part1();
            //day7.RunDay7Part2();

            //var day8 = new Day8();
            //day8.RunDay8Part1();
            //day8.RunDay8Part2();

            //var day9 = new Day9();
            //day9.RunDay9Part1();
            //day9.RunDay9Part2();

            //var day10 = new Day10();
            //day10.RunDay10Part1();
            //day10.RunDay10Part2();

            //var day11 = new Day11("\\AOC 2020\\Day11\\InputDay11.txt");
            //day11.RunDay11Part1();
            //day11.RunDay11Part2();

            //var day12 = new Day12();
            //day12.RunDay12Part1();
            //day12.RunDay12Part2();

            //var day13 = new Day13();
            //day13.RunDay13Part1();
            //day13.RunDay13Part2();

            //var day14 = new Day14();
            //day14.RunDay14Part1();
            //day14.RunDay14Part2();

            //var day15 = new Day15();
            //day15.RunDay15Part1();
            //day15.RunDay15Part2();

            //var day16 = new Day16("\\AOC 2020\\Day16\\InputDay16.txt");
            //var scanningErrorRate = day16.RunDay16Part1();
            //Console.WriteLine($"The ticket scanning error rate is {scanningErrorRate}");

            //var fields = day16.RunDay16Part2("departure");
            //long answer = 0;
            //foreach(var importantField in fields.Where(f => f.Key.Contains("departure")))
            //{
            //    if (answer == 0)
            //        answer = importantField.Value;
            //    else
            //        answer = answer * importantField.Value;
            //}
            //Console.WriteLine($"The fields with the word departure multiplied is {answer}");

            //var day17 = new Day17("\\AOC 2020\\Day17\\InputDay17.txt");
            //Console.WriteLine($"The amount of active cubes in 3 dimensions is {day17.RunDay17Part1()}");
            //Console.WriteLine($"The amount of active cubes in 4 dimensions is {day17.RunDay17Part2()}");

            //var day18 = new Day18();
            //day18.RunDay18Part1();
            //day18.RunDay18Part2();

            //var day19 = new Day19("\\AOC 2020\\Day19\\InputDay19.txt");
            //day19.RunDay19Part1();
            //day19.RunDay19Part2();

            var day20 = new Day20("\\AOC 2020\\Day20\\InputDay20.txt");
            //day20.RunDay20Part1();
            //day20.RunDay20Part2();

            //var day21 = new Day21("\\AOC 2020\\Day21\\InputDay21.txt");
            //day21.RunDay21Part1();
            //day21.RunDay21Part2();

            //var day22 = new Day22("\\AOC 2020\\Day22\\InputDay22.txt");
            ////day22.RunDay22Part1();
            //day22.RunDay22Part2();

            //var day23 = new Day23();
            //day23.RunDay23Part1();
            //day23.RunDay23Part2();

            //var day24 = new Day24();
            //day24.RunDay24Part1();
            //Too slow
            //day24.RunDay24Part2();

            var day25 = new Day25();
            day25.RunDay25Part1();


            Console.ReadLine();
        }

        internal bool?[,] CreateTwoDimensionalBoolArrayFromInput(string path)
        {
            var lines = ConvertFileToLines(path);

            var array = new bool?[lines.First().Length, lines.Count()];

            int x;
            int y;
            bool? value = null;

            for (y = 0; y < lines.Count(); y++)
            {
                var line = lines[y];
                var lineArray = line.ToCharArray();
                for (x = 0; x < lineArray.Count(); x++)
                {
                    var character = lineArray[x];
                    switch (character)
                    {
                        case '.':
                            value = null;
                            break;
                        case 'L':
                            value = false;
                            break;
                            throw new ArgumentException("An unknown character is used");
                    }
                    array[x, y] = value;
                }
            }

            return array;
        }

        internal long[] ConvertLinesToLongArray(List<string> lines)
        {
            var longArray = new long[lines.Count];

            for(int i = 0; i < lines.Count; i++)
            {
                longArray[i] = long.Parse(lines[i]);
            }

            return longArray;
        }

        public char[,] ConvertLinesToTwoDimensionalCharArray(List<string> lines)
        {
            var width = lines.First().Length;
            var height = lines.Count;

            var char2DArray = new char[width, height];

            for (int h = 0; h < height; h++)
            {
                var line = lines[height - h - 1].ToCharArray();
                for (int w = 0; w < width; w++)
                {
                    char2DArray[w, h] = line[w];
                }
            }

            return char2DArray;
        }

        public List<string> ConvertFileToLines(string path)
        {
            return File.ReadAllLines(BaseUrl + path).ToList();
        }

        public List<string> ConvertFileToParagraphs(string path)
        {
            var fulltext = File.ReadAllText(BaseUrl + path);

            return fulltext.Split(new string[] { Environment.NewLine + Environment.NewLine },
                StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        public List<string> SplitParagraphsByLineEndingsAndSpaces(string paragraph)
        {
            //Remove new line
            var paragraphWithoutLineEndings = SplitParagraphsByLineEndings(paragraph);
            return paragraphWithoutLineEndings.Split(' ').ToList();
        }

        public List<string> SplitParagraphByLineEndings(string paragraph)
        {
            return paragraph.Split("\r\n").ToList();
        }

        public List<string> SplitLinesByCharacters(string line, char splitCharacter)
        {
            return line.Split(splitCharacter).ToList();
        }

        internal string SplitParagraphsByLineEndings(string paragraph)
        {
            return paragraph.Replace("\r\n", " ");
        }

        internal List<int> MakeSortedListFromFile(string path)
        {
            var numbers = new List<int>();
            List<string> lines = ConvertFileToLines(path);

            foreach (var line in lines)
            {
                int.TryParse(line, out var number);

                numbers.Add(number);
            }

            numbers.Sort();
            return numbers;
        }
    }
}

