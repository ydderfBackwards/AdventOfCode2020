using System;
using System.IO;
using System.Linq;
using System.Collections.Generic; //For list
using System.Text.RegularExpressions; // For regex

namespace AdventOfCode2020
{
    public class Day24
    {
        public string SolvePart1(string input)
        {
            var directionsTiles = ReadInput(input);
            var tiles = PlaceBlackTiles(directionsTiles);
            int nrBlackTiles = tiles.Count();

            return nrBlackTiles.ToString();
        }

        public string SolvePart2(string input)
        {
            var directionsTiles = ReadInput(input);
            var blackTiles = PlaceBlackTiles(directionsTiles);

            for (int day = 1; day <= 100; day++)
            {
                blackTiles = FlipTiles(blackTiles);
            }

            int nrBlackTiles = blackTiles.Count();
            Console.WriteLine(nrBlackTiles);
            return nrBlackTiles.ToString();

        }

        public HashSet<Location> FlipTiles(HashSet<Location> blackTiles)
        {
            HashSet<Location> newBlackTiles = new HashSet<Location>(blackTiles); //New hashset with data of existing hashset

            //Make the floor one tile larger than the outer black tiles
            int xMin = blackTiles.Min(t => t.x);
            int xMax = blackTiles.Max(t => t.x);
            int yMin = blackTiles.Min(t => t.y);
            int yMax = blackTiles.Max(t => t.y);

            for (int x = xMin - 1; x <= xMax + 1; x++)
            {
                for (int y = yMin - 1; y <= yMax + 1; y++)
                {
                    int nrAdjBlack = blackTiles.Where(t =>
                                                ((t.y == y && (t.x == x + 1 || t.x == x - 1)) || //Horizontal connected
                                                (t.y == y + 1 && (t.x == x - 1 || t.x == x)) || //South connected
                                                (t.y == y - 1 && (t.x == x || t.x == x + 1)))//North connected
                                            ).Count();

                    //Check if its a black tile
                    if (blackTiles.Any(t => t.x == x && t.y == y))
                    {
                        if (nrAdjBlack == 0 || nrAdjBlack > 2)
                        {
                            //Tile is black but will be flipped to white 
                            Location newBlackTile = new Location();
                            newBlackTile.x = x;
                            newBlackTile.y = y;
                            newBlackTiles.Remove(newBlackTile);
                        }
                    }
                    else
                    {
                        //If white tile with two black adjacent --> flip to black
                        if (nrAdjBlack == 2)
                        {
                            Location newBlackTile = new Location();
                            newBlackTile.x = x;
                            newBlackTile.y = y;
                            newBlackTiles.Add(newBlackTile);
                        }
                    }
                }
            }
            return newBlackTiles;
        }



        public HashSet<Location> PlaceBlackTiles(List<Directions>[] directionsTiles)
        {
            HashSet<Location> blackTiles = new HashSet<Location>();

            foreach (List<Directions> directionTile in directionsTiles)
            {
                Location currentLocation = new Location();
                foreach (Directions direction in directionTile)
                {
                    currentLocation = Move(currentLocation, direction);
                }

                Location tile = new Location();
                tile.x = currentLocation.x;
                tile.y = currentLocation.y;


                if (blackTiles.Any(t => t.x == currentLocation.x && t.y == currentLocation.y))
                {
                    blackTiles.Remove(tile); 
                }
                else
                {
                    blackTiles.Add(tile);
                }
            }

            return blackTiles;
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
