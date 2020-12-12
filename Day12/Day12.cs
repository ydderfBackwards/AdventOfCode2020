using System;
using System.IO;
using System.Linq;
using System.Collections.Generic; //For list
using System.Text.RegularExpressions; // For regex

namespace AdventOfCode2020
{
    public class Day12
    {
        public string SolvePart1(string input)
        {
            //Convert input to array of integers.
            string[] lines = input.Split(Environment.NewLine);

            NavInstruction[] navInstructions = new NavInstruction[lines.Length];
            Location location;

            //Define start location
            location.north = 0;
            location.east = 0;

            navInstructions = ReadNavigationInstructions(lines);
            location = Navigate(navInstructions, location, 'E');
            int result = Math.Abs(location.north) + Math.Abs(location.east);

            return result.ToString();
        }



        public string SolvePart2(string input)
        {
            //Convert input to array of integers.
            string[] lines = input.Split(Environment.NewLine);

            NavInstruction[] navInstructions = new NavInstruction[lines.Length];
            Location locationShip, locationWP;

            //Define start location
            locationShip.north = 0;
            locationShip.east = 0;
            locationWP.north = 1;
            locationWP.east = 10;

            navInstructions = ReadNavigationInstructions(lines);
            locationShip = Navigate_Part2(navInstructions, locationShip, 'E', locationWP);
            int result = Math.Abs(locationShip.north) + Math.Abs(locationShip.east);

            return result.ToString();
        }

        public Location Navigate_Part2(NavInstruction[] navInstructions, Location startlocationShip, char startDirectionShip, Location startLocationWayPoint)
        {
            //Function: Navigate ship depending on navigation instructions.
            //          Used for part 2

            Location locationShip = startlocationShip;
            Location locationWP = startLocationWayPoint;
            char direction = startDirectionShip;

            for (int i = 0; i < navInstructions.Length; i++)
            {
                switch (navInstructions[i].direction)
                {
                    case 'N':
                        locationWP.north += navInstructions[i].value;
                        break;

                    case 'E':
                        locationWP.east += navInstructions[i].value;
                        break;

                    case 'S':
                        locationWP.north -= navInstructions[i].value;
                        break;

                    case 'W':
                        locationWP.east -= navInstructions[i].value;
                        break;

                    case 'L':
                        locationWP = RotateWayPoint(navInstructions[i], locationWP);
                        break;

                    case 'R':
                        locationWP = RotateWayPoint(navInstructions[i], locationWP);
                        break;

                    case 'F':
                        locationShip = MoveToWayPoint(navInstructions[i], locationShip, locationWP);
                        break;
                }

            }


            return locationShip;

        }

        public Location MoveToWayPoint(NavInstruction navInstruction, Location locationShip, Location locationWP)
        {
            //Function: Move ship to waypoint and return new position of ship.

            Location newLocationShip = locationShip;

            newLocationShip.north += navInstruction.value * locationWP.north;
            newLocationShip.east += navInstruction.value * locationWP.east;

            return newLocationShip;
        }


        public Location RotateWayPoint(NavInstruction navInstruction, Location locationWP)
        {
            //Function: Rotate waypoint en return new location of waypoint. 

            Location newLocationWP = locationWP;

            if ((navInstruction.direction.Equals('L') && navInstruction.value == 90) || (navInstruction.direction.Equals('R') && navInstruction.value == 270))
            {
                newLocationWP.north = locationWP.east;
                newLocationWP.east = locationWP.north * -1;
            }

            if (navInstruction.value == 180) //Don't care about direction
            {
                newLocationWP.north = locationWP.north * -1;
                newLocationWP.east = locationWP.east * -1;
            }

            if ((navInstruction.direction.Equals('L') && navInstruction.value == 270) || (navInstruction.direction.Equals('R') && navInstruction.value == 90))
            {
                newLocationWP.north = locationWP.east * -1;
                newLocationWP.east = locationWP.north;
            }

            return newLocationWP;
        }

        public Location Navigate(NavInstruction[] navInstructions, Location startlocation, char startDirection)
        {
            //Function: Navigate ship depending on navigation instructions.
            //          Used for part 1

            Location location = startlocation;
            char direction = startDirection;

            for (int i = 0; i < navInstructions.Length; i++)
            {
                switch (navInstructions[i].direction)
                {
                    case 'N':
                        location.north += navInstructions[i].value;
                        break;

                    case 'E':
                        location.east += navInstructions[i].value;
                        break;

                    case 'S':
                        location.north -= navInstructions[i].value;
                        break;

                    case 'W':
                        location.east -= navInstructions[i].value;
                        break;

                    case 'L':
                        direction = ChangeDirection(navInstructions[i], direction);
                        break;

                    case 'R':
                        direction = ChangeDirection(navInstructions[i], direction);
                        break;

                    case 'F':

                        if (direction.Equals('N')) { location.north += navInstructions[i].value; }
                        if (direction.Equals('E')) { location.east += navInstructions[i].value; }
                        if (direction.Equals('S')) { location.north -= navInstructions[i].value; }
                        if (direction.Equals('W')) { location.east -= navInstructions[i].value; }

                        break;
                }
                //Console.WriteLine("east: {0} north: {1} - input: {2} - {3}", location.east, location.north, navInstructions[i].direction, navInstructions[i].value);
            }


            return location;

        }

        public char ChangeDirection(NavInstruction navInstruction, char direction)
        {
            //Function: Change direction depending on L or R and number of degrees

            char newDirection = direction;

            if (navInstruction.value == 0 || navInstruction.value == 360)
            {
                newDirection = direction;
            }
            else
            {
                switch (direction)
                {
                    case 'N':
                        if ((navInstruction.direction.Equals('L') && navInstruction.value == 90) || (navInstruction.direction.Equals('R') && navInstruction.value == 270)) { newDirection = 'W'; }
                        else if (navInstruction.value == 180) { newDirection = 'S'; }
                        else { newDirection = 'E'; }
                        break;

                    case 'E':
                        if ((navInstruction.direction.Equals('L') && navInstruction.value == 90) || (navInstruction.direction.Equals('R') && navInstruction.value == 270)) { newDirection = 'N'; }
                        else if (navInstruction.value == 180) { newDirection = 'W'; }
                        else { newDirection = 'S'; }
                        break;

                    case 'S':
                        if ((navInstruction.direction.Equals('L') && navInstruction.value == 90) || (navInstruction.direction.Equals('R') && navInstruction.value == 270)) { newDirection = 'E'; }
                        else if (navInstruction.value == 180) { newDirection = 'N'; }
                        else { newDirection = 'W'; }
                        break;

                    case 'W':
                        if ((navInstruction.direction.Equals('L') && navInstruction.value == 90) || (navInstruction.direction.Equals('R') && navInstruction.value == 270)) { newDirection = 'S'; }
                        else if (navInstruction.value == 180) { newDirection = 'E'; }
                        else { newDirection = 'N'; }
                        break;
                }
            }

            return newDirection;
        }

        public NavInstruction[] ReadNavigationInstructions(string[] lines)
        {
            //Function: reads input lines and convert to array of navigation instructions 

            NavInstruction[] navInstructions = new NavInstruction[lines.Length];
            int i = 0;

            foreach (string line in lines)
            {
                navInstructions[i].direction = line[0];
                navInstructions[i].value = int.Parse(line[1..]);
                i++;
            }

            return navInstructions;
        }

        public struct Location
        {
            public int east;
            public int north;
        }

        public struct NavInstruction
        {
            public char direction;
            public int value;
        }

        public string GetInput(bool testInput)
        {

            var myInput = new Inputs.Day12();
            return (testInput) ? myInput.testInput : myInput.input;
        }

    }



}
