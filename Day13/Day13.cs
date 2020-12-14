using System;
using System.IO;
using System.Linq;
using System.Collections.Generic; //For list
using System.Text.RegularExpressions; // For regex

namespace AdventOfCode2020
{
    public class Day13
    {
        public string SolvePart1(string input)
        {
            var (earliestTimestamp, BusIDs) = ReadNotes(input);

            var (BusID, WaitingTime) = FindEarliestBus(earliestTimestamp, BusIDs);

            return (BusID * WaitingTime).ToString();
        }



        public string SolvePart2(string input)
        {
            var  BusIDs = ReadNotes_Part2(input);

            double result = FindTimeStamp(BusIDs);
            return result.ToString();

        }

        public (int, int) FindEarliestBus(int earliestTime, List<int> busIDs)
        {
            int busID;
            int shortestWaitingTime = 999999999;

            busID = busIDs.Select(b => earliestTime % b).Min();

            foreach (int bus in busIDs)
            {
                int nrCompleteRuns = earliestTime / bus;
                int waitingTime = ((nrCompleteRuns + 1) * bus) - earliestTime;

                if (waitingTime < shortestWaitingTime)
                {
                    shortestWaitingTime = waitingTime;
                    busID = bus;
                }
            }

            return (busID, shortestWaitingTime);
        }


        public (int, List<int>) ReadNotes(string input)
        {
            int earliestTimestamp = 0;
            List<int> BusIDs = new List<int>();

            //Convert input to array of integers.
            string[] lines = input.Split(Environment.NewLine);

            //First line contains the earliest Timestamp. 
            earliestTimestamp = int.Parse(lines[0]);

            //Second line contains bus ID's
            string[] items = lines[1].Split(',');

            int BusID;
            foreach (string item in items)
            {
                if (int.TryParse(item, out BusID))
                {
                    BusIDs.Add(BusID);
                }
            }

            return (earliestTimestamp, BusIDs);
        }


        public double FindTimeStamp(List<BusData> busData)
        {
            double interfall = 1;
            double timestamp = 1;

            //For all busses
            foreach (BusData bus in busData)
            {
                //Check if the actual timestamp matches with the bus ID
                while ((timestamp + bus.timeOffset) % bus.BusID != 0)
                {
                    //If not found, add interfall to timestamp
                    timestamp += interfall;
                }
                //Multiply interfall with busID when the bus is found (a valid timestamp should be a factor of this busID and all previous busID)
                interfall *= bus.BusID;

                // Console.WriteLine("Found valid on timestamp: {0}", timestamp);
                // Console.WriteLine("New interfall: {0}", interfall);
            }
            return timestamp;
        }



        public List<BusData> ReadNotes_Part2(string input)
        {
            List<BusData> BusIDs = new List<BusData>();

            //Convert input to array of integers.
            string[] lines = input.Split(Environment.NewLine);


            //Second line contains bus ID's
            string[] items = lines[1].Split(',');

            int BusID;
            int timeOffset = 0;
            BusData busData = new BusData();
          
            foreach (string item in items)
            {
                if (int.TryParse(item, out BusID))
                {
                    busData.BusID = BusID;
                    busData.timeOffset = timeOffset;

                    BusIDs.Add(busData);
                }
                timeOffset++;
            }

            return BusIDs;
        }

        public struct BusData
        {
            public int BusID;
            public int timeOffset;

        }
        public string GetInput(bool testInput)
        {

            var myInput = new Inputs.Day13();
            return (testInput) ? myInput.testInput : myInput.input;
        }

    }



}
