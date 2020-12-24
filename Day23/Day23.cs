using System;
using System.IO;
using System.Linq;
using System.Collections.Generic; //For list
using System.Text.RegularExpressions; // For regex

namespace AdventOfCode2020
{
    public class Day23
    {
        public string SolvePart1(string input)
        {

            long[] cups = input.Select(x => long.Parse(x.ToString())).ToArray();
            long movesToDo = 100;
            long posCurrentCup = 0;

            for (long move = 1; move <= movesToDo; move++)
            {
                long[] cupsPickedUp = PickUpCups(cups, posCurrentCup);
                long destinationCup = DestinationCup(cups, posCurrentCup, cupsPickedUp);
                cups = PlaceCups(cups, posCurrentCup, cupsPickedUp, destinationCup);

                posCurrentCup++;
                posCurrentCup = CheckLimits(posCurrentCup, 0, 8);

            }

            return GetCupOrder(cups);
        }

        public string SolvePart2(string input)
        {
            int[] cups = new int[1000000 + 1]; //Add one because we don't use zero (so cup 45 is at index 45)

            bool firstScan = true;
            int previousNumber = 0;
            int firstNumber = 0;
            //Loop to input
            foreach (var c in input)
            {
                int number = int.Parse(c.ToString());
                if (!firstScan)
                {
                    cups[previousNumber] = number;
                }
                else
                {
                    firstNumber = number;
                    firstScan = false;
                }
                previousNumber = number;
            }

            //Add the remaining number
            for (int i = 10; i <= 1000000; i++)
            {
                cups[previousNumber] = i;
                previousNumber = i;
            }

            cups[previousNumber] = firstNumber; //The first number comes after the last one (is a circle)

            //************ Start game part ***********/
            long movesToDo = 10000000;
            int[] cupsPickedUp = new int[3];
            int actCup = firstNumber; //Start with the first number

            for (long move = 1; move <= movesToDo; move++)
            {
                //Get the next three cups after the actual cup.
                cupsPickedUp[0] = cups[actCup];
                cupsPickedUp[1] = cups[cupsPickedUp[0]];
                cupsPickedUp[2] = cups[cupsPickedUp[1]];


                //Determine destination cup.
                int destinationCup = actCup - 1;
                if (destinationCup < 1) { destinationCup = cups.Max(); }
                while (cupsPickedUp.Any(x => x == destinationCup))
                {
                    destinationCup--;
                    if (destinationCup < 1) { destinationCup = cups.Max(); }
                }

                //Insert the three remove cups after the destination cup
                cups[actCup] = cups[cupsPickedUp[2]]; //The 3 cups after the actual cup will be removed -> so the next one after the actual cup, will be the one after the third removed cup
                cups[cupsPickedUp[2]] = cups[destinationCup]; //The 3 cups will be inserted after the destination cup. So the one after the destination cup will now be after the 3 cups
                cups[destinationCup] = cupsPickedUp[0]; //The first of the 3 cups will be after the destination cup.

                actCup = cups[actCup]; //The actual cup for the next move, is the one after the actual cup of the last move

            }

            long result = (long)cups[1] * (long)cups[cups[1]];
            return result.ToString();

        }


        public string GetCupOrder(long[] cups)
        {
            string cupOrder = "";
            long posCupOne = Array.FindIndex(cups, x => x == 1);

            long posCopy = posCupOne + 1;
            posCopy = CheckLimits((posCopy), 0, cups.Length - 1);
            while (posCupOne != posCopy) //Copy cups until the destination cup
            {
                cupOrder += cups[posCopy].ToString();
                posCopy++;
                posCopy = CheckLimits((posCopy), 0, cups.Length - 1);
            }

            return cupOrder;
        }

        public long[] PlaceCups(long[] cups, long posCurrentCup, long[] cupsPickedUp, long destinationCup)
        {


            long[] newCups = new long[cups.Length];
            long posDestinationCup = Array.FindIndex(cups, x => x == destinationCup);

            long posNewCups = posCurrentCup;
            long posOldCups = posCurrentCup;
            newCups[posNewCups] = cups[posOldCups]; //Current cup remains always on the same position


            posOldCups = posOldCups + 3;//Skip the three picked up cups
            posOldCups = CheckLimits(posOldCups, 0, cups.Length - 1);

            while (posOldCups != posDestinationCup) //Copy cups until the destination cup
            {
                posNewCups++;
                posOldCups++;
                posNewCups = CheckLimits(posNewCups, 0, cups.Length - 1);
                posOldCups = CheckLimits((posOldCups), 0, cups.Length - 1);
                newCups[posNewCups] = cups[posOldCups];
            }


            for (long i = 0; i < 3; i++)    //Insert the destination cups
            {
                posNewCups++;
                posNewCups = CheckLimits(posNewCups, 0, cups.Length - 1);
                newCups[posNewCups] = cupsPickedUp[i];
            }

            while (posOldCups != posCurrentCup) //Copy the remaining cups
            {
                posNewCups++;
                posOldCups++;
                posNewCups = CheckLimits(posNewCups, 0, cups.Length - 1);
                posOldCups = CheckLimits((posOldCups), 0, cups.Length - 1);
                newCups[posNewCups] = cups[posOldCups];
            }

            return newCups;
        }

        public long DestinationCup(long[] cups, long posCurrentCup, long[] cupsPickedUp)
        {
            long destinationCup = cups[posCurrentCup] - 1;
            destinationCup = CheckLimits(destinationCup, 1, cups.Length);

            while (cupsPickedUp.Contains(destinationCup))
            {
                destinationCup = CheckLimits((destinationCup - 1), 1, cups.Length);
            }

            return destinationCup;
        }

        public long[] PickUpCups(long[] cups, long posCurrentCup)
        {
            long[] cupsPickedUp = new long[3];
            long pickupPos = posCurrentCup;

            for (long i = 0; i < cupsPickedUp.Length; i++)
            {
                pickupPos++;
                pickupPos = CheckLimits(pickupPos, 0, cups.Length - 1);
                cupsPickedUp[i] = cups[pickupPos];
            }
            return cupsPickedUp;
        }

        public long CheckLimits(long value, long lowLim, long highLim)
        {
            if (value < lowLim) { return highLim - (lowLim - value) + 1; }
            if (value > highLim) { return lowLim + (value - highLim) - 1; }
            return value;
        }





        public string GetInput(bool testInput)
        {

            var myInput = new Inputs.Day23();
            return (testInput) ? myInput.testInput : myInput.input;
        }

    }



}
