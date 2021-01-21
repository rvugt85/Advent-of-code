using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC_2020
{
    public class Day11
    {
        private MainProgram mainProgram = new MainProgram();

        private bool?[,] SeatPlan;
        private bool?[,] Changes;
        

        public Day11(string path)
        {
            SeatPlan = mainProgram.CreateTwoDimensionalBoolArrayFromInput(path);
            Changes = new bool?[SeatPlan.GetLength(0), SeatPlan.GetLength(1)];  
        }

        public void RunDay11Part1()
        {
            int amountOfSeatsTaken = RunModelForArrivals();

            Console.WriteLine($"The amount of seat taken when no seat changes happen anymore is {amountOfSeatsTaken}");
        }

        public void RunDay11Part2()
        {
            int amountOfSeatsTaken = RunModelForArrivalsWithComplicateSeatingRules();

            Console.WriteLine($"The amount of seat taken when no seat changes happen anymore is {amountOfSeatsTaken}");
        }

        public int RunModelForArrivals()
        {
            int changedSeats = 1;

            while(changedSeats != 0)
            {
                AnalyseChanges(false);
                changedSeats = ProcessChanges();
            }

            return CountTrueValuesInArray(SeatPlan);
        }

        public int RunModelForArrivalsWithComplicateSeatingRules()
        {
            int changedSeats = 1;

            while(changedSeats != 0)
            {
                AnalyseChanges(true);
                changedSeats = ProcessChanges();
            }

            return CountTrueValuesInArray(SeatPlan);
        }

        private void AnalyseChanges(bool complicated)
        {
            bool? isChanged = null;

            for (int x = 0; x < SeatPlan.GetLength(0); x++)
                for (int y = 0; y < SeatPlan.GetLength(1); y++)
                {
                    var value = SeatPlan[x, y];
                    switch (value)
                    {
                        case true:
                            isChanged = ProcessOccupiedRule(x,y, complicated);
                            break;
                        case false:
                            isChanged = ProcessEmptyRule(x, y, complicated);
                            break;
                        case null:
                            isChanged = null;
                            break;
                        throw new ArgumentException("Something went wrong");
                    }
                    Changes[x, y] = isChanged;
                }            
        }

        private int ProcessChanges()
        {
            var changedSeats = 0;

            for (int x = 0; x < Changes.GetLength(0); x++)
                for (int y = 0; y < Changes.GetLength(1); y++)
                {
                    if(Changes[x,y] == true)
                    {
                        changedSeats++;
                        var seatChanged = SeatPlan[x, y];
                        if(seatChanged == false)
                        {
                            SeatPlan[x,y] = true;
                        }
                        if(seatChanged == true)
                        {
                            SeatPlan[x,y] = false;
                        }
                    }
                }
            return changedSeats;
        }

        private bool ProcessOccupiedRule(int x, int y, bool complicated)
        {
            var isChanged = false;
            int countOfOccupiedSeats = 0;
            if (complicated == false)
            {
                var adjacentSeats = GetAdjacentSeats(x, y);
                countOfOccupiedSeats = CountTrueValuesInArray(adjacentSeats);
            }
            else
            {
                countOfOccupiedSeats = GetAdjacentSeatsOccupiedComplicated(x, y);
            }
            
            if (countOfOccupiedSeats > 4)
                isChanged = true;
            return isChanged;
        }

        private bool ProcessEmptyRule(int x, int y, bool complicated)
        {
            var isChanged = false;
            int countOfOccupiedSeats = 0;
            if (complicated == false)
            {
                var adjacentSeats = GetAdjacentSeats(x, y);
                countOfOccupiedSeats = CountTrueValuesInArray(adjacentSeats);
            }
            else
            {
                countOfOccupiedSeats = GetAdjacentSeatsOccupiedComplicated(x, y);
            }
            if (countOfOccupiedSeats == 0)
                isChanged = true;
            return isChanged;
        }

        private bool?[,] GetAdjacentSeats(int x, int y)
        {
            var xWithinAdjacentSeats = 0;
            var yWithinAdjacentSeats = 0;

            List<int> xValues = GetValuesOfAdjacentSeats(x, SeatPlan.GetLength(0));
            List<int> yvalues = GetValuesOfAdjacentSeats(y, SeatPlan.GetLength(1));
            var adjacentChairs = new bool?[xValues.Count(), yvalues.Count()];

            foreach(var xvalue in xValues)
            {
                foreach(var yvalue in yvalues)
                {
                    adjacentChairs[xWithinAdjacentSeats, yWithinAdjacentSeats] = SeatPlan[xvalue, yvalue];
                    yWithinAdjacentSeats++;
                }
                yWithinAdjacentSeats = 0;
                xWithinAdjacentSeats++;
            }

            return adjacentChairs;
        }

        private int GetAdjacentSeatsOccupiedComplicated(int x, int y)
        {
            int seatsInSightOccupied = 0;

            for (int xmovement = -1; xmovement <= 1; xmovement++)
                for (int ymovement = -1; ymovement <= 1; ymovement++)
                {
                    if (!(xmovement == 0 && ymovement == 0))
                    {
                        var newXValue = x + xmovement;
                        var newYValue = y + ymovement;

                        bool seatInSightReached = false;
                        while (!seatInSightReached
                            && (newXValue >= 0)
                            && (newXValue < SeatPlan.GetLength(0))
                            && (newYValue >= 0)
                            && (newYValue < SeatPlan.GetLength(1)))
                        {
                            var seat = SeatPlan[newXValue, newYValue];
                            if (seat == true)
                            {
                                seatInSightReached = true;
                                seatsInSightOccupied++;
                            }
                            else if (seat == false)
                            {
                                seatInSightReached = true;
                            }
                            else
                            {
                                newXValue += xmovement;
                                newYValue += ymovement;
                            }
                        }
                    }
                }

            return seatsInSightOccupied;
        }

        private List<int> GetValuesOfAdjacentSeats(int valueOfSeat, int length)
        {
            List<int> valuesOfAdjacentSeats = new List<int> { valueOfSeat };

            if(valueOfSeat != length - 1)
            {
                valuesOfAdjacentSeats.Add(valueOfSeat + 1);
            }
            if(valueOfSeat != 0)
            {
                valuesOfAdjacentSeats.Add(valueOfSeat - 1);
            }
            return valuesOfAdjacentSeats;
        }

        private int CountTrueValuesInArray(bool?[,] adjacentSeats)
        {
            int takenSeats = 0;

            for (int x = 0; x < adjacentSeats.GetLength(0); x++)
                for (int y = 0; y < adjacentSeats.GetLength(1); y++)
                {
                    if (adjacentSeats[x, y] == true)
                        takenSeats++;
                }

            return takenSeats;
        }
    }
}
