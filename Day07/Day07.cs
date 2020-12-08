using System;
using System.IO;
using System.Linq;
using System.Collections.Generic; //For list
using System.Text.RegularExpressions; // For regex

namespace AdventOfCode2020
{
    public class Day07
    {
        public string SolvePart1(string input)
        {
            var bagRules = new Dictionary<string, ContainingBags>();
            var containingBags = new ContainingBags();
            var goodBags = new List<string>();
            var checkNextBags = new List<string>();
            var checkBags = new List<string>();

            //Convert input to array of integers.
            string[] lines = input.Split(Environment.NewLine);

            foreach (string line in lines)
            {
                //Reset values
                containingBags.bag1 = "";
                containingBags.bag2 = "";
                containingBags.bag3 = "";
                containingBags.bag4 = "";
                containingBags.bag5 = "";
                containingBags.bag6 = "";

                string[] data = line.Split("bags contain"); //Split line to string array

                string[] bags = data[1].Split(',');

                int bagNr = 1;

                foreach (string bag in bags)
                {
                    if (bag.Length > 0)
                    {
                        string bag2 = bag.Trim();
                        int i = bag2.IndexOf(' ') + 1; //Find first space
                        string str = bag2.Substring(i); //Remove items before space
                        i = str.LastIndexOf(' '); //Find last space
                        if (i > 0)
                        {
                            string str2 = str.Substring(0, i); //Remove items after last space

                            if (bagNr == 1) { containingBags.bag1 = str2; };
                            if (bagNr == 2) { containingBags.bag2 = str2; };
                            if (bagNr == 3) { containingBags.bag3 = str2; };
                            if (bagNr == 4) { containingBags.bag4 = str2; };
                            if (bagNr == 5) { containingBags.bag5 = str2; };
                            if (bagNr == 6) { containingBags.bag6 = str2; };
                            bagNr++;
                        }
                    }
                }

                string bagKey = data[0].Trim();


                //Add bag to dictionary
                bagRules.Add(bagKey, containingBags);
            }

            //Debug info:
            foreach (KeyValuePair<string, ContainingBags> bagRule in bagRules)
            {
                //Console.WriteLine("{0}: {1}, {2}, {3}, {4}, {5}, {6}", bagRule.Key.ToString(), bagRule.Value.bag1, bagRule.Value.bag2.ToString(), bagRule.Value.bag3.ToString(), bagRule.Value.bag4.ToString(), bagRule.Value.bag5.ToString(), bagRule.Value.bag6.ToString());
            }


            //************* Start searching and counting ***********//

            bool doneSeaching = false;

            //goodBags.Add("shiny gold");
            checkNextBags.Add("shiny gold");

            while (doneSeaching == false)
            {

                checkBags = checkNextBags.ToList();
                checkNextBags.Clear();

                foreach (string checkBag in checkBags)
                {
                    //Console.WriteLine("Seaching for: {0}",checkBag);
                    foreach (KeyValuePair<string, ContainingBags> bagRule in bagRules)
                    {
                        if (bagRule.Value.bag1.Contains(checkBag) || bagRule.Value.bag2.Equals(checkBag) || bagRule.Value.bag3.Equals(checkBag) || bagRule.Value.bag4.Equals(checkBag) || bagRule.Value.bag5.Equals(checkBag) || bagRule.Value.bag6.Equals(checkBag))
                        {
                            checkNextBags.Add(bagRule.Key.ToString());
                            goodBags.Add(bagRule.Key.ToString());
                            bagRules.Remove(bagRule.Key.ToString());
                        }

                    }

                }

                if (checkNextBags.Count() == 0)
                {
                    doneSeaching = true;
                }

            }

            int nrOfBags = goodBags.Count();
            return nrOfBags.ToString();
        }

        public struct ContainingBags
        {
            public string bag1;
            public string bag2;
            public string bag3;
            public string bag4;
            public string bag5;
            public string bag6;
        }

        public struct ContainingBagsNr
        {
            public string bag1;
            public string bag2;
            public string bag3;
            public string bag4;
            public string bag5;
            public string bag6;

            public int bagNr1;
            public int bagNr2;
            public int bagNr3;
            public int bagNr4;
            public int bagNr5;
            public int bagNr6;

        }

        public string SolvePart2(string input)
        {
            var bagRules = new Dictionary<string, ContainingBagsNr>();
            var containingBags = new ContainingBagsNr();
            var goodBags = new List<string>();
            var checkNextBags = new List<string>();
            var checkBags = new List<string>();

            //Convert input to array of integers.
            string[] lines = input.Split(Environment.NewLine);

            foreach (string line in lines)
            {
                //Reset values
                containingBags.bag1 = "";
                containingBags.bag2 = "";
                containingBags.bag3 = "";
                containingBags.bag4 = "";
                containingBags.bag5 = "";
                containingBags.bag6 = "";
                containingBags.bagNr1 = 0;
                containingBags.bagNr2 = 0;
                containingBags.bagNr3 = 0;
                containingBags.bagNr4 = 0;
                containingBags.bagNr5 = 0;
                containingBags.bagNr6 = 0;

                string[] data = line.Split("contain"); //Split line to string array

                string[] bags = data[1].Split(',');

                int bagNr = 1;

                foreach (string bag in bags)
                {
                    if (bag.Length > 0)
                    {
                        string bag2 = bag.Trim();
                        int i = bag2.IndexOf(' ') + 1; //Find first space

                        int nrOfBag = 0;
                        string strNr = bag2.Substring(0, i - 1);
                        bool isParsable = Int32.TryParse(strNr, out nrOfBag);

                        // if (!isParsable)
                        //     Console.WriteLine(number);
                        // else
                        //     Console.WriteLine("Could not be parsed.");

                        string str = bag2.Substring(i); //Remove items before space

                        i = str.LastIndexOf(' '); //Find last space
                        if (i > 0)
                        {
                            string str2 = str.Substring(0, i); //Remove items after last space

                            if (bagNr == 1) { containingBags.bag1 = str2; containingBags.bagNr1 = nrOfBag; };
                            if (bagNr == 2) { containingBags.bag2 = str2; containingBags.bagNr2 = nrOfBag; };
                            if (bagNr == 3) { containingBags.bag3 = str2; containingBags.bagNr3 = nrOfBag; };
                            if (bagNr == 4) { containingBags.bag4 = str2; containingBags.bagNr4 = nrOfBag; };
                            if (bagNr == 5) { containingBags.bag5 = str2; containingBags.bagNr5 = nrOfBag; };
                            if (bagNr == 6) { containingBags.bag6 = str2; containingBags.bagNr6 = nrOfBag; };
                            bagNr++;
                        }
                    }
                }

                string bagKey = data[0].Trim();

                //Add bag to dictionary
                bagRules.Add(bagKey, containingBags);
            }

            //Debug info:
            foreach (KeyValuePair<string, ContainingBagsNr> bagRule in bagRules)
            {
                Console.WriteLine("{0}: {1}, {2}, {3}, {4}, {5}, {6}", bagRule.Key.ToString(), bagRule.Value.bag1, bagRule.Value.bag2.ToString(), bagRule.Value.bag3.ToString(), bagRule.Value.bag4.ToString(), bagRule.Value.bag5.ToString(), bagRule.Value.bag6.ToString());
                //Console.WriteLine("{0}: {1}, {2}, {3}, {4}, {5}, {6}", bagRule.Key.ToString(), bagRule.Value.bagNr1, bagRule.Value.bagNr2.ToString(), bagRule.Value.bagNr3.ToString(), bagRule.Value.bagNr4.ToString(), bagRule.Value.bagNr5.ToString(), bagRule.Value.bagNr6.ToString());

            }


            //************* Start searching and counting ***********//



            //goodBags.Add("shiny gold");
            string checkBag = "shiny gold";

            int nrOfBags = CountNrBagsInside(checkBag, bagRules);



            return nrOfBags.ToString();
        }

        public int CountNrBagsInside(string checkBag, Dictionary<string, ContainingBagsNr> bagRules)
        {
            int count = 0;



            if (checkBag.Equals("other") || checkBag.Length < 1)
            {
                //Console.WriteLine("no check");
            }
            else
            {


                foreach (KeyValuePair<string, ContainingBagsNr> bagRule in bagRules)
                {
                    if (bagRule.Key.Contains(checkBag))
                    {
                        //count += bagRule.Value.bagNr1;
                        string checkNext = bagRule.Value.bag1.ToString();
                        count += bagRule.Value.bagNr1 * CountNrBagsInside(checkNext, bagRules);
                        count += bagRule.Value.bagNr1;

                        //count += bagRule.Value.bagNr2;
                        checkNext = bagRule.Value.bag2.ToString();
                        count += bagRule.Value.bagNr2 * CountNrBagsInside(checkNext, bagRules);
                        count += bagRule.Value.bagNr2;

                        //count += bagRule.Value.bagNr3;
                        checkNext = bagRule.Value.bag3.ToString();
                        count += bagRule.Value.bagNr3 * CountNrBagsInside(checkNext, bagRules);
                        count += bagRule.Value.bagNr3;

                        //count += bagRule.Value.bagNr2;
                        checkNext = bagRule.Value.bag4.ToString();
                        count += bagRule.Value.bagNr4 * CountNrBagsInside(checkNext, bagRules);
                        count += bagRule.Value.bagNr4;

                        //count += bagRule.Value.bagNr2;
                        checkNext = bagRule.Value.bag5.ToString();
                        count += bagRule.Value.bagNr5 * CountNrBagsInside(checkNext, bagRules);
                        count += bagRule.Value.bagNr5;

                        //count += bagRule.Value.bagNr2;
                        checkNext = bagRule.Value.bag6.ToString();
                        count += bagRule.Value.bagNr6 * CountNrBagsInside(checkNext, bagRules);
                        count += bagRule.Value.bagNr6;

                    }

                }
                Console.WriteLine("Searching for: {0}, count: {1}", checkBag, count);
            }

            if (count == 0)
            {
                //count = 1;
            }

            return count;

        }

        public string GetInput(bool testInput)
        {

            var myInput = new Inputs.Day07();
            return (testInput) ? myInput.testInput : myInput.input;
        }

    }



}
