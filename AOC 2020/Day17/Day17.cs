using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC_2020
{
    public class Day17
    {
        MainProgram mainProgram = new MainProgram();

        private bool[,,] StartingConfiguration3D;
        private bool[,,,] StartingConfiguration4D;

        public Day17(string path)
        {
            StartingConfiguration3D = CreateThreeDimensionalBoolArrayFromInput(path);
            StartingConfiguration4D = CreateFourDimensionalBoolArrayFromInput(path);
        }

        public int RunDay17Part1()
        {
            var configuration = StartingConfiguration3D;

            for(int i = 0; i < 6; i++)
            {
                configuration = IncreaseConfiguration3D(configuration);

                var changes = StateChanges3D(configuration);
                configuration = ProcessChanges3D(changes, configuration);
            }

            return CountActiveCubes3D(configuration);
        }

        public int RunDay17Part2()
        {
            var configuration = StartingConfiguration4D;

            for (int i = 0; i < 6; i++)
            {
                configuration = IncreaseConfiguration4D(configuration);

                var changes = StateChanges4D(configuration);
                configuration = ProcessChanges4D(changes, configuration);
            }

            return CountActiveCubes4D(configuration);
        }

        internal bool[,,] CreateThreeDimensionalBoolArrayFromInput(string path)
        {
            var lines = mainProgram.ConvertFileToLines(path);

            var array = new bool[lines.First().Length, lines.Count(), 1];

            int x;
            int y;
            bool value = false;

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
                            value = false;
                            break;
                        case '#':
                            value = true;
                            break;
                            throw new ArgumentException("An unknown character is used");
                    }
                    array[x, y, 0] = value;
                }
            }

            return array;
        }

        internal bool[,,,] CreateFourDimensionalBoolArrayFromInput(string path)
        {
            var lines = mainProgram.ConvertFileToLines(path);

            var array = new bool[lines.First().Length, lines.Count(), 1, 1];

            int x;
            int y;
            bool value = false;

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
                            value = false;
                            break;
                        case '#':
                            value = true;
                            break;
                            throw new ArgumentException("An unknown character is used");
                    }
                    array[x, y, 0, 0] = value;
                }
            }

            return array;
        }

        private bool[,,] IncreaseConfiguration3D(bool[,,] configuration)
        {
            var xLength = configuration.GetLength(0);
            var yLength = configuration.GetLength(1);
            var zLength = configuration.GetLength(2);

            var increasedConfiguration = new bool[xLength + 2, yLength + 2, zLength + 2];

            for (int x = 0; x < xLength; x++)
                for (int y = 0; y < yLength; y++)
                    for (int z = 0; z < zLength; z++)
                    {
                        if (configuration[x, y, z] == true)
                            increasedConfiguration[x + 1, y + 1, z + 1] = true;
                    }

            return increasedConfiguration;

        }

        private bool[,,,] IncreaseConfiguration4D(bool[,,,] configuration)
        {
            var xLength = configuration.GetLength(0);
            var yLength = configuration.GetLength(1);
            var zLength = configuration.GetLength(2);
            var wLength = configuration.GetLength(3);

            var increasedConfiguration = new bool[xLength + 2, yLength + 2, zLength + 2, wLength + 2];

            for (int x = 0; x < xLength; x++)
                for (int y = 0; y < yLength; y++)
                    for (int z = 0; z < zLength; z++)
                        for (int w = 0; w < wLength; w++)
                    {
                        if (configuration[x, y, z, w] == true)
                            increasedConfiguration[x + 1, y + 1, z + 1, w + 1] = true;
                    }

            return increasedConfiguration;

        }

        private int CountActiveCubes3D(bool [,,] configuration)
        {
            var xLength = configuration.GetLength(0);
            var yLength = configuration.GetLength(1);
            var zLength = configuration.GetLength(2);

            var activeCubes = 0;

            for (int x = 0; x < xLength; x++)
                for (int y = 0; y < yLength; y++)
                    for (int z = 0; z < zLength; z++)
                    {
                        if (configuration[x, y, z] == true)
                            activeCubes++;
                    }

            return activeCubes;
        }

        private int CountActiveCubes4D(bool[,,,] configuration)
        {
            var xLength = configuration.GetLength(0);
            var yLength = configuration.GetLength(1);
            var zLength = configuration.GetLength(2);
            var wLength = configuration.GetLength(3);

            var activeCubes = 0;

            for (int x = 0; x < xLength; x++)
                for (int y = 0; y < yLength; y++)
                    for (int z = 0; z < zLength; z++)
                        for (int w = 0; w < wLength; w++)
                        {
                            if (configuration[x, y, z, w] == true)
                                activeCubes++;
                        }

            return activeCubes;
        }

        private bool[,,] StateChanges3D(bool[,,] configuration)
        {
            var xLength = configuration.GetLength(0);
            var yLength = configuration.GetLength(1);
            var zLength = configuration.GetLength(2);

            var changes = new bool[xLength, yLength, zLength];

            for (int x = 0; x < xLength; x++)
                for (int y=0; y < yLength; y++)
                    for (int z=0; z < zLength; z++)
                    {
                        var activeNeighbors = GetActiveNeighbors3D(x, y, z, configuration);

                        if(configuration[x,y,z] == true)
                        {
                            if (activeNeighbors != 3 && activeNeighbors != 4)
                                changes[x, y, z] = true;
                        }
                        if (configuration[x, y, z] == false)
                        {
                            if (activeNeighbors == 3)
                                changes[x, y, z] = true;
                        }
                    }

            return changes;
        }

        private bool[,,,] StateChanges4D(bool[,,,] configuration)
        {
            var xLength = configuration.GetLength(0);
            var yLength = configuration.GetLength(1);
            var zLength = configuration.GetLength(2);
            var wLength = configuration.GetLength(3);

            var changes = new bool[xLength, yLength, zLength, wLength];

            for (int x = 0; x < xLength; x++)
                for (int y = 0; y < yLength; y++)
                    for (int z = 0; z < zLength; z++)
                        for (int w = 0; w < wLength; w++)
                        {
                            var activeNeighbors = GetActiveNeighbors4D(x, y, z, w, configuration);

                            if (configuration[x, y, z, w] == true)
                            {
                                if (activeNeighbors != 3 && activeNeighbors != 4)
                                    changes[x, y, z, w] = true;
                            }
                            if (configuration[x, y, z, w] == false)
                            {
                                if (activeNeighbors == 3)
                                    changes[x, y, z, w] = true;
                            }
                        }

            return changes;
        }

        private bool[,,] ProcessChanges3D(bool[,,] changes, bool[,,] configuration)
        {
            var xLength = configuration.GetLength(0);
            var yLength = configuration.GetLength(1);
            var zLength = configuration.GetLength(2);

            for (int x = 0; x < xLength; x++)
                for (int y = 0; y < yLength; y++)
                    for (int z = 0; z < zLength; z++)
                    {
                        if (changes[x,y,z] == true)
                        {
                            if (configuration[x, y, z] == true)
                                configuration[x, y, z] = false;
                            else configuration[x, y, z] = true;
                        }
                    }

            return configuration;
        }

        private bool[,,,] ProcessChanges4D(bool[,,,] changes, bool[,,,] configuration)
        {
            var xLength = configuration.GetLength(0);
            var yLength = configuration.GetLength(1);
            var zLength = configuration.GetLength(2);
            var wLength = configuration.GetLength(3);

            for (int x = 0; x < xLength; x++)
                for (int y = 0; y < yLength; y++)
                    for (int z = 0; z < zLength; z++)
                        for (int w = 0; w < wLength; w++)
                        {
                            if (changes[x, y, z, w] == true)
                            {
                                if (configuration[x, y, z, w] == true)
                                    configuration[x, y, z, w] = false;
                                else configuration[x, y, z, w] = true;
                            }
                        }

            return configuration;
        }

        private int GetActiveNeighbors3D(int xLocation, int yLocation, int zLocation, bool[,,] configuration)
        {
            var xLength = configuration.GetLength(0);
            var yLength = configuration.GetLength(1);
            var zLength = configuration.GetLength(2);

            var activeNeighbors = 0;

            var xValues = new List<int>();
            var yValues = new List<int>();
            var zValues = new List<int>();

            xValues = GetValues(xLocation, xLength);
            yValues = GetValues(yLocation, yLength);
            zValues = GetValues(zLocation, zLength);

            for (int x = xValues.Min(); x <= xValues.Max(); x++)
                for (int y = yValues.Min(); y <= yValues.Max(); y++)
                    for (int z = zValues.Min(); z <= zValues.Max(); z++)
                    {
                        if (configuration[x, y, z] == true)
                            activeNeighbors++;
                    }

            return activeNeighbors;

        }

        private int GetActiveNeighbors4D(int xLocation, int yLocation, int zLocation, int wLocation, bool[,,,] configuration)
        {
            var xLength = configuration.GetLength(0);
            var yLength = configuration.GetLength(1);
            var zLength = configuration.GetLength(2);
            var wLength = configuration.GetLength(3);

            var activeNeighbors = 0;

            var xValues = new List<int>();
            var yValues = new List<int>();
            var zValues = new List<int>();
            var wValues = new List<int>();

            xValues = GetValues(xLocation, xLength);
            yValues = GetValues(yLocation, yLength);
            zValues = GetValues(zLocation, zLength);
            wValues = GetValues(wLocation, wLength);

            for (int x = xValues.Min(); x <= xValues.Max(); x++)
                for (int y = yValues.Min(); y <= yValues.Max(); y++)
                    for (int z = zValues.Min(); z <= zValues.Max(); z++)
                        for (int w = wValues.Min(); w <= wValues.Max(); w++)
                        {
                            if (configuration[x, y, z, w] == true)
                                activeNeighbors++;
                        }

            return activeNeighbors;

        }

        private List<int> GetValues(int value, int length)
        {
            var list = new List<int> { value };

            if (value != 0)
                list.Add(value - 1);
            if (value != length - 1)
                list.Add(value + 1);

            return list;
        }
    }

               
}
