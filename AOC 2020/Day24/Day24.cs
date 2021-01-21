using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AOC_2020
{
    public class Day24
    {
        static string path = "\\AOC 2020\\Day24\\InputDay24.txt";

        private MainProgram mainProgram = new MainProgram();

        public void RunDay24Part1()
        {
            var lines = mainProgram.ConvertFileToLines(path);

            var result = GetBlackTiles(lines).Count;

            Console.WriteLine($"The count of black tiles is {result}");
        }

        public void RunDay24Part2()
        {
            var lines = mainProgram.ConvertFileToLines(path);

            var coordinates = GetBlackTiles(lines);

            for (int day = 1; day <= 100; day++)
                coordinates = RunRules(coordinates);

            Console.WriteLine($"The count of black tiles after 100 days is {coordinates.Count}");
        }

        public List<Coordinate> GetBlackTiles(List<string> lines)
        {
            List<Coordinate> coordinates = new List<Coordinate>();

            foreach (var line in lines)
            {
                Coordinate coordinate = new Coordinate { XCoordinate = 0, YCoordinate = 0 };
                var characters = line.ToCharArray();

                for (var i = 0; i < characters.Length; i++)
                {
                    var character = characters[i];
                    switch (character)
                    {
                        case 's':
                            coordinate.YCoordinate--;
                            if (characters[i + 1] == 'w')
                                coordinate.XCoordinate--;
                            i++;
                            break;
                        case 'n':
                            coordinate.YCoordinate++;
                            if (characters[i + 1] == 'e')
                                coordinate.XCoordinate++;
                            i++;
                            break;
                        case 'e':
                            coordinate.XCoordinate++;
                            break;
                        case 'w':
                            coordinate.XCoordinate--;
                            break;
                    }
                }

                if (!coordinates.Contains(coordinate))
                {
                    coordinates.Add(coordinate);
                }
                else
                {
                    coordinates.Remove(coordinate);
                }
            }
            return coordinates;
        }

        public List<Coordinate> RunRules(List<Coordinate> coordinates)
        {
            List<Coordinate> newCoordinates = new List<Coordinate>();
            var maxXValue = coordinates.Max(c => c.XCoordinate) + 2;
            var minXValue = coordinates.Min(c => c.XCoordinate) - 2;
            var maxYValue = coordinates.Max(c => c.YCoordinate) + 2;
            var minYValue = coordinates.Min(c => c.YCoordinate) - 2;
            for(var xValue = minXValue; xValue < maxXValue; xValue++)
            {
                for (var yValue = minYValue; yValue < maxYValue; yValue++)
                {
                    var coordinate = new Coordinate { XCoordinate = xValue, YCoordinate = yValue };
                    var neigbors = GetNeighbors(coordinate);
                    var blackNeighbors = 0;
                    foreach(var neighbor in neigbors)
                    {
                        if (coordinates.Contains(neighbor))
                            blackNeighbors++;
                    }
                    if (coordinates.Contains(coordinate))
                    {
                        if (blackNeighbors > 0 && blackNeighbors <= 2)
                            newCoordinates.Add(coordinate);
                    }
                    else
                    {
                        if (blackNeighbors == 2)
                            newCoordinates.Add(coordinate);
                    }
                }
            }

            return newCoordinates;
        }

        private List<Coordinate> GetNeighbors(Coordinate coordinate)
        {
            List<Coordinate> neighbors = new List<Coordinate>();

            neighbors.Add(new Coordinate { XCoordinate = coordinate.XCoordinate, YCoordinate = coordinate.YCoordinate + 1 });
            neighbors.Add(new Coordinate { XCoordinate = coordinate.XCoordinate, YCoordinate = coordinate.YCoordinate - 1 });
            neighbors.Add(new Coordinate { XCoordinate = coordinate.XCoordinate + 1, YCoordinate = coordinate.YCoordinate });
            neighbors.Add(new Coordinate { XCoordinate = coordinate.XCoordinate - 1, YCoordinate = coordinate.YCoordinate });
            neighbors.Add(new Coordinate { XCoordinate = coordinate.XCoordinate -1, YCoordinate = coordinate.YCoordinate - 1 });
            neighbors.Add(new Coordinate { XCoordinate = coordinate.XCoordinate + 1, YCoordinate = coordinate.YCoordinate + 1 });

            return neighbors;
        }
    }
}

