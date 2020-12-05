using System;
using System.IO;
using System.Linq;
using System.Collections.Generic; //For list
using System.Text.RegularExpressions; // For regex

namespace AdventOfCode2020
{
    public class Day05
    {
        public string SolvePart1(string input)
        {
            SeatData seatData = new SeatData();

            //Convert input to array of integers.
            string[] boardingpasses = input.Split(Environment.NewLine);

            int maxSeatID = 0;

            foreach (string boardingpass in boardingpasses)
            {
                seatData = ReadBoardingPass(boardingpass);

                //Check if this seatID is the heighest seatID.
                maxSeatID = (seatData.seatID > maxSeatID) ? seatData.seatID : maxSeatID;

            }

            return maxSeatID.ToString();
        }


        public string SolvePart2(string input)
        {
            //Convert input to array of integers.
            string[] lines = input.Split(Environment.NewLine);

            //SeatData seatData = new SeatData();
            var seatData = new List<SeatData>();


            //Convert input to array of integers.
            string[] boardingpasses = input.Split(Environment.NewLine);

            int seatID = 0;


            foreach (string boardingpass in boardingpasses)
            {
                seatData.Add(ReadBoardingPass(boardingpass));
            }



            seatID = FindSeat(seatData);



            return seatID.ToString();

        }

        public int FindSeat(List<SeatData> seatData)
        {
            //Finds free seat in list of boardingpasses

            //Sort data on seat ID
            seatData.Sort((s1, s2) => s1.seatID.CompareTo(s2.seatID));

            int previousSeatID = 999999;
  
            //Loop through the sorted list of seatData (sorted bij seatID)
            foreach (SeatData seat in seatData)
            {
                //If the previous SeatID is 2 less than the current seat --> Then there is exactly one empty seat between them. 
                if (seat.seatID == previousSeatID +2)
                {
                    //Current seat minus one is the free seat
                    return seat.seatID - 1;
                }
                else
                {
                    //Save seatID for next check
                    previousSeatID = seat.seatID;
                }
            }

            return 0;
        }



        public SeatData ReadBoardingPass(string boardingpass)
        {
            //Convert data (binary space partitioning) from boardingpass to row, column and seatID

            SeatData seatData = new SeatData();

            seatData.row = (boardingpass[0].CompareTo('F') == 0) ? 0 : 64;
            seatData.row += (boardingpass[1].CompareTo('F') == 0) ? 0 : 32;
            seatData.row += (boardingpass[2].CompareTo('F') == 0) ? 0 : 16;
            seatData.row += (boardingpass[3].CompareTo('F') == 0) ? 0 : 8;
            seatData.row += (boardingpass[4].CompareTo('F') == 0) ? 0 : 4;
            seatData.row += (boardingpass[5].CompareTo('F') == 0) ? 0 : 2;
            seatData.row += (boardingpass[6].CompareTo('F') == 0) ? 0 : 1;

            seatData.column = (boardingpass[7].CompareTo('L') == 0) ? 0 : 4;
            seatData.column += (boardingpass[8].CompareTo('L') == 0) ? 0 : 2;
            seatData.column += (boardingpass[9].CompareTo('L') == 0) ? 0 : 1;

            seatData.seatID = seatData.row * 8 + seatData.column;

            return seatData;
        }

        public struct SeatData
        {
            public int row;
            public int column;
            public int seatID;
        }





        public string GetInput(bool testInput)
        {

            var myInput = new Inputs.Day05();
            return (testInput) ? myInput.testInput : myInput.input;
        }

    }



}
