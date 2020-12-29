using System;
using System.IO;
using System.Linq;
using System.Collections.Generic; //For list
using System.Text.RegularExpressions; // For regex

namespace AdventOfCode2020
{
    public class Day19
    {
        public string SolvePart1(string input)
        {
            var (checkLines, rules, ruleZero, charA, charB) = ReadInput(input);

            //Old slower solution
            // var rulesTotal = CreateRuleZeroList(rules, ruleZero, charA, charB);
            // var count = CountValidMessages(rulesTotal, checkLines);

            //New faster solution, but not completely fair because we have hard coded rule 42 en 31 for rule zero 
            var rules42 = FindRule(rules, 42, charA, charB);
            var rules31 = FindRule(rules, 31, charA, charB);
            var count = CountValidMessages_Part1(rules42, rules31, checkLines);
            return count.ToString();

        }

        public string SolvePart2(string input)
        {
            var (checkLines, rules, ruleZero, charA, charB) = ReadInput(input);

            var rules42 = FindRule(rules, 42, charA, charB);
            var rules31 = FindRule(rules, 31, charA, charB);

            var count = CountValidMessages_Part2(rules42, rules31, checkLines);

            return count.ToString();
        }


        public int CountValidMessages_Part1(List<string> rules42, List<string> rules31, string[] checkLines)
        {
            int count = 0;

            int line42Length = rules42[0].Length; //Made the assumption that all rules have the same length
            int line31Length = rules31[0].Length;

            foreach (string line in checkLines)
            {
                int count42 = 0;
                int count31 = 0;
                string checkLine = line;
                while (checkLine.Length > 0)
                {
                    string partOfLine = checkLine[0..line42Length];
                    if (rules42.Any(x => x.Equals(partOfLine)))
                    {
                        checkLine = checkLine[line42Length..checkLine.Length];
                        count42++;
                    }
                    else
                    {
                        goto Check31;
                    }
                }

            Check31:

                while (checkLine.Length > 0)
                {
                    string partOfLine = checkLine[0..line31Length];
                    if (rules31.Any(x => x.Equals(partOfLine)))
                    {
                        checkLine = checkLine[line31Length..checkLine.Length];
                        count31++;
                    }
                    else
                    {
                        goto Done;
                    }
                }

                //Rule 0 is: 8 11 ==> this is: 42 42 31 (because 8 = 42 and 11 = 42 31)
                if (count42 == 2 && count31 == 1)
                {
                    count++;
                }
            Done:;

            }

            return count;
        }


        public int CountValidMessages_Part2(List<string> rules42, List<string> rules31, string[] checkLines)
        {
            int count = 0;
            int line42Length = rules42[0].Length; //Made the assumption that all rules have the same length
            int line31Length = rules31[0].Length;

            foreach (string line in checkLines)
            {
                int count42 = 0;
                int count31 = 0;
                string checkLine = line;
                while (checkLine.Length > 0)
                {
                    string partOfLine = checkLine[0..line42Length];
                    if (rules42.Any(x => x.Equals(partOfLine)))
                    {
                        checkLine = checkLine[line42Length..checkLine.Length];
                        count42++;
                    }
                    else
                    {
                        goto Check31;
                    }
                }

            Check31:

                while (checkLine.Length > 0)
                {
                    string partOfLine = checkLine[0..line31Length];
                    if (rules31.Any(x => x.Equals(partOfLine)))
                    {
                        checkLine = checkLine[line31Length..checkLine.Length];
                        count31++;
                    }
                    else
                    {
                        goto Done;
                    }
                }

               //Rule 0 is: 8 11 ==> this is: 42 42 31 (because 8 = 42 and 11 = 42 31) or a repetition of this eq. 42 42 42 31 31
                //Both rules should be used at leased ones, and rule 42 more than rule 31
                if (count42 > 0 && count31 > 0 && count42 > count31)
                {
                    count++;
                }

            Done:;

            }

            return count;
        }


        public int CountValidMessages(List<string> rules, string[] checkLines)
        {
            int count = 0;
            foreach (string line in checkLines)
            {
                if (rules.Any(x => x.Equals(line)))
                {
                    count++;
                }
            }
            return count;
        }

        public List<string> CreateRuleZeroList(int[,,] rules, string ruleZero, int charA, int charB) //Old slow solution
        {
            List<string>[] ruleZeroList = new List<string>[3];
            List<string> rulesTotal = new List<string>();

            string[] data = ruleZero.Trim().Split(" ");

            for (int i = 0; i < data.Length; i++)
            {
                int number = int.Parse(data[i]);
                ruleZeroList[i] = FindRule(rules, number, charA, charB);
            }

            foreach (string rule0 in ruleZeroList[0])
            {
                foreach (string rule1 in ruleZeroList[1])
                {
                    string rule = rule0 + rule1;// + rule2;
                    rulesTotal.Add(rule);
                }
            }
            List<string> distinct = rulesTotal.Distinct().ToList();

            return distinct;
        }

        public List<string> FindRule(int[,,] rules, int ruleNr, int charA, int charB)
        {
            List<string> ruleList = new List<string>();
            List<string> tmpList00 = new List<string>();
            List<string> tmpList01 = new List<string>();
            List<string> tmpList10 = new List<string>();
            List<string> tmpList11 = new List<string>();

            if (ruleNr == charA)
            {
                ruleList.Add("a");
            }
            else if (ruleNr == charB)
            {
                ruleList.Add("b");
            }
            else
            {
                if (rules[ruleNr, 0, 0] != 0)
                {
                    tmpList00 = FindRule(rules, rules[ruleNr, 0, 0], charA, charB); 
                }
                if (rules[ruleNr, 0, 1] != 0)
                {
                    tmpList01 = FindRule(rules, rules[ruleNr, 0, 1], charA, charB); 
                }
                if (rules[ruleNr, 1, 0] != 0)
                {
                    tmpList10 = FindRule(rules, rules[ruleNr, 1, 0], charA, charB); 
                }
                if (rules[ruleNr, 1, 1] != 0)
                {
                    tmpList11 = FindRule(rules, rules[ruleNr, 1, 1], charA, charB); 
                }

                foreach (string tmp0 in tmpList00)
                {
                    if (tmpList01.Count > 0)
                    {
                        foreach (string tmp1 in tmpList01)
                        {
                            string rule = tmp0 + tmp1;
                            ruleList.Add(rule);
                        }
                    }
                    else
                    {
                        ruleList.Add(tmp0);
                    }

                }


                foreach (string tmp0 in tmpList10)
                {
                    if (tmpList11.Count > 0)
                    {
                        foreach (string tmp1 in tmpList11)
                        {
                            string rule = tmp0 + tmp1;
                            ruleList.Add(rule);
                        }
                    }
                    else
                    {
                        ruleList.Add(tmp0);
                    }

                }
            }

            return ruleList;
        }


        public (string[], int[,,], string, int, int) ReadInput(string input)
        {
            string ruleZero = "";
            //Convert input to array of integers.
            string[] blocks = input.Split(Environment.NewLine + Environment.NewLine);

            //Split in two main blocks
            string[] ruleLines = blocks[0].Split(Environment.NewLine);
            string[] checkLines = blocks[1].Split(Environment.NewLine);

            int[,,] rules = new int[ruleLines.Length, 2, 2]; //rule[a.b.c] -> a = rule number, b = option (left or right part of | ), c = value
            int charA = 0;
            int charB = 0;

            foreach (string ruleLine in ruleLines)
            {
                string[] data = ruleLine.Split(':');

                if (data[0].Equals("0"))
                {
                    ruleZero = data[1].Trim();
                }
                else
                {
                    if (data[1].Contains("a"))
                    {
                        charA = int.Parse(data[0]);
                    }
                    else if (data[1].Contains("b"))
                    {
                        charB = int.Parse(data[0]);
                    }
                    else
                    {
                        int ruleNr = int.Parse(data[0]);
                        string[] rule = data[1].Split('|');

                        for (int i = 0; i < rule.Length; i++)
                        {
                            rule[i] = rule[i].Trim();
                            string[] ruleValues = rule[i].Split(" ");
                            for (int j = 0; j < ruleValues.Length; j++)
                            {
                                rules[ruleNr, i, j] = int.Parse(ruleValues[j]);
                            }
                        }
                    }
                }
            }

            return (checkLines, rules, ruleZero, charA, charB);
        }

        public class Conditions
        {
            public string[] option1;
            public string[] option2;
        }



        public string GetInput(bool testInput)
        {
            var myInput = new Inputs.Day19();
            return (testInput) ? myInput.testInput : myInput.input;
        }

    }



}
