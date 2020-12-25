using System;
using System.IO;
using System.Linq;
using System.Collections.Generic; //For list
using System.Text.RegularExpressions; // For regex

namespace AdventOfCode2020
{
    public class Day24tmp
    {
        public string SolvePart1(string input)
        {
            var directionsTiles = ReadInput(input);
            var tiles = PlaceTiles(directionsTiles);
            int nrBlackTiles = tiles.Where(t => t.black == true).Count();

            return nrBlackTiles.ToString();
        }



        public string SolvePart2(string input)
        {
            var directionsTiles = ReadInput(input);
            var tiles = PlaceTiles(directionsTiles);
            tiles = AddBorderTiles(tiles);
            tiles = FlipTiles(tiles, directionsTiles);

            // for (int day = 1; day <= 100; day++)
            // {
            //     tiles = FlipTiles(tiles, directionsTiles);
            //     int nrBlackTiles = tiles.Where(t => t.black == true).Count();
            //     Console.WriteLine(nrBlackTiles);
            // }

            int nrBlackTiles = tiles.Where(t => t.black == true).Count();
            Console.WriteLine(nrBlackTiles);
            //  return nrBlackTiles.ToString();
            return "";
        }

        public HashSet<Tile> AddBorderTiles(HashSet<Tile> tiles)
        {
            HashSet<Tile> newTiles = new HashSet<Tile>();
            for (int x = -110; x < 110; x++)
            {
                for (int y = -110; y < 110; y++)
                {
                    Tile newTile = new Tile();
                    newTile.x = x;
                    newTile.y = y;

                    if (tiles.Any(t => t.x == x && t.y == y))
                    {
                        newTile.black = tiles.Where(t => t.x == x && t.y == y).First().black;
                    }
                    else
                    {
                        newTile.black = false;
                    }

                    newTiles.Add(newTile);
                }
            }
            return newTiles;
        }

        public HashSet<Tile> FlipTiles(HashSet<Tile> tiles, List<Directions>[] directionsTiles)
        {
            HashSet<Tile> newTiles = new HashSet<Tile>();

            foreach (Tile tile in tiles)
            {
                Tile newTile = new Tile();
                newTile.x = tile.x;
                newTile.y = tile.y;
                newTile.black = tile.black;

                int nrAdjBlack = tiles.Where(t => t.black == true && 
                                                ((t.y == tile.y && (t.x == tile.x + 1 || t.x == tile.x - 1))  || //Horizontal connected
                                                (t.y == tile.y + 1 && (t.x == tile.x || t.x == tile.x + 1))  || //South connected
                                                (t.y == tile.y - 1 && (t.x == tile.x || t.x == tile.x + 1)) )//North connected
                                            ).Count();
               // Console.WriteLine("Black: {0}, nrAdjBlack: {1}", tile.black, nrAdjBlack);
                if (tile.black && (nrAdjBlack == 0 || nrAdjBlack > 2))
                {
                    newTile.black = false;
                    //Console.WriteLine(" changed To white");
                }
                if (tile.black == false && nrAdjBlack == 2)
                {
                    newTile.black = true;
                   // Console.WriteLine(" changed To black");
                }

                newTiles.Add(newTile);
            }

            // (t.y == tile.y && (t.x == tile.x + 1 || t.x == tile.x - 1)) ||
            // (t.y == tile.y + 1 && (t.x == tile.x + 1 || t.x == tile.x - 1)) ||
            // (t.y == tile.y - 1 && (t.x == tile.x + 1 || t.x == tile.x - 1))




            return newTiles;
        }



        public HashSet<Tile> PlaceTiles(List<Directions>[] directionsTiles)
        {

            HashSet<Tile> tiles = new HashSet<Tile>();

            foreach (List<Directions> directionTile in directionsTiles)
            {
                Location currentLocation = new Location();
                foreach (Directions direction in directionTile)
                {
                    currentLocation = Move(currentLocation, direction);
                }

                Tile tile = new Tile();
                tile.x = currentLocation.x;
                tile.y = currentLocation.y;
                tile.black = true;

                if (tiles.Any(t => t.x == currentLocation.x && t.y == currentLocation.y))
                {
                    bool isBlack = tiles.Where(t => t.x == currentLocation.x && t.y == currentLocation.y).First().black;
                    tiles.Remove(tile);
                    tile.black = !isBlack; //Flip tile
                }

                tiles.Add(tile);
            }

            return tiles;
        }


        public Location Move(Location currentLocation, Directions direction)
        {
            currentLocation.x = currentLocation.x + direction.x;
            currentLocation.y = currentLocation.y + direction.y;

            return currentLocation;
        }

        public List<Directions>[] ReadInput(string input)
        {
            string regex = (@"se|e|sw|w|nw|ne");
            string[] lines = input.Split(Environment.NewLine);

            List<Directions>[] directions = new List<Directions>[lines.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                directions[i] = new List<Directions>();
                foreach (Match match in Regex.Matches(lines[i], regex))
                {
                    Directions direction = new Directions();
                    switch (match.Value)
                    {
                        case "e":
                            direction.x = 1;
                            direction.y = 0;
                            break;

                        case "se":
                            direction.x = 0;
                            direction.y = 1;
                            break;

                        case "sw":
                            direction.x = -1;
                            direction.y = 1;
                            break;

                        case "w":
                            direction.x = -1;
                            direction.y = 0;
                            break;

                        case "nw":
                            direction.x = 0;
                            direction.y = -1;
                            break;

                        case "ne":
                            direction.x = 1;
                            direction.y = -1;
                            break;

                        default:
                            Console.WriteLine("ERROR! Invalid direction");
                            break;
                    }
                    directions[i].Add(direction);
                }
            }
            return directions;
        }

        public struct Directions
        {
            public int x;
            public int y;
        }

        public struct Location
        {
            public int x;
            public int y;
        }

        public struct Tile
        {
            public int x;
            public int y;
            public bool black;
        }

        public string GetInput(bool testInput)
        {
            var myInput = new Inputs.Day24();
            return (testInput) ? myInput.testInput : myInput.input;
        }

    }



}
