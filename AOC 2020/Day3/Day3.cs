using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC_2020
{
    public class Day3
    {
        static string path = "\\AOC 2020\\Day3\\InputDay3.txt";

        private MainProgram mainProgram = new MainProgram();

        public void RunDay3Part1()
        {
            var lines = mainProgram.ConvertFileToLines(path);
            var answer = CalculateTrees(lines, 3, 1);

            Console.WriteLine($"The count of tree hit is {answer} ");
        }

        public void RunDay3Part2()
        {
            var lines = mainProgram.ConvertFileToLines(path);
            long answer = CalculateTreesPart2(lines);

            Console.WriteLine($"The count of tree hit is {answer} ");
        }

        public long CalculateTreesPart2(List<string> lines)
        {
            var slope1 = CalculateTrees(lines, 1, 1);
            var slope2 = CalculateTrees(lines, 3, 1);
            var slope3 = CalculateTrees(lines, 5, 1);
            var slope4 = CalculateTrees(lines, 7, 1);
            var slope5 = CalculateTrees(lines, 1, 2);

            long answer = slope1 * slope2 * slope3 * slope4 * slope5;
            return answer;
        }

        public long CalculateTrees(List<string> lines, int right, int down)
        {
            long LineNumber = 0;
            int XCoordinate = 0;
            int trees = 0;
            int width = lines.First().Count();

            List<Coordinate> Coordinates = new List<Coordinate>();

            foreach (var line in lines)
            {
                Coordinates.AddRange(CreateLine(line, LineNumber));
                LineNumber++;
            }

            for(int y = 0; y < LineNumber; y += down)
            {
                Math.DivRem(XCoordinate, width, out int finalXCoordinate);

                var passedCoordinate = new Coordinate { XCoordinate = finalXCoordinate, YCoordinate = y };

                var treeCoordinate = Coordinates.Where(x => 
                x.XCoordinate == passedCoordinate.XCoordinate && 
                x.YCoordinate == passedCoordinate.YCoordinate)
                .SingleOrDefault();

                if(treeCoordinate != null)
                    trees++;

                XCoordinate = XCoordinate + right;
            }

            return trees;
        }

        public List<Coordinate> CreateLine(string line, long lineNumber)
        {
            long XCoordinate = 0;

            var arrayCharacters = line.ToCharArray();
            
            List<Coordinate> TreeCoordinates = new List<Coordinate>();

            foreach (var character in arrayCharacters)
            {
                if(character == '#')
                {
                    TreeCoordinates.Add(new Coordinate { XCoordinate = XCoordinate, YCoordinate = lineNumber });
                }
                XCoordinate++;
            }

            return TreeCoordinates;
        }
    }
}

public class Coordinate : IEquatable<Coordinate>
{
    public long XCoordinate { get; set; }
    public long YCoordinate { get; set; }

    public bool Equals(Coordinate other)
    {
        return XCoordinate == other.XCoordinate
            && YCoordinate == other.YCoordinate;
    }
}
