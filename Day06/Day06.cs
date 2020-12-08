using System;
using System.IO;
using System.Linq;
using System.Collections.Generic; //For list
using System.Text.RegularExpressions; // For regex

namespace AdventOfCode2020
{
    public class Day06
    {
        public string SolvePart1(string input)
        {

            //Convert input to array of integers.
            string[] lines = input.Split(Environment.NewLine);

            var answers = new List<char>();
            int total = 0;

            foreach (string line in lines)
            {
                if (line.Length == 0)
                {
                    //Next is new group --> count number of destinct anwsers in this group
                    total += answers.Distinct().Count();
                    //Clear answerlist for next groep.
                    answers.Clear();
                }
                else
                {
                    foreach (char answer in line)
                    {
                        //Add all answers to list
                        answers.Add(answer);
                    }
                }
            }

            //count number of destinct anwsers in the last group
            total += answers.Distinct().Count();
   
            return total.ToString();
        }



        public string SolvePart2(string input)
        {
            //Convert input to array of integers.
            string[] lines = input.Split(Environment.NewLine);
            string validAnswers = "";
            string stillValidAnswers = "";

            int total = 0;
            bool newGroup = true;

            foreach (string line in lines)
            {

                if (line.Length == 0)
                {
                    //Next is new group --> count number of destinct anwsers in this group
                    total += validAnswers.Length;
                    //Clear answerlist for next groep.
                    newGroup = true;

                }
                else
                {
                    //Check if this is a new group
                    if (newGroup)
                    {
                        //New group --> awnser is base set to start with
                        validAnswers = line;
                        newGroup = false;
                    }
                    else
                    {
                        foreach (char answer in line)
                        {
                            //Check if this answer is given by all previous person in this group.
                            if (validAnswers.Contains(answer))
                            {
                                //Save all valid anwsers for this person
                                stillValidAnswers += answer.ToString();
                            }
                        }

                        //Save valid for this group 
                        validAnswers = stillValidAnswers;
                        //Reset valid answers for next person
                        stillValidAnswers = "";

                    }

                }
            }

            //count number of destinct anwsers in the last group
            total += validAnswers.Length;

            return total.ToString();
        }



        public string GetInput(bool testInput)
        {

            var myInput = new Inputs.Day06();
            return (testInput) ? myInput.testInput : myInput.input;
        }

    }



}
