using System;
using System.IO;
using System.Linq;
using System.Collections.Generic; //For list
using System.Text.RegularExpressions; // For regex
using AOC.Helpers;

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
            var tiles = ReadInput(input);
            var result = WriteMatchingSide(tiles);
            var image = CreateImage(tiles);

            var myInput = new Inputs.Day20();
            var seaMonster = GetSeaMonster(myInput.seaMonster);
            var roughWater = DetermineWaterRoughness(image, seaMonster);

            return roughWater.ToString();
        }

        public int DetermineWaterRoughness(int[,] image, int[,] seaMonster)
        {
            int monsterCount = 0;
            int rotation = 0;

            long nrOfHashtags = Array2D.CountArray(seaMonster, 1);
            int[] foundMonsters = new int[8];

            for (int i = 0; i < 8; i++) //4 rotations and the 4 mirror.
            {
                var checkMonster = Array2D.RotateCW(seaMonster, rotation);

                int xLength = image.GetLength(0) - checkMonster.GetLength(0);
                int yLength = image.GetLength(1) - checkMonster.GetLength(1);

                monsterCount = 0;
                for (int x = 0; x < xLength; x++)
                {
                    for (int y = 0; y < yLength; y++)
                    {
                        int tmpCount = 0;
                        for (int a = 0; a < checkMonster.GetLength(0); a++)
                        {
                            for (int b = 0; b < checkMonster.GetLength(1); b++)
                            {
                                if (checkMonster[a, b] == 1)
                                {
                                    if (image[x + a, y + b] >= 1)
                                    {
                                        tmpCount++;
                                    }
                                    else
                                    {
                                        goto GOTO_NEXT_MONSTER;
                                    }
                                }
                            }
                        }

                        if (tmpCount == nrOfHashtags)
                        {
                            monsterCount++;
                            for (int a = 0; a < checkMonster.GetLength(0); a++)
                            {
                                for (int b = 0; b < checkMonster.GetLength(1); b++)
                                {
                                    if (checkMonster[a, b] == 1)
                                    {
                                        if (image[x + a, y + b] == 1)
                                        {
                                            image[x + a, y + b] = 2;
                                        }
                                    }
                                }
                            }
                        }

                    GOTO_NEXT_MONSTER:;
                    }
                }
                if (monsterCount > 0)
                {
                    // Console.WriteLine(monsterCount);
                    goto GOTO_COUNT;
                }

                //Prepare for next monster layout
                rotation += 90;
                if (rotation > 270)
                {
                    seaMonster = Array2D.FlipHorizontal(seaMonster);
                    rotation = 0;
                }

            }

        GOTO_COUNT:
            int nrOfMonsters = monsterCount;
            long nrOfHashtagsTotal = Array2D.CountArray(image, 1);
            long roughWater = nrOfHashtagsTotal;

            return (int)roughWater;
        }

        public int FindStartCornerTile(Tile[] tiles)
        {
            int i = 0;
            int side1 = 0;
            int side2 = 0;
            bool firstFound = false;
            for (i = 0; i < tiles.Length; i++)
            {
                if (tiles[i].matchingSides == 2)
                {
                    for (int tileNr = 0; tileNr < tiles.Length; tileNr++)
                    {
                        for (int checkSide = 0; checkSide < 8; checkSide++) //Check all 4 sides and 4 mirror sides
                        {
                            for (int tileSide = 0; tileSide < 4; tileSide++) //Only have to check his sides, no mirror
                            {
                                if (tiles[i].sides[tileSide] == tiles[tileNr].sides[checkSide] && tiles[tileNr].tileNr != tiles[i].tileNr)
                                {
                                    if (firstFound == false)
                                    {
                                        side1 = tileSide;
                                        firstFound = true;
                                    }
                                    else
                                    {
                                        side2 = tileSide;
                                        goto GOTO_DONE;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            Console.WriteLine("ERROR: No corner found!");
        GOTO_DONE:

            if (side1 == 0 && side2 == 1)
            {
                //Rotate 90 degree for correct position
                tiles[i].tile = Array2D.RotateCW(tiles[i].tile, 90);
                return i;
            }


            if (side1 == 1 && side2 == 2)
            {
                return i; //tile is correct places for top left corner
            }


            if (side1 == 2 && side2 == 3)
            {
                tiles[i].tile = Array2D.RotateCW(tiles[i].tile, 270);
                return i;
            }

            return -1;

            //Side 0 => x = 0->9, y = 0  ==> Top side
            //Side 1 => x = 9, y = 0->9  ==> Right side
            //Side 2 => x = 9->0, y = 9  ==> Bottom side 
            //Side 3 => x = 0, y = 9->0  ==> Left side 


            //Side 4 => x = 9->0, y = 0  ==> Top side mirror
            //Side 5 => x = 9, y = 9->0  ==> Right side mirror
            //Side 6 => x = 0->9, y = 9  ==> Bottom side mirror
            //Side 7 => x = 0, y = 0->9  ==> Left side mirror
        }

        public int GetBottomSide(int[,] tile)
        {
            int number = 0;
            for (int i = 9; i >= 0; i--)
            {
                number += tile[i, 9] << i;
            }
            return number;
        }


        public int GetRightSide(int[,] tile)
        {
            int number = 0;
            for (int i = 0; i < 10; i++)
            {
                number += tile[9, i] << i;
            }
            return number;
        }

        public int[,] CreateImage(Tile[] tiles)
        {
            int maxSize = (int)Math.Sqrt(tiles.Length); ;

            int[,] imagePosition = new int[maxSize, maxSize]; //Memory to store the position of the tiles.

            //Find first tile in the top left corner
            int tileIndexPrev = FindStartCornerTile(tiles);
            int checkSide = GetRightSide(tiles[tileIndexPrev].tile);
            tiles[tileIndexPrev].matched = true;

            /*************************** Find the position of all tiles ***************************/
            //Find all other tile positions
            for (int y = 0; y < maxSize; y++)
            {
                for (int x = 0; x < maxSize; x++)
                {
                    if (x == 0 && y == 0)
                    {
                        imagePosition[x, y] = tileIndexPrev;
                        continue; //Skip 0,0 because its done before this loop
                    }

                    //Determine how many sides should be machable. For the corner = 2, for the other outer tiles = 3, for the middle tiles = 4
                    //So we don't match eg. a corner tile in the middle.
                    int nrSidesToMach = 4;
                    if (x == 0 || x == maxSize - 1) { nrSidesToMach--; }
                    if (y == 0 || y == maxSize - 1) { nrSidesToMach--; }

                    //The second column and more, match on the left/right side. The first column match on the bottom/top side.
                    if (x > 0)
                    {
                        //Match on the right side of the previous tile except on the first column;
                        tileIndexPrev = imagePosition[x - 1, y];
                        checkSide = GetRightSide(tiles[tileIndexPrev].tile);
                        var (foundIndex, foundSide) = MatchNextTile(tiles, checkSide, nrSidesToMach);

                        //Rotate and flip the found tile.
                        if (foundSide == 0 || foundSide == 4)
                        {
                            tiles[foundIndex].tile = Array2D.RotateCW(tiles[foundIndex].tile, 270);
                        }

                        if (foundSide == 1 || foundSide == 5)
                        {
                            tiles[foundIndex].tile = Array2D.RotateCW(tiles[foundIndex].tile, 180);
                        }

                        if (foundSide == 2 || foundSide == 6)
                        {
                            tiles[foundIndex].tile = Array2D.RotateCW(tiles[foundIndex].tile, 90);
                        }

                        if (foundSide < 4)
                        {
                            tiles[foundIndex].tile = Array2D.FlipHorizontal(tiles[foundIndex].tile);
                        }

                        //Store data
                        imagePosition[x, y] = foundIndex;
                        tiles[foundIndex].matched = true;
                    }
                    else
                    {
                        //Match on the tile above (= the previous one)
                        tileIndexPrev = imagePosition[x, y - 1];
                        checkSide = GetBottomSide(tiles[tileIndexPrev].tile);
                        var (foundIndex, foundSide) = MatchNextTile(tiles, checkSide, nrSidesToMach);

                        //Rotate and flip the found tile.
                        if (foundSide == 1 || foundSide == 5)
                        {
                            tiles[foundIndex].tile = Array2D.RotateCW(tiles[foundIndex].tile, 270);
                        }

                        if (foundSide == 2 || foundSide == 6)
                        {
                            tiles[foundIndex].tile = Array2D.RotateCW(tiles[foundIndex].tile, 180);
                        }

                        if (foundSide == 3 || foundSide == 7)
                        {
                            tiles[foundIndex].tile = Array2D.RotateCW(tiles[foundIndex].tile, 90);
                        }

                        if (foundSide > 3)
                        {
                            tiles[foundIndex].tile = Array2D.FlipVertical(tiles[foundIndex].tile);
                        }

                        //Store data
                        imagePosition[x, y] = foundIndex;
                        tiles[foundIndex].matched = true;
                    }
                }
            }

            /*************************** Create one big image of all tiles ***************************/
            int imageSize = maxSize * (tiles[0].tile.GetLength(0) - 2);
            int[,] image = new int[imageSize, imageSize];

            for (int y = 0; y < maxSize; y++)
            {
                for (int x = 0; x < maxSize; x++)
                {
                    int tileNr = imagePosition[x, y]; //Read tile number

                    //For all pixel in the tile except the outside row/column
                    for (int b = 1; b < tiles[0].tile.GetLength(0) - 1; b++)
                    {
                        for (int a = 1; a < tiles[0].tile.GetLength(1) - 1; a++)
                        {
                            int posX = x * 8 + a - 1;
                            int posY = y * 8 + b - 1;
                            image[posX, posY] = tiles[tileNr].tile[a, b];
                        }
                    }
                }
            }
            return image;
        }

        public (int, int) MatchNextTile(Tile[] tiles, int tileSide, int nrSidesToMach) //Find matching tile and rotate it correctly
        {
            int tileNrFound = 0;
            int tileSideFound = 0;

            for (int index = 0; index < tiles.Length; index++) //For all tiles
            {
                for (int checkSide = 0; checkSide < 8; checkSide++) //Check all 4 sides and 4 mirror sides
                {
                    if (tiles[index].sides[checkSide] == tileSide && tiles[index].matched == false && tiles[index].matchingSides == nrSidesToMach)
                    {
                        tileNrFound = index;
                        tileSideFound = checkSide;
                        goto GOTO_MATCH_FOUND;
                    }
                }
            }
            Console.WriteLine("ERROR! No matching tile found");

        GOTO_MATCH_FOUND:
            return (tileNrFound, tileSideFound);
        }

        public Tile[] WriteMatchingSide(Tile[] tiles) //Write the number of matching side to the tiles array
        {
            for (int i = 0; i < tiles.Length; i++)
            {
                tiles[i].matchingSides = CountNrOfMatchingSides(tiles, tiles[i]);
            }
            return tiles;
        }

        public long FindCornerTiles(Tile[] tiles) //Get the 4 corner tiles (for part 1)
        {
            long result = 1;

            foreach (Tile tile in tiles)
            {
                if (CountNrOfMatchingSides(tiles, tile) == 2)
                {
                    result *= tile.tileNr;
                }
            }
            return result;
        }

        public int CountNrOfMatchingSides(Tile[] tiles, Tile checkTile) //Count the number of matching sides 
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



        public int[] CreateSides(int[,] tile) //Create a int[] with a decimal number for each side of the array and each mirror side of the array
        {
            //Side 0 => x = 0->9, y = 0  ==> Top side
            //Side 1 => x = 9, y = 0->9  ==> Right side
            //Side 2 => x = 9->0, y = 9  ==> Bottom side 
            //Side 3 => x = 0, y = 9->0  ==> Left side 


            //Side 4 => x = 9->0, y = 0  ==> Top side mirror
            //Side 5 => x = 9, y = 9->0  ==> Right side mirror
            //Side 6 => x = 0->9, y = 9  ==> Bottom side mirror
            //Side 7 => x = 0, y = 0->9  ==> Left side mirror

            int[] sides = new int[8]; //4 sides and 4 sides mirrored


            for (int i = 0; i < 10; i++)
            {
                sides[0] += tile[i, 0] << i;
            }

            for (int i = 0; i < 10; i++)
            {
                sides[1] += tile[9, i] << i;
            }

            for (int i = 0; i < 10; i++)
            {
                sides[2] += tile[9 - i, 9] << i;
            }

            for (int i = 0; i < 10; i++)
            {
                sides[3] += tile[0, 9 - i] << i;
            }

            for (int i = 0; i < 10; i++)
            {
                sides[4] += tile[9 - i, 0] << i;
            }

            for (int i = 0; i < 10; i++)
            {
                sides[5] += tile[9, 9 - i] << i;
            }

            for (int i = 0; i < 10; i++)
            {
                sides[6] += tile[i, 9] << i;
            }

            for (int i = 0; i < 10; i++)
            {
                sides[7] += tile[0, i] << i;
            }

            return sides;

        }

        public int[,] GetSeaMonster(string input)
        {
            string[] data = input.Split(Environment.NewLine);
            int[,] seaMonter = new int[data.Length, data[0].Length];

            for (int j = 0; j < data.Length; j++) //For each line in a block
            {
                for (int k = 0; k < data[j].Length; k++) //For each char in line
                {
                    if (data[j][k].Equals('#'))
                    {
                        seaMonter[j, k] = 1;
                    }
                }
            }

            return seaMonter;

        }

        public Tile[] ReadInput(string input) //Read input and write to Tile array
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
                tiles[i].matchingSides = 0;
                tiles[i].matched = false;

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


        public struct Tile
        {
            public int tileNr; //The number as given at the input
            public int[,] tile; //The data of the tile
            public int[] sides; //A number of the binairy code of each side and the mirror of each side
            public int matchingSides; //The number of sides that match an other tile (eg. two means its a corner tile)
            public bool matched; //True if the tile is matched in the big image (prevention to use a tile twice)
        }

        public string GetInput(bool testInput)
        {
            var myInput = new Inputs.Day20();
            return (testInput) ? myInput.testInput : myInput.input;
        }

    }

}
