using System;
using System.IO;
using System.Linq;
using System.Collections.Generic; //For list
using System.Text.RegularExpressions; // For regex

namespace AdventOfCode2020
{
    public class Day25
    {
        public string SolvePart1(string input)
        {
            string[] lines = input.Split(Environment.NewLine);

            long cardKey = int.Parse(lines[0]);
            long doorKey = int.Parse(lines[1]);

            var loopSizeCard = GetLoopSize(cardKey);
            var loopSizeDoor = GetLoopSize(doorKey);

            var encriptionKey = GetEncriptionKey(doorKey, loopSizeCard);

            return encriptionKey.ToString();
        }


        public long GetEncriptionKey(long key, long loopSize)
        {
            long value = 1;
            long divValue = 20201227;
            long subject = key;

            for (long i = 0; i < loopSize; i++)
            {
                value = (value * subject) % divValue;
            }

            return value;
        }

        public long GetLoopSize(long key)
        {
            long value = 1;
            long subject = 7;
            long divValue = 20201227;
            long loopSize = 0;

            while (value != key)
            {
                value = (value * subject) % divValue;
                loopSize++;
            }

            return loopSize;
        }

        public string SolvePart2(string input)
        {
            return "No part two!";
        }



        public string GetInput(bool testInput)
        {
            var myInput = new Inputs.Day25();
            return (testInput) ? myInput.testInput : myInput.input;
        }

    }



}
