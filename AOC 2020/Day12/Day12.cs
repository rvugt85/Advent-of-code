using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AOC_2020
{
    public class Day12
    {
        public string path = "\\AOC 2020\\Day12\\InputDay12.txt";

        private MainProgram mainProgram = new MainProgram();

        public void RunDay12Part1()
        {
            var instructions = mainProgram.ConvertFileToLines(path);

            var finalPosition = ProcessInstructions(instructions);

            var manhattanDistance = (Math.Abs(finalPosition.NorthSouth) + Math.Abs(finalPosition.EastWest));

            Console.WriteLine($"The manhattan distance of the boat after the instructions is {manhattanDistance}");
        }

        public void RunDay12Part2()
        {
            var instructions = mainProgram.ConvertFileToLines(path);

            var finalPosition = ProcessInstructionsWithWaypoint(instructions);

            var manhattanDistance = (Math.Abs(finalPosition.NorthSouth) + Math.Abs(finalPosition.EastWest));

            Console.WriteLine($"The manhattan distance of the boat after the instructions with waypoint is {manhattanDistance}");
        }

        public Location ProcessInstructions(List<string> instructions)
        {
            var currentLocation = new Location { NorthSouth = 0, EastWest = 0 };
            var currentDirection = 'E';

            foreach(var instruction in instructions)
            {
                var action = instruction[0];
                var value = int.Parse(instruction.Remove(0, 1));

                switch (action)
                {
                    case 'N':
                        currentLocation.NorthSouth += value;
                        break;
                    case 'S':
                        currentLocation.NorthSouth -= value;
                        break;
                    case 'E':
                        currentLocation.EastWest += value;
                        break;
                    case 'W':
                        currentLocation.EastWest -= value;
                        break;
                    case 'L':
                        for(int i = 0; i < (value/90); i++)
                            switch (currentDirection)
                            {
                                case 'N':
                                    currentDirection = 'W';
                                    break;
                                case 'S':
                                    currentDirection = 'E';
                                    break;
                                case 'E':
                                    currentDirection = 'N';
                                    break;
                                case 'W':
                                    currentDirection = 'S';
                                    break;
                            }
                        break;
                    case 'R':
                        for (int i = 0; i < (value / 90); i++)
                            switch (currentDirection)
                            {
                                case 'N':
                                    currentDirection = 'E';
                                    break;
                                case 'S':
                                    currentDirection = 'W';
                                    break;
                                case 'E':
                                    currentDirection = 'S';
                                    break;
                                case 'W':
                                    currentDirection = 'N';
                                    break;
                            }
                        break;
                    case 'F':
                        switch (currentDirection)
                        {
                            case 'N':
                                currentLocation.NorthSouth += value;
                                break;
                            case 'S':
                                currentLocation.NorthSouth -= value;
                                break;
                            case 'E':
                                currentLocation.EastWest += value;
                                break;
                            case 'W':
                                currentLocation.EastWest -= value;
                                break;
                        }
                        break;
                }
                

            }

            return currentLocation;
        }

        public Location ProcessInstructionsWithWaypoint(List<string> instructions)
        {
            var currentLocation = new Location { NorthSouth = 0, EastWest = 0 };
            var waypoint = new Location { NorthSouth = 1, EastWest = 10 };

            foreach (var instruction in instructions)
            {
                var action = instruction[0];
                var value = int.Parse(instruction.Remove(0, 1));
                int newNorthSouth = 0;
                int newEastWest = 0;

                switch (action)
                {
                    case 'N':
                        waypoint.NorthSouth += value;
                        break;
                    case 'S':
                        waypoint.NorthSouth -= value;
                        break;
                    case 'E':
                        waypoint.EastWest += value;
                        break;
                    case 'W':
                        waypoint.EastWest -= value;
                        break;
                    case 'L':
                        for (int i = 0; i < (value / 90); i++)
                        {
                            newNorthSouth = waypoint.EastWest;
                            newEastWest = -waypoint.NorthSouth;
                            waypoint.NorthSouth = newNorthSouth;
                            waypoint.EastWest = newEastWest;
                        }
                        break;
                    case 'R':
                        for (int i = 0; i < (value / 90); i++)
                        {
                            newNorthSouth = -waypoint.EastWest;
                            newEastWest = waypoint.NorthSouth;
                            waypoint.NorthSouth = newNorthSouth;
                            waypoint.EastWest = newEastWest;
                        }
                        break;
                    case 'F':
                        currentLocation.NorthSouth += waypoint.NorthSouth * value;
                        currentLocation.EastWest += waypoint.EastWest * value;
                        break;
                }
            }

            return currentLocation;
        }
    }

    public class Location
    {
        public int NorthSouth { get; set; }
        public int EastWest { get; set; }
    }
}
