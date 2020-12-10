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

            //long result = NrOfWaysToOutlet_SLOW(0, joltages);
            int result = NrOfWaysToOutlet(joltages);
            return result.ToString();

        }

    public int NrOfWaysToOutlet(int[] joltages)
    {
        long[] groupOfOnes = new long[10];
        int count = 0;
        long nrOfOnes = 0;


        for(int i = 0; i<joltages.Length-1; i++)
        {
            if(joltages[i+1] - joltages[i] == 1)
            {
                nrOfOnes++;
            }
            else
            {
                groupOfOnes[nrOfOnes]++;
                nrOfOnes = 0;
            }
        }

        groupOfOnes[nrOfOnes]++; //For the last group at the end of the array
 

        Console.Write("The awnser for part 2 is: ");
        Console.Write("2^{0} * ", groupOfOnes[2]);      //Two ones after eachother --> You have two options (5,6,7 --> 5,6,7 or 5,7)
        Console.Write("4^{0} * ", groupOfOnes[3]);      //Three ones after eachother --> You have four options (5,6,7,8 --> 5,6,7,8 or 5,6,8 or 5,7,8 or 5,8)
        Console.WriteLine("7^{0}", groupOfOnes[4]);     //Four ones after eachother --> You have seven options

        
        Console.WriteLine("TODO: Calculate and return awnser without overflow......");
 

        return count;


    }


        public long NrOfWaysToOutlet_SLOW(int pos, int[] joltages)
        {
            //Takes very long with real input. After more than one hour aborted the function.....
            //Both test input work fine.....
            long count = 0;
            int nextPosMin = pos + 1;
            int nextPosMax = pos + 4;

            

            //Limit search area to array size
            if (nextPosMax > joltages.Length)
            {
                nextPosMax = joltages.Length;
            }

            //Check if we reached the outlet
            if (nextPosMin >= joltages.Length)
            {
                //This is the endpoint (outlet)
                
                count++;
            }
            else
            {
                //Check if we can make a step with difference 1 
                if (joltages[nextPosMin..nextPosMax].Contains(joltages[pos] + 1))
                {
                    count += NrOfWaysToOutlet_SLOW(Array.IndexOf(joltages, joltages[pos] + 1), joltages);
                }

                //Check if we can make a step with difference 2
                if (joltages[nextPosMin..nextPosMax].Contains(joltages[pos] + 2))
                {
                    count += NrOfWaysToOutlet_SLOW(Array.IndexOf(joltages, joltages[pos] + 2), joltages);
                }
                //Check if we can make a step with difference 3
                if (joltages[nextPosMin..nextPosMax].Contains(joltages[pos] + 3))
                {
                    count += NrOfWaysToOutlet_SLOW(Array.IndexOf(joltages, joltages[pos] + 3), joltages);
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
