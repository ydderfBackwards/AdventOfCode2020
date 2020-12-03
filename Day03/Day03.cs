using System;
using System.IO;
using System.Linq;


namespace AdventOfCode2020
{
    public class Day03
    {
        public string SolvePart1(string input)
        {

            //Convert input to array of integers.
            string[] lines = input.Split(Environment.NewLine);

            int nrOfThrees = 0;

            int maxWidth = lines[0].Length - 1;
            int xPos = 0;

            Console.WriteLine(maxWidth);

            foreach (string line in lines)
            {
                if (xPos > maxWidth) { xPos = xPos - 1 - maxWidth; }

                if (line[xPos] == '#')
                {
                    nrOfThrees++;
                }
                xPos += 3;
            }

            return nrOfThrees.ToString();
        }



        public string SolvePart2(string input)
        {

            //Convert input to array of integers.
            string[] lines = input.Split(Environment.NewLine);

            long nrOfThrees = 0;

            int maxWidth = lines[0].Length - 1;
            int xPos = 0;
            long result = 1;


            foreach (string line in lines)
            {
                if (xPos > maxWidth) { xPos = xPos - 1 - maxWidth; }

                if (line[xPos] == '#')
                {
                    nrOfThrees++;
                }
                xPos += 1;
            }

            result = result * nrOfThrees;
            nrOfThrees = 0;
            xPos = 0;
            
            foreach (string line in lines)
            {
                if (xPos > maxWidth) { xPos = xPos - 1 - maxWidth; }

                if (line[xPos] == '#')
                {
                    nrOfThrees++;
                }
                xPos += 3;
            }

            result = result * nrOfThrees;
            nrOfThrees = 0;
            xPos = 0;

            foreach (string line in lines)
            {
                if (xPos > maxWidth) { xPos = xPos - 1 - maxWidth; }

                if (line[xPos] == '#')
                {
                    nrOfThrees++;
                }
                xPos += 5;
            }

            result = result * nrOfThrees;
            nrOfThrees = 0;
            xPos = 0;

            foreach (string line in lines)
            {
                if (xPos > maxWidth) { xPos = xPos - 1 - maxWidth; }

                if (line[xPos] == '#')
                {
                    nrOfThrees++;
                }
                xPos += 7;
            }

            result = result * nrOfThrees;
            nrOfThrees = 0;
            xPos = 0;
            bool toggle = true;

            foreach (string line in lines)
            {
                if (toggle)
                {
                    if (xPos > maxWidth) { xPos = xPos - 1 - maxWidth; }

                    if (line[xPos] == '#')
                    {
                        nrOfThrees++;
                    }
                    xPos += 1;
                }

                toggle = !toggle;
            }

            result = result * nrOfThrees;

            return result.ToString();
        }


        public string GetInput(bool testInput)
        {

            var myInput = new Inputs.Day03();
            return (testInput) ? myInput.testInput : myInput.input;
        }

    }



}
