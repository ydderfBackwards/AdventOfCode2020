using System;
using System.IO;
using System.Linq;
using System.Collections.Generic; //For list
using System.Text.RegularExpressions; // For regex

namespace AdventOfCode2020
{
    public class Day14
    {
        public string SolvePart1(string input)
        {

            //Convert input to array of integers.
            string[] lines = input.Split(Environment.NewLine);

            string mask;
            long maskX0 = 0;
            long maskX1 = 0;
            long memAdres;
            long memValue;
            long tempValue;
            Memory tempMemory;


            var memory = new List<Memory>();

            foreach (string line in lines)
            {
                if (line.Contains("mask"))
                {
                    string[] data = line.Split(" = ");

                    mask = data[1];
                    maskX0 = Convert.ToInt64(mask.Replace('X', '0'), 2);
                    maskX1 = Convert.ToInt64(mask.Replace('X', '1'), 2);

                    // Console.WriteLine(mask);
                    // Console.WriteLine(maskX0);
                    // Console.WriteLine(maskX1);

                }
                else
                {
                    string[] separatingStrings = { "mem[", "] = " };
                    string[] data = line.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);

                    memAdres = uint.Parse(data[0]);
                    memValue = uint.Parse(data[1]);

                    //Calculate new value with mask.
                    tempValue = memValue & maskX1;
                    memValue = tempValue | maskX0;

                    //Store value in list. First check for duplicate memory usage
                    if (memory.Where(m => m.adres == memAdres).Count() > 0)
                    {
                        //Remove double record
                        memory.RemoveAll(m => m.adres == memAdres);
                    }

                    tempMemory.adres = memAdres;
                    tempMemory.value = memValue;
                    memory.Add(tempMemory);
                }
            }

            long total = 0;

            foreach (Memory mem in memory)
            {
                total += mem.value;
            }

            return total.ToString();
        }

        public struct Memory
        {
            public long adres;
            public long value;
        }

        public string SolvePart2(string input)
        {
            //Convert input to array of integers.
            string[] lines = input.Split(Environment.NewLine);


            return "ERROR: No program";

        }



        public string GetInput(bool testInput)
        {

            var myInput = new Inputs.Day14();
            return (testInput) ? myInput.testInput : myInput.input;
        }

    }



}
