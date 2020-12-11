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
            List<SeatInfo> seatInfo = new List<SeatInfo>();
            int seatsOccupied = 0;
            bool layoutChanged = true;

            //Read input file --> to list()
            seatInfo = ReadSeatLayout(input);


            while (layoutChanged == true)
            {
                (seatInfo, layoutChanged) = UpdateSeatLayout(seatInfo);

            }

            seatsOccupied = seatInfo.Count(seat => seat.State == 2);


            return seatsOccupied.ToString();
        }



        public string SolvePart2(string input)
        {
            List<SeatInfo> seatInfo = new List<SeatInfo>();
            int seatsOccupied = 0;
            bool layoutChanged = true;

            //Read input file --> to list()
            seatInfo = ReadSeatLayout(input);


            while (layoutChanged == true)
            {
                (seatInfo, layoutChanged) = UpdateSeatLayout_V2(seatInfo);
            }

            seatsOccupied = seatInfo.Count(seat => seat.State == 2);
            return seatsOccupied.ToString();

        }

        public void PrintList(List<SeatInfo> seatInfo)
        {
            //Function for showing the seat layout during testing. 
            int xMax = 9;

            Console.WriteLine("--------------------------");
            foreach (SeatInfo seat in seatInfo)
            {

                if (seat.X < xMax)
                {
                    Console.Write(seat.State);
                }
                else
                {
                    Console.WriteLine(seat.State);
                }
            }
            Console.WriteLine("--------------------------");

        }

        public (List<SeatInfo>, bool) UpdateSeatLayout_V2(List<SeatInfo> seatInfo)
        {
            List<SeatInfo> newSeatInfo = new List<SeatInfo>();
            SeatInfo newSeat = new SeatInfo();

            int nrOfSeatsAdjacentOccupied = 0;

            List<SeatInfo> checkSeat = new List<SeatInfo>();

            bool layoutChanged = false;

            foreach (SeatInfo seat in seatInfo)
            {
                newSeat.X = seat.X;
                newSeat.Y = seat.Y;
                newSeat.State = seat.State;

                if (seat.State > 0)
                {
                    nrOfSeatsAdjacentOccupied = 0;

                    //Find seat right of this seat
                    checkSeat = seatInfo.Where(s => s.X > seat.X && s.Y == seat.Y && s.State > 0).OrderBy(s => s.X).ToList();
                    if (checkSeat.Count() > 0)
                    {
                        if (checkSeat.First().State == 2)
                        {
                            nrOfSeatsAdjacentOccupied++;
                        }
                    }


                    //Find seat left of this seat
                    checkSeat = seatInfo.Where(s => s.X < seat.X && s.Y == seat.Y && s.State > 0).OrderByDescending(s => s.X).ToList();
                    if (checkSeat.Count() > 0)
                    {
                        if (checkSeat.First().State == 2)
                        {
                            nrOfSeatsAdjacentOccupied++;
                        }
                    }

                    //Find seat down of this seat
                    checkSeat = seatInfo.Where(s => s.Y > seat.Y && s.X == seat.X && s.State > 0).OrderBy(s => s.Y).ToList();
                    if (checkSeat.Count() > 0)
                    {
                        if (checkSeat.First().State == 2)
                        {
                            nrOfSeatsAdjacentOccupied++;
                        }
                    }

                    //Find seat up of this seat
                    checkSeat = seatInfo.Where(s => s.Y < seat.Y && s.X == seat.X && s.State > 0).OrderByDescending(s => s.Y).ToList();
                    if (checkSeat.Count() > 0)
                    {
                        if (checkSeat.First().State == 2)
                        {
                            nrOfSeatsAdjacentOccupied++;
                        }
                    }


                    //Find seat right/up of this seat
                    checkSeat = seatInfo.Where(s => s.X > seat.X && s.Y < seat.Y && (Math.Abs(s.X - seat.X) == Math.Abs(s.Y - seat.Y)) && s.State > 0).OrderBy(s => s.X).ToList();
                    if (checkSeat.Count() > 0)
                    {
                        if (checkSeat.First().State == 2)
                        {
                            nrOfSeatsAdjacentOccupied++;
                        }
                    }

                    //Find seat right/down of this seat
                    checkSeat = seatInfo.Where(s => s.X > seat.X && s.Y > seat.Y && (Math.Abs(s.X - seat.X) == Math.Abs(s.Y - seat.Y)) && s.State > 0).OrderBy(s => s.X).ToList();
                    if (checkSeat.Count() > 0)
                    {
                        if (checkSeat.First().State == 2)
                        {
                            nrOfSeatsAdjacentOccupied++;
                        }
                    }

                    //Find seat left/up of this seat
                    checkSeat = seatInfo.Where(s => s.X < seat.X && s.Y < seat.Y && (Math.Abs(s.X - seat.X) == Math.Abs(s.Y - seat.Y)) && s.State > 0).OrderByDescending(s => s.X).ToList();
                    if (checkSeat.Count() > 0)
                    {
                        if (checkSeat.First().State == 2)
                        {
                            nrOfSeatsAdjacentOccupied++;
                        }
                    }

                    //Find seat left/down of this seat
                    checkSeat = seatInfo.Where(s => s.X < seat.X && s.Y > seat.Y && (Math.Abs(s.X - seat.X) == Math.Abs(s.Y - seat.Y)) && s.State > 0).OrderByDescending(s => s.X).ToList();
                    if (checkSeat.Count() > 0)
                    {
                        if (checkSeat.First().State == 2)
                        {
                            nrOfSeatsAdjacentOccupied++;
                        }
                    }






                    //If seat is empty and No occupied seats adjacent to it ==> Seat becomes occupied
                    if (seat.State == 1 && nrOfSeatsAdjacentOccupied == 0)
                    {
                        layoutChanged = true;
                        newSeat.State = 2;
                    }

                    //If seat is occupied  and 4 or more seats adjacent to it are occupied ==> Seat becomes empty
                    if (seat.State == 2 && nrOfSeatsAdjacentOccupied >= 5)
                    {
                        layoutChanged = true;
                        newSeat.State = 1;
                    }
                }

                newSeatInfo.Add(newSeat);

            }
            return (newSeatInfo, layoutChanged);
        }

        public (List<SeatInfo>, bool) UpdateSeatLayout(List<SeatInfo> seatInfo)
        {
            List<SeatInfo> newSeatInfo = new List<SeatInfo>();
            SeatInfo newSeat = new SeatInfo();


            int xMin = 0;
            int xMax = 0;
            int yMin = 0;
            int yMax = 0;
            int nrOfSeatsAdjacentOccupied = 0;
            bool layoutChanged = false;

            foreach (SeatInfo seat in seatInfo)
            {
                newSeat.X = seat.X;
                newSeat.Y = seat.Y;
                newSeat.State = seat.State;

                if (seat.State > 0)
                {
                    xMin = seat.X - 1;
                    xMax = seat.X + 1;
                    yMin = seat.Y - 1;
                    yMax = seat.Y + 1;

                    nrOfSeatsAdjacentOccupied = seatInfo.Count(s => s.X <= xMax && s.X >= xMin && s.Y <= yMax && s.Y >= yMin && s.State == 2);

                    //If seat is empty and No occupied seats adjacent to it ==> Seat becomes occupied
                    if (seat.State == 1 && nrOfSeatsAdjacentOccupied == 0)
                    {
                        layoutChanged = true;
                        newSeat.State = 2;
                    }

                    //If seat is occupied  and 4 or more seats adjacent to it are occupied ==> Seat becomes empty
                    if (seat.State == 2 && nrOfSeatsAdjacentOccupied > 4) //Included this seat so no >=
                    {
                        layoutChanged = true;
                        newSeat.State = 1;
                    }
                }
                newSeatInfo.Add(newSeat);
            }

            return (newSeatInfo, layoutChanged);
        }

        public List<SeatInfo> ReadSeatLayout(string input)
        {
            List<SeatInfo> seatInfo = new List<SeatInfo>();
            int x = 0;
            int y = 0;
            int state;
            SeatInfo oneSeat = new SeatInfo();

            //Convert input to array of integers.
            string[] lines = input.Split(Environment.NewLine);

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
                            break;
                    }

                    oneSeat.X = x;
                    oneSeat.Y = y;
                    oneSeat.State = state;

                    seatInfo.Add(oneSeat);

                    x++;
                }

                y++;
                // break;
            }
            return seatInfo;

        }

        public struct SeatInfo
        {
            public int X;
            public int Y;
            public int State; //0 = floor, 1 = empty seat, 2 = occupied seat 
        }

        public string GetInput(bool testInput)
        {

            var myInput = new Inputs.Day11();
            return (testInput) ? myInput.testInput : myInput.input;
        }

    }

}
