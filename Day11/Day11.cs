using System;
using System.IO;
using System.Linq;
using System.Collections.Generic; //For list
using System.Text.RegularExpressions; // For regex

namespace AdventOfCode2020
{
    public class Day11
    {
        public string SolvePart1(string input)
        {
            bool layoutChanged = true;

            //Read input file --> to list()
            int[,] seatInfo = ReadSeatLayout(input);
            Console.WriteLine("VALUE is: {0}", seatInfo[0, 0]);
            while (layoutChanged == true)
            {
                (seatInfo, layoutChanged) = UpdateSeatLayout(seatInfo);
                //PrintLayout(seatInfo);
            }

            int seatsOccupied = CountOccupiedSeats(seatInfo);

            return seatsOccupied.ToString();
        }



        public string SolvePart2(string input)
        {
            bool layoutChanged = true;

            //Read input file --> to list()
            int[,] seatInfo = ReadSeatLayout(input);

            while (layoutChanged == true)
            {
                (seatInfo, layoutChanged) = UpdateSeatLayout_V2(seatInfo);
                //PrintLayout(seatInfo);
            }

            int seatsOccupied = CountOccupiedSeats(seatInfo);

            return seatsOccupied.ToString();

        }

        public int CountOccupiedSeats(int[,] seatInfo)
        {
            int seatsOccupied = 0;
            for (int x = 0; x < seatInfo.GetLength(0); x++)
            {
                for (int y = 0; y < seatInfo.GetLength(1); y++)
                {
                    if (seatInfo[x, y] == 2)
                    {
                        seatsOccupied++;
                    }
                }
            }
            return seatsOccupied;
        }





        public (int[,], bool) UpdateSeatLayout_V2(int[,] seatInfo) //For Part 2
        {
            int[,] newSeatInfo = new int[seatInfo.GetLength(0), seatInfo.GetLength(1)];

            //Limit search area on size of array
            int xMin = 0;
            int xMax = seatInfo.GetLength(0) - 1;
            int yMin = 0;
            int yMax = seatInfo.GetLength(1) - 1;

            int nrOfSeatsAdjacentOccupied = 0;
            bool layoutChanged = false;

            for (int x = 0; x <= xMax; x++)
            {
                for (int y = 0; y <= yMax; y++)
                {
                    newSeatInfo[x, y] = seatInfo[x, y];
                    if (seatInfo[x, y] > 0)
                    {
                        nrOfSeatsAdjacentOccupied = 0;

                        //Find adjacent seats to right and check if they are occupied
                        for (int xCheck = x + 1; xCheck <= xMax; xCheck++)
                        {
                            if (xCheck <= xMax)
                            {
                                if (seatInfo[xCheck, y] == 2)
                                {
                                    nrOfSeatsAdjacentOccupied++;
                                    break;
                                }
                                if (seatInfo[xCheck, y] == 1)
                                {
                                    break;
                                }
                            }
                        }

                        //Find adjacent seats to left and check if they are occupied
                        for (int xCheck = x - 1; xCheck >= xMin; xCheck--)
                        {
                            if (xCheck >= xMin)
                            {
                                if (seatInfo[xCheck, y] == 2)
                                {
                                    nrOfSeatsAdjacentOccupied++;
                                    break;
                                }
                                if (seatInfo[xCheck, y] == 1)
                                {
                                    break;
                                }
                            }
                        }



                        //Find adjacent seats to down and check if they are occupied
                        for (int yCheck = y + 1; yCheck <= yMax; yCheck++)
                        {
                            if (yCheck <= yMax)
                            {
                                if (seatInfo[x, yCheck] == 2)
                                {
                                    nrOfSeatsAdjacentOccupied++;
                                    break;
                                }
                                if (seatInfo[x, yCheck] == 1)
                                {
                                    break;
                                }
                            }
                        }

                        //Find adjacent seats to up and check if they are occupied
                        for (int yCheck = y - 1; yCheck >= yMin; yCheck--)
                        {
                            if (yCheck >= yMin)
                            {
                                if (seatInfo[x, yCheck] == 2)
                                {
                                    nrOfSeatsAdjacentOccupied++;
                                    break;
                                }
                                if (seatInfo[x, yCheck] == 1)
                                {
                                    break;
                                }
                            }
                        }


                        //Find adjacent seats to right/up and check if they are occupied
                        for (int xCheck = x + 1; xCheck <= xMax; xCheck++)
                        {
                            int yCheck = y + (xCheck - x);
                            if (xCheck <= xMax && yCheck <= yMax)
                            {
                                if (seatInfo[xCheck, yCheck] == 2)
                                {
                                    nrOfSeatsAdjacentOccupied++;
                                    break;
                                }
                                if (seatInfo[xCheck, yCheck] == 1)
                                {
                                    break;
                                }
                            }
                        }


                        //Find adjacent seats to right/down and check if they are occupied
                        for (int xCheck = x + 1; xCheck <= xMax; xCheck++)
                        {
                            int yCheck = y - (xCheck - x);
                            if (xCheck <= xMax && yCheck >= yMin)
                            {
                                if (seatInfo[xCheck, yCheck] == 2)
                                {
                                    nrOfSeatsAdjacentOccupied++;
                                    break;
                                }
                                if (seatInfo[xCheck, yCheck] == 1)
                                {
                                    break;
                                }
                            }
                        }


                        //Find adjacent seats to left/up and check if they are occupied
                        for (int xCheck = x - 1; xCheck >= xMin; xCheck--)
                        {
                            int yCheck = y - (x - xCheck);
                            if (xCheck >= xMin && yCheck >= yMin)
                            {
                                if (seatInfo[xCheck, yCheck] == 2)
                                {
                                    nrOfSeatsAdjacentOccupied++;
                                    break;
                                }
                                if (seatInfo[xCheck, yCheck] == 1)
                                {
                                    break;
                                }
                            }
                        }

                        //Find adjacent seats to left/down and check if they are occupied
                        for (int xCheck = x - 1; xCheck >= xMin; xCheck--)
                        {
                            int yCheck = y + (x - xCheck);
                            if (xCheck >= xMin && yCheck <= yMax)
                            {
                                if (seatInfo[xCheck, yCheck] == 2)
                                {
                                    nrOfSeatsAdjacentOccupied++;
                                    break;
                                }
                                if (seatInfo[xCheck, yCheck] == 1)
                                {
                                    break;
                                }
                            }
                        }


                        //If seat is empty and No occupied seats adjacent to it ==> Seat becomes occupied
                        if (seatInfo[x, y] == 1 && nrOfSeatsAdjacentOccupied == 0)
                        {
                            layoutChanged = true;
                            newSeatInfo[x, y] = 2;
                        }


                        //If seat is occupied  and 4 or more seats adjacent to it are occupied ==> Seat becomes empty
                        if (seatInfo[x, y] == 2 && nrOfSeatsAdjacentOccupied >= 5) //Included this seat so no >=
                        {
                            layoutChanged = true;
                            newSeatInfo[x, y] = 1;
                        }
                    }
                }
            }

            return (newSeatInfo, layoutChanged);
        }



        public (int[,], bool) UpdateSeatLayout(int[,] seatInfo)
        {
            int[,] newSeatInfo = new int[seatInfo.GetLength(0), seatInfo.GetLength(1)];

            int xMin = 0;
            int xMax = 0;
            int yMin = 0;
            int yMax = 0;

            int nrOfSeatsAdjacentOccupied = 0;
            bool layoutChanged = false;

            for (int x = 0; x < seatInfo.GetLength(0); x++)
            {
                for (int y = 0; y < seatInfo.GetLength(1); y++)
                {
                    newSeatInfo[x, y] = seatInfo[x, y];
                    if (seatInfo[x, y] > 0)
                    {
                        //Define search area for adjacent seats
                        xMin = x - 1;
                        xMax = x + 1;
                        yMin = y - 1;
                        yMax = y + 1;

                        //Limit search area on size of array
                        if (xMin < 0) { xMin = 0; }
                        if (yMin < 0) { yMin = 0; }
                        if (xMax >= seatInfo.GetLength(0)) { xMax = seatInfo.GetLength(0) - 1; }
                        if (yMax >= seatInfo.GetLength(1)) { yMax = seatInfo.GetLength(1) - 1; }


                        nrOfSeatsAdjacentOccupied = 0;

                        //Find adjacent seats and check if they are occupied
                        for (int xCheck = xMin; xCheck <= xMax; xCheck++)
                        {
                            for (int yCheck = yMin; yCheck <= yMax; yCheck++)
                            {
                                if (seatInfo[xCheck, yCheck] >= 2)
                                {
                                    nrOfSeatsAdjacentOccupied++;
                                }
                            }
                        }

                        //If seat is empty and No occupied seats adjacent to it ==> Seat becomes occupied
                        if (seatInfo[x, y] == 1 && nrOfSeatsAdjacentOccupied == 0)
                        {
                            layoutChanged = true;
                            newSeatInfo[x, y] = 2;
                        }


                        //If seat is occupied  and 4 or more seats adjacent to it are occupied ==> Seat becomes empty
                        if (seatInfo[x, y] == 2 && nrOfSeatsAdjacentOccupied > 4) //Included this seat so no >=
                        {
                            layoutChanged = true;
                            newSeatInfo[x, y] = 1;
                        }
                    }
                }
            }

            return (newSeatInfo, layoutChanged);
        }

        public int[,] ReadSeatLayout(string input)
        {
            //Convert input to array of integers.
            string[] lines = input.Split(Environment.NewLine);

            int[,] seatInfo = new int[lines[0].Length, lines.Length];

            int x = 0;
            int y = 0;
            int state;

            foreach (string line in lines)
            {
                x = 0;
                foreach (char seat in line)
                {
                    switch (seat)
                    {
                        case '.':
                            state = 0;
                            break;

                        case 'L':
                            state = 1;
                            break;
                        case '#':
                            state = 2;
                            break;

                        default:
                            state = 999;
                            Console.WriteLine("ERROR: Invalid state detected!");
                            break;
                    }

                    seatInfo[x, y] = state;

                    x++;
                }

                y++;
            }
            return seatInfo;

        }

        public void PrintLayout(int[,] seatInfo)
        {
            //Function just for debugging.....
            Console.WriteLine("-------------------");
            for (int y = 0; y < seatInfo.GetLength(1); y++)
            {
                for (int x = 0; x < seatInfo.GetLength(0); x++)
                {
                    Console.Write(seatInfo[x, y]);
                }
                Console.WriteLine("");
            }

            Console.WriteLine("-------------------");
        }



        public string GetInput(bool testInput)
        {
            var myInput = new Inputs.Day11();
            return (testInput) ? myInput.testInput : myInput.input;
        }

    }

}
