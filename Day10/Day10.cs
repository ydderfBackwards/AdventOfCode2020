using System;
using System.IO;
using System.Linq;
using System.Collections.Generic; //For list
using System.Text.RegularExpressions; // For regex

namespace AdventOfCode2020
{
    public class Day10
    {
        public string SolvePart1(string input)
        {

            //Convert input to array of integers.
            int[] joltages = Array.ConvertAll(input.Split(Environment.NewLine), int.Parse);

            //Sort array
            Array.Sort(joltages);

            int countDiff1 = CountAdaptersInChain(1, joltages);
            int countDiff3 = CountAdaptersInChain(3, joltages);
            int result = countDiff1 * countDiff3;

            return result.ToString();
        }



        public string SolvePart2(string input)
        {
            //Convert input to array of integers.
            int[] joltages = Array.ConvertAll(input.Split(Environment.NewLine), int.Parse);

            //Sort array
            Array.Sort(joltages);

            double result = NrOfWaysToOutlet(joltages);
            return result.ToString();

        }

        public double NrOfWaysToOutlet(int[] joltages)
        {
            List<int> commonPoints = new List<int>();
            double count = 1;

            int i;

            //Find positions where every route has to pass (difference with next is 3)
            for (i = 0; i < joltages.Length - 1; i++)
            {
                if (joltages[i + 1] - joltages[i] == 3)
                {
                    commonPoints.Add(i + 1);
                }

            }

            commonPoints.Add(i + 1);

            int fromPos = 0;
            int toPos = 0;

            //Calculate number of routes from point to point and multiply the results
            foreach (int commonPoint in commonPoints)
            {
                toPos = commonPoint;
                count = count * NrOfWaysPointToPoint(fromPos, toPos, joltages);
                fromPos = toPos;
            }


            return count;


        }


        public double NrOfWaysPointToPoint(int posStart, int posStop, int[] joltages)
        {
            //Takes very long with real input. After more than one hour aborted the function.....
            //Both test input work fine.....
            double count = 0;
            int nextPosMin = posStart + 1;
            int nextPosMax = posStart + 4;



            //Limit search area to array size
            if (nextPosMax > joltages.Length)
            {
                nextPosMax = joltages.Length;
            }

            //Check if we reached the outlet
            if (nextPosMin >= posStop)
            {
                //This is the endpoint (outlet)

                count++;
            }
            else
            {
                //Check if we can make a step with difference 1 
                if (joltages[nextPosMin..nextPosMax].Contains(joltages[posStart] + 1))
                {
                    count += NrOfWaysPointToPoint(Array.IndexOf(joltages, joltages[posStart] + 1), posStop, joltages);
                }

                //Check if we can make a step with difference 2
                if (joltages[nextPosMin..nextPosMax].Contains(joltages[posStart] + 2))
                {
                    count += NrOfWaysPointToPoint(Array.IndexOf(joltages, joltages[posStart] + 2), posStop, joltages);
                }
                //Check if we can make a step with difference 3
                if (joltages[nextPosMin..nextPosMax].Contains(joltages[posStart] + 3))
                {
                    count += NrOfWaysPointToPoint(Array.IndexOf(joltages, joltages[posStart] + 3), posStop, joltages);
                }
            }

            return count;

        }

        public int CountAdaptersInChain(int joltDiff, int[] joltages)
        {
            int count = 1;

            for (int i = 0; i < joltages.Length - 1; i++)
            {
                if ((joltages[i] + joltDiff) == joltages[i + 1])
                {
                    count++;
                }
            }

            return count;
        }

        public string GetInput(bool testInput)
        {

            var myInput = new Inputs.Day10();
            return (testInput) ? myInput.testInput : myInput.input;
        }

    }



}
