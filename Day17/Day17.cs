using System;
using System.IO;
using System.Linq;
using System.Collections.Generic; //For list
using System.Text.RegularExpressions; // For regex

namespace AdventOfCode2020
{
    public class Day17
    {
        public string SolvePart1(string input)
        {
            int cycleSetpoint = 6;

            //Convert input to array of integers.
            string[] lines = input.Split(Environment.NewLine);

            int x, y, z;
            int maxSizeX = (cycleSetpoint * 2) + lines[0].Length;
            int maxSizeY = (cycleSetpoint * 2) + lines.Length ;
            int maxSizeZ = (cycleSetpoint * 2) + 3 ;
            int centerX = maxSizeX / 2;
            int centerY = maxSizeY / 2;
            int centerZ = maxSizeZ / 2;


            int[,,] cubes = new int[maxSizeX, maxSizeY, maxSizeZ];


            //************** Read input ************
            y = centerY - (lines.Length / 2);

            foreach (string line in lines)
            {
                x = centerX - (lines[0].Length / 2);
                foreach (char letter in line)
                {
                    if (letter.Equals('#'))
                    {
                        cubes[x, y, centerZ] = 1;
                    }
                    x++;
                }
                y++;
            }

            // //************** Print input ************
            // for (y = 0; y < maxSizeY; y++)
            // {
            //     for (x = 0; x < maxSizeX; x++)
            //     {
            //         Console.Write(cubes[x, y, centerZ]);
            //     }
            //     Console.WriteLine("");
            // }
            // Console.WriteLine("---------------------------");

            int[,,] cubesOld = new int[maxSizeX, maxSizeY, maxSizeZ];


            //************** Run Cycle's ************
            for (int c = 1; c <= cycleSetpoint; c++)
            {
                //Copy array
                for (z = 0; z < maxSizeZ; z++)
                {

                    for (y = 0; y < maxSizeY; y++)
                    {
                        for (x = 0; x < maxSizeX; x++)
                        {
                            cubesOld[x, y, z] = cubes[x, y, z];
                        }
                    }
                }

                //Update array
                for (z = 1; z < maxSizeZ - 1; z++)
                {
                    for (y = 1; y < maxSizeY - 1; y++)
                    {
                        for (x = 1; x < maxSizeX - 1; x++)
                        {
                            var nrActive = CountActiveNeighbors(cubesOld, x, y, z);

                            if (nrActive == 3 || (cubesOld[x, y, z] == 1 && (nrActive == 2)))
                            {
                                cubes[x, y, z] = 1;
                            }
                            else
                            {
                                cubes[x, y, z] = 0;
                            }
                        }
                    }
                }
            }


            //************** Count number active cubes ************
            long totalActive = 0;
            for (z = 0; z < maxSizeZ; z++)
            {
                for (y = 0; y < maxSizeY; y++)
                {
                    for (x = 0; x < maxSizeX; x++)
                    {
                        totalActive += cubes[x, y, z];
                    }
                }
            }

            // //************** Print input ************
            // for (y = 0; y < maxSizeY; y++)
            // {
            //     for (x = 0; x < maxSizeX; x++)
            //     {
            //         Console.Write(cubes[x, y, centerZ]);
            //     }
            //     Console.WriteLine("");
            // }

            // Console.WriteLine("---------------------------");
            return totalActive.ToString();
        }

        public int CountActiveNeighbors(int[,,] cubes, int xCheck, int yCheck, int zCheck)
        {
            int count = 0;
            int x, y, z;

            for (z = zCheck - 1; z <= zCheck + 1; z++)
            {
                for (y = yCheck - 1; y <= yCheck + 1; y++)
                {
                    for (x = xCheck - 1; x <= xCheck + 1; x++)
                    {
                        count += cubes[x, y, z];
                    }
                }
            }

            count -= cubes[xCheck, yCheck, zCheck];

            return count;
        }


        public string SolvePart2(string input)
        {
            int cycleSetpoint = 6;

            //Convert input to array of integers.
            string[] lines = input.Split(Environment.NewLine);

            int x, y, z, w;
            int maxSizeX = (cycleSetpoint * 2) + lines[0].Length + 10;
            int maxSizeY = (cycleSetpoint * 2) + lines.Length + 10;
            int maxSizeZ = (cycleSetpoint * 2) + 3 + 10;
            int maxSizeW = (cycleSetpoint * 2) + 3 + 10;
            int centerX = maxSizeX / 2;
            int centerY = maxSizeY / 2;
            int centerZ = maxSizeZ / 2;
            int centerW = maxSizeW / 2;

            int[,,,] cubes = new int[maxSizeX, maxSizeY, maxSizeZ, maxSizeW];


            //************** Read input ************
            y = centerY - (lines.Length / 2);

            foreach (string line in lines)
            {
                x = centerX - (lines[0].Length / 2);
                foreach (char letter in line)
                {
                    if (letter.Equals('#'))
                    {
                        cubes[x, y, centerZ, centerW] = 1;
                    }
                    x++;
                }
                y++;
            }

            // //************** Print input ************
            // for (y = 0; y < maxSizeY; y++)
            // {
            //     for (x = 0; x < maxSizeX; x++)
            //     {
            //         Console.Write(cubes[x, y, centerZ]);
            //     }
            //     Console.WriteLine("");
            // }
            // Console.WriteLine("---------------------------");

            int[,,,] cubesOld = new int[maxSizeX, maxSizeY, maxSizeZ, maxSizeW];


            //************** Run Cycle's ************
            for (int c = 1; c <= cycleSetpoint; c++)
            {
                //Copy array
                for (w = 0; w < maxSizeW; w++)
                {
                    for (z = 0; z < maxSizeZ; z++)
                    {
                        for (y = 0; y < maxSizeY; y++)
                        {
                            for (x = 0; x < maxSizeX; x++)
                            {
                                cubesOld[x, y, z, w] = cubes[x, y, z, w];
                            }
                        }
                    }
                }

                //Update array
                for (w = 1; w < maxSizeW - 1; w++)
                {
                    for (z = 1; z < maxSizeZ - 1; z++)
                    {
                        for (y = 1; y < maxSizeY - 1; y++)
                        {
                            for (x = 1; x < maxSizeX - 1; x++)
                            {
                                var nrActive = CountActiveNeighbors_4D(cubesOld, x, y, z, w);

                                if (nrActive == 3 || (cubesOld[x, y, z, w] == 1 && (nrActive == 2)))
                                {
                                    cubes[x, y, z, w] = 1;
                                }
                                else
                                {
                                    cubes[x, y, z, w] = 0;
                                }
                            }
                        }
                    }
                }
            }


            //************** Count number active cubes ************
            long totalActive = 0;
            for (w = 0; w < maxSizeW; w++)
            {
                for (z = 0; z < maxSizeZ; z++)
                {
                    for (y = 0; y < maxSizeY; y++)
                    {
                        for (x = 0; x < maxSizeX; x++)
                        {
                            totalActive += cubes[x, y, z, w];
                        }
                    }
                }
            }

            // //************** Print input ************
            // for (y = 0; y < maxSizeY; y++)
            // {
            //     for (x = 0; x < maxSizeX; x++)
            //     {
            //         Console.Write(cubes[x, y, centerZ]);
            //     }
            //     Console.WriteLine("");
            // }

            // Console.WriteLine("---------------------------");
            return totalActive.ToString();

        }


        public int CountActiveNeighbors_4D(int[,,,] cubes, int xCheck, int yCheck, int zCheck, int wCheck)
        {
            int count = 0;
            int x, y, z, w;

            for (w = wCheck - 1; w <= wCheck + 1; w++)
            {
                for (z = zCheck - 1; z <= zCheck + 1; z++)
                {
                    for (y = yCheck - 1; y <= yCheck + 1; y++)
                    {
                        for (x = xCheck - 1; x <= xCheck + 1; x++)
                        {
                            count += cubes[x, y, z, w];
                        }
                    }
                }
            }
            count -= cubes[xCheck, yCheck, zCheck, wCheck];

            return count;
        }


        public string GetInput(bool testInput)
        {
            var myInput = new Inputs.Day17();
            return (testInput) ? myInput.testInput : myInput.input;
        }

    }

}
