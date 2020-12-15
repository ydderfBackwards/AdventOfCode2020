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

            string mask;
            long maskX0 = 0;
            long maskX1 = 0;
            long maskX = 0;
            long maskXinv = 0;
            long memAdres;
            long memValue;
            long baseValue;
            Memory tempMemory;
            int numberOffOffsets = 0;
            var values = new long[20]; //20 items should be enough.......

            var memory = new List<Memory>();

            foreach (string line in lines)
            {
                if (line.Contains("mask"))
                {
                    string[] data = line.Split(" = ");

                    mask = data[1];
                    maskX0 = Convert.ToInt64(mask.Replace('X', '0'), 2);
                    maskX1 = Convert.ToInt64(mask.Replace('X', '1'), 2);


                    //Calculate new value with mask.
                    maskX = maskX0 ^ maskX1;
                    maskXinv = ~maskX; //Invert bits

                    string maskX_string = "";
                    maskX_string = Convert.ToString(maskX, 2);

                    // Console.WriteLine(mask);
                    // Console.WriteLine(maskX0);
                    // Console.WriteLine(maskX1);
                    // Console.WriteLine(maskX);
                    // Console.WriteLine(maskX_string);

                    values = new long[20];

                    int loopCount = 0;
                    int charCount = 1;

                    //Save all possible offset values
                    foreach (char maskValue in maskX_string)
                    {
                        if (maskValue == '1')
                        {
                            values[loopCount] = (long)(Math.Pow(2, (maskX_string.Length - charCount)));
                            loopCount++;
                        }
                        charCount++;
                    }

                    numberOffOffsets = loopCount;

                    // foreach (long value in values)
                    // {
                    //     if (value > 0) { Console.WriteLine("Value = {0}", value); }

                    // }




                }
                else
                {
                    string[] separatingStrings = { "mem[", "] = " };
                    string[] data = line.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);

                    memAdres = uint.Parse(data[0]);
                    memValue = uint.Parse(data[1]);

                    //Calculate new value with mask.
                    long tempValue = memAdres | maskX0;
                    baseValue = tempValue & maskXinv;

                    int nrOffPossibilities = (int)Math.Pow(2, numberOffOffsets);
                    if (nrOffPossibilities > 262144) { Console.WriteLine("NR pos: {0}", nrOffPossibilities); }

                    for (int i = 0; i < nrOffPossibilities; i++)
                    {
                        long offset = 0;

                        if ((i & 1) > 0) { offset += values[0]; } //0001
                        if ((i & 2) > 0) { offset += values[1]; } //0010
                        if ((i & 4) > 0) { offset += values[2]; } //0011
                        if ((i & 8) > 0) { offset += values[3]; }
                        if ((i & 16) > 0) { offset += values[4]; }
                        if ((i & 32) > 0) { offset += values[5]; }
                        if ((i & 64) > 0) { offset += values[6]; }
                        if ((i & 128) > 0) { offset += values[7]; }
                        if ((i & 256) > 0) { offset += values[8]; }
                        if ((i & 512) > 0) { offset += values[9]; }

                        if ((i & 1024) > 0) { offset += values[10]; }
                        if ((i & 2048) > 0) { offset += values[11]; }
                        if ((i & 4096) > 0) { offset += values[12]; }
                        if ((i & 8192) > 0) { offset += values[13]; }
                        if ((i & 16384) > 0) { offset += values[14]; }
                        if ((i & 32768) > 0) { offset += values[15]; }
                        if ((i & 65536) > 0) { offset += values[16]; }
                        if ((i & 131072) > 0) { offset += values[17]; }
                        if ((i & 262144) > 0) { offset += values[18]; }
                        if ((i & 524288) > 0) { offset += values[19]; }

                        memAdres = baseValue + offset;
                        //Console.WriteLine("Adres: {0}, offset: {1}, basevalue: {2}, Value: {3}", memAdres, offset, baseValue, memValue);

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
            }

            long total = 0;

            foreach (Memory mem in memory)
            {
                total += mem.value;
            }

            return total.ToString();


            //   return "ERROR: No program";

        }



        public string GetInput(bool testInput)
        {

            var myInput = new Inputs.Day14();
            return (testInput) ? myInput.testInput : myInput.input;
        }

    }



}
