using System;
using System.IO;
using System.Linq;
using System.Collections.Generic; //For list
using System.Text.RegularExpressions; // For regex

namespace AdventOfCode2020
{
    public class Day20
    {
        public string SolvePart1(string input)
        {
            var tiles = ReadInput(input);
            var result = FindCornerTiles(tiles);

            return result.ToString();
        }



        public string SolvePart2(string input)
        {
            //Convert input to array of integers.
            string[] lines = input.Split(Environment.NewLine);


            return "ERROR: No program";

        }

        public long FindCornerTiles(Tile[] tiles)
        {
            long result = 1;
         
            foreach (Tile tile in tiles)
            {
                if (CountNrOfMachingSides(tiles, tile) == 2)
                {
                    result *= tile.tileNr;
                }
            }
            return result;
        }

        public int CountNrOfMachingSides(Tile[] tiles, Tile checkTile)
        {
            int count = 0;
            for (int tileNr = 0; tileNr < tiles.Length; tileNr++)
            {
                for (int checkSide = 0; checkSide < 8; checkSide++) //Check all 4 sides and 4 mirror sides
                {
                    for (int tileSide = 0; tileSide < 4; tileSide++) //Only have to check his sides, no mirror
                    {
                        if (tiles[tileNr].sides[tileSide] == checkTile.sides[checkSide] && tiles[tileNr].tileNr != checkTile.tileNr)
                        {
                            count++;
                        }
                    }
                }
            }

            return count;
        }

        public int[,] Rotate90Degree(int[,] source) //Not used.......
        {
            int size = 10; //source[0].Length;
            int[,] destination = new int[size, size];

            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    destination[size - y - 1, x] = source[x, y];
                }
            }

            return destination;
        }

        public Tile[] ReadInput(string input)
        {
            string[] data = input.Split(Environment.NewLine + Environment.NewLine);
            Tile[] tiles = new Tile[data.Length];


            for (int i = 0; i < data.Length; i++) //For each block of data
            {
                string[] lines = data[i].Split(Environment.NewLine);

                //Get tile nr.
                tiles[i].tileNr = int.Parse(lines[0][5..9]);
                tiles[i].tile = new int[10, 10];
                tiles[i].sides = new int[8];

                for (int j = 1; j < lines.Length; j++) //For each line in a block
                {
                    for (int k = 0; k < lines[j].Length; k++) //For each char in line
                    {
                        if (lines[j][k].Equals('#'))
                        {
                            tiles[i].tile[j - 1, k] = 1;
                        }
                    }
                }

                tiles[i].sides = CreateSides(tiles[i].tile);

            }
            return tiles;
        }


        public int[] CreateSides(int[,] tile)
        {
            int[] sides = new int[8]; //4 sides and 4 sides mirrored
            int number;

            number = 0;
            for (int i = 0; i < 10; i++)
            {
                number += Convert.ToInt32(Math.Pow(2, (double)i) * tile[0, i]);
            }
            sides[0] = number;

            number = 0;
            for (int i = 0; i < 10; i++)
            {
                number += Convert.ToInt32(Math.Pow(2, (double)i) * tile[0, 9 - i]);
            }
            sides[4] = number;


            number = 0;
            for (int i = 0; i < 10; i++)
            {
                number += Convert.ToInt32(Math.Pow(2, (double)i) * tile[9, i]);
            }
            sides[1] = number;

            number = 0;
            for (int i = 0; i < 10; i++)
            {
                number += Convert.ToInt32(Math.Pow(2, (double)i) * tile[9, 9 - i]);
            }
            sides[5] = number;


            number = 0;
            for (int i = 0; i < 10; i++)
            {
                number += Convert.ToInt32(Math.Pow(2, (double)i) * tile[i, 0]);
            }
            sides[2] = number;

            number = 0;
            for (int i = 0; i < 10; i++)
            {
                number += Convert.ToInt32(Math.Pow(2, (double)i) * tile[9 - i, 0]);
            }
            sides[6] = number;


            number = 0;
            for (int i = 0; i < 10; i++)
            {
                number += Convert.ToInt32(Math.Pow(2, (double)i) * tile[i, 9]);
            }
            sides[3] = number;

            number = 0;
            for (int i = 0; i < 10; i++)
            {
                number += Convert.ToInt32(Math.Pow(2, (double)i) * tile[9 - i, 9]);
            }
            sides[7] = number;


            return sides;

        }
        public struct Tile
        {
            public int tileNr;
            public int[,] tile;
            public int[] sides;
        }

        public string GetInput(bool testInput)
        {

            var myInput = new Inputs.Day20();
            return (testInput) ? myInput.testInput : myInput.input;
        }

    }



}
