using System;
using System.IO;
using System.Linq;
using System.Collections.Generic; //For list
using System.Text.RegularExpressions; // For regex

namespace AdventOfCode2020
{
    public class Day09
    {
        public string SolvePart1(string input)
        {

            //Convert input to array of long.
            long[] dataStream = Array.ConvertAll(input.Split(Environment.NewLine), long.Parse);

            return AttackXMAS(dataStream).ToString();
        }



        public string SolvePart2(string input)
        {
            //Convert input to array of long.
            long[] dataStream = Array.ConvertAll(input.Split(Environment.NewLine), long.Parse);

            //Get invalid number (result part 1)
            long invalidNr = AttackXMAS(dataStream);

            long weakness = FindEncryptionWeakness(invalidNr, dataStream);

            return weakness.ToString();

        }

        public long AttackXMAS(long[] dataStream)
        {
            long checkNumber = 0;
            int preambleSize = 25; //5 for test input, 25 for real input!
            long[] preambleData = new long[preambleSize];
            int i = 0;


            for (i = preambleSize; i < dataStream.Length - preambleSize; i++)
            {
                //Copy the numbers who should contain a valid set of numbers to a temp array
                Array.Copy(dataStream, i - preambleSize, preambleData, 0, preambleSize);

                if (!NrIsValid(dataStream[i], preambleData)) { break; }
            }

            checkNumber = dataStream[i];

            return checkNumber;
        }

        public bool NrIsValid(long checkNumber, long[] preambleData)
        {
            //Check if a combination of two number add up to checknumber

            bool valid = false;

            foreach (long number in preambleData)
            {
                if (preambleData.Contains((checkNumber - number)))
                {
                    valid = true;
                    break;
                }
            }
            return valid;
        }

        public long FindEncryptionWeakness(long InvalidNumber, long[] dataStream)
        {
            long result = 0;
            long sum = 0;
            int i = 0, j = 0;

            //Find datarange where all numbers add up to InvalidNumber
            for (i = 0; i < dataStream.Length; i++)
            {
                sum = 0;
                j = i;
                while (sum < InvalidNumber)
                {
                    sum += dataStream[j];
                    j++;
                }

                if (sum == InvalidNumber) { break; }
            }

            //Find min and max in data range and add them
            result = dataStream[i..j].Min() + dataStream[i..j].Max();

            return result;

        }

        public string GetInput(bool testInput)
        {

            var myInput = new Inputs.Day09();
            return (testInput) ? myInput.testInput : myInput.input;
        }

    }



}
