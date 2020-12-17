using System;
using System.IO;
using System.Linq;
using System.Collections.Generic; //For list
using System.Text.RegularExpressions; // For regex

namespace AdventOfCode2020
{
    public class Day16
    {
        public string SolvePart1(string input)
        {
            var (rules, myTicket, otherTickets) = ReadInput(input);
            var errorRate = ScanTickets(rules, otherTickets);
            return errorRate.ToString();
        }



        public string SolvePart2(string input)
        {
            var (rules, myTicket, otherTickets) = ReadInput_Part2(input);
            var validOtherTickets = RemoveInvalidTickets(rules, otherTickets);
            var awnser = FindPositions(rules, myTicket, validOtherTickets);

            return awnser.ToString();

        }

        public long FindPositions(Dictionary<string, Rule> rules, int[] myTicket, List<int[]> otherTickets)
        {
            int[] ruleFieldNr = new int[rules.Count()]; //If a field is found, we save the fieldnumber in this array. The array index is the rule number

            //Whe have to search a value for each rule. So worst case make a check for each rule (sometimes we found more than one match in one loop)
            for (int i = 0; i < rules.Count(); i++)
            {
                if (!ruleFieldNr.Contains(0))
                {
                    //All rules and fields are matched, so no need to check again.
                    break;
                }

                //For each field
                for (int field = 0; field < rules.Count(); field++)
                {
                    int nrOkRules = 0;
                    int ruleNr = 0;
                    int ruleNrOK = 0;
                    foreach (var rule in rules)
                    {
                        if (ruleFieldNr[ruleNr] == 0) //Only check if this rule has no matching field yet....
                        {
                            bool allOK = true;
                            foreach (var ticket in otherTickets)
                            {
                                int checkValue = ticket[field];
                                if (!(checkValue >= rule.Value.min1 && checkValue <= rule.Value.max1) && !(checkValue >= rule.Value.min2 && checkValue <= rule.Value.max2))
                                {   //Value is outside limits
                                    allOK = false;
                                }
                            }

                            if (allOK)
                            {
                                nrOkRules++;
                                ruleNrOK = ruleNr; //Save rule number when this field is OK. (we only match a field with a rule when there is only one match)
                            }
                        }
                        ruleNr++;
                    }

                    if (nrOkRules == 1) //If this field only matches with one rule --> its a match!
                    {
                        ruleFieldNr[ruleNrOK] = field + 1;
                        //Console.WriteLine("Found field {0} for rule {1}", field, ruleNrOK);
                    }
                    // Console.WriteLine("Field {0} has {1} possibilities for rule: {2}", field, nrOkRules, ruleNr);
                }
                // Console.WriteLine("Next check");
            }

            //*********** Multiply all values of your ticket for the "departure" rules
            long result = 1;
            int ruleNumber = 0;
            foreach (var rule in rules)
            {
                if (rule.Key.Contains("departure"))
                {
                    long value = (long)myTicket[ruleFieldNr[ruleNumber] - 1];
                    result = result * value;
                }
                ruleNumber++;
            }

            return result;
        }


        public List<int[]> RemoveInvalidTickets(Dictionary<string, Rule> rules, List<int[]> otherTickets)
        {
            List<int[]> validTickets = new List<int[]>(otherTickets); //Create a copy where we can remove the invalid tickets


            foreach (var ticket in otherTickets)
            {
                //For all values on a ticket
                for (int j = 0; j < ticket.Length; j++)
                {
                    bool fieldOK = false;
                    int checkValue = ticket[j];
                    foreach (var rule in rules)
                    {
                        if ((checkValue >= rule.Value.min1 && checkValue <= rule.Value.max1) || (checkValue >= rule.Value.min2 && checkValue <= rule.Value.max2))
                        {   //Value is within limits 
                            fieldOK = true;
                        }
                    }

                    if (!fieldOK) //One or more fields are not valid --> remove ticket
                    {
                        validTickets.Remove(ticket);
                    }
                }
            }

            return validTickets;
        }

        public int ScanTickets(Dictionary<string, Rule> rules, int[,] otherTickets) //Scan all other tickets and determine error rate
        {
            int errorRate = 0;

            //Check for each rule --> if a value on the ticket is oke, set it to zero so only the invalid number remain.
            foreach (var rule in rules)
            {
                //For all tickets
                for (int i = 0; i < otherTickets.GetLength(0); i++)
                {
                    //For all values on a ticket
                    for (int j = 0; j < otherTickets.GetLength(1); j++)
                    {
                        int checkValue = otherTickets[i, j];
                        if ((checkValue >= rule.Value.min1 && checkValue <= rule.Value.max1) || (checkValue >= rule.Value.min2 && checkValue <= rule.Value.max2))
                        {   //Value is within a limit --> write value to zero.
                            otherTickets[i, j] = 0;
                        }
                    }
                }
            }


            //For all tickets
            for (int i = 0; i < otherTickets.GetLength(0); i++)
            {
                //For all values
                for (int j = 0; j < otherTickets.GetLength(1); j++)
                {   //Count the sum of all values (only the wrong ones are left, the good ones have value zero)
                    errorRate += otherTickets[i, j];
                }
            }


            return errorRate;
        }



        public (Dictionary<string, Rule>, int[], int[,]) ReadInput(string input)
        {
            //Convert input to array of integers.
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            Dictionary<string, Rule> rules = new Dictionary<string, Rule>();
            Rule rule = new Rule();
            int[] myTicket = new int[1];
            int[,] otherTickets = new int[1, 1];
            int i = 0;

            string[] ruleSeperators = { ":", "-", " or " };
            int mode = 0; //0 = rules for ticket, 1 = your ticket, 2 = nearby tickets

            foreach (string line in lines)
            {
                switch (mode)
                {
                    case 0: //Rules
                        if (line.StartsWith("your ticket"))
                        {
                            mode = 1;
                            myTicket = new int[rules.Count()];
                        }
                        else
                        {
                            string[] data = line.Split(ruleSeperators, System.StringSplitOptions.RemoveEmptyEntries);

                            rule.min1 = int.Parse(data[1].Trim());
                            rule.max1 = int.Parse(data[2].Trim());
                            rule.min2 = int.Parse(data[3].Trim());
                            rule.max2 = int.Parse(data[4].Trim());

                            rules.Add(data[0], rule);
                        }

                        break;

                    case 1: //my Ticket
                        if (line.StartsWith("nearby tickets"))
                        {
                            mode = 2;
                            //Redefine int[,] with found data size
                            otherTickets = new int[lines.Length - Array.FindIndex(lines, s => s.StartsWith("nearby tickets")) - 1, myTicket.Count()];
                            i = 0;
                        }
                        else
                        {
                            myTicket = Array.ConvertAll<string, int>(line.Split(','), Convert.ToInt32);
                        }

                        break;

                    case 2: //Nearby tickets
                        string[] dataNearby = line.Split(',', System.StringSplitOptions.RemoveEmptyEntries);
                        for (int j = 0; j < otherTickets.GetLength(1); j++)
                        {
                            otherTickets[i, j] = int.Parse(dataNearby[j].Trim());
                        }
                        i++;

                        break;
                }
            }

            return (rules, myTicket, otherTickets);

        }


        public (Dictionary<string, Rule>, int[], List<int[]>) ReadInput_Part2(string input)
        {
            //Convert input to array of integers.
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            Dictionary<string, Rule> rules = new Dictionary<string, Rule>();
            Rule rule = new Rule();
            int[] myTicket = new int[1];
            //  int[,] otherTickets = new int[1, 1];
            List<int[]> otherTickets = new List<int[]>();

            string[] ruleSeperators = { ":", "-", " or " };
            int mode = 0; //0 = rules for ticket, 1 = your ticket, 2 = nearby tickets

            foreach (string line in lines)
            {
                switch (mode)
                {
                    case 0: //Rules
                        if (line.StartsWith("your ticket"))
                        {
                            mode = 1;
                            myTicket = new int[rules.Count()];
                        }
                        else
                        {
                            string[] data = line.Split(ruleSeperators, System.StringSplitOptions.RemoveEmptyEntries);

                            rule.min1 = int.Parse(data[1].Trim());
                            rule.max1 = int.Parse(data[2].Trim());
                            rule.min2 = int.Parse(data[3].Trim());
                            rule.max2 = int.Parse(data[4].Trim());
                            
                            rules.Add(data[0], rule);
                        }

                        break;

                    case 1: //my Ticket
                        if (line.StartsWith("nearby tickets"))
                        {
                            mode = 2;
                        }
                        else
                        {
                            myTicket = Array.ConvertAll<string, int>(line.Split(','), Convert.ToInt32);
                        }

                        break;

                    case 2: //Nearby tickets
                        var tmpTicket = Array.ConvertAll<string, int>(line.Split(','), Convert.ToInt32);
                        otherTickets.Add(tmpTicket);
                        break;
                }
            }

            return (rules, myTicket, otherTickets);

        }

        public struct Rule
        {
            public int min1;
            public int max1;

            public int min2;
            public int max2;
        }

        public string GetInput(bool testInput)
        {
            var myInput = new Inputs.Day16();
            return (testInput) ? myInput.testInput : myInput.input;
        }
    }
}
