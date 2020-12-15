using System;
using System.IO;
using System.Linq;
using System.Collections.Generic; //For list
using System.Text.RegularExpressions; // For regex

namespace AdventOfCode2020
{
    public class Day15
    {
        public string SolvePart1(string input)
        {
            int setpoint = 2020;
            var number = Solve(setpoint, input);

            return number.ToString();
        }


        public string SolvePart2(string input)
        {
            int setpoint = 30000000;
            var number = Solve(setpoint, input);

            return number.ToString();
        }


        public int Solve(int setpoint, string input)
        {
            var (numbers, number, turn) = ReadInput(input);

            while (turn <= setpoint)
            {
                number = DetermineNumber(numbers, number, turn);
                numbers = AddNumber(numbers, number, turn);
                turn++;
            }

            return number;
        }


        public int DetermineNumber(Dictionary<int, PreviousTurn> numbers, int lastSpokenNumber, int thisTurn)
        {
            int newNumber = 0;

            //If number exist already
            if (numbers.ContainsKey(lastSpokenNumber))
            {
                //Read current data
                PreviousTurn value = numbers[lastSpokenNumber];

                if (value.previousTurnSecondLast == 0)
                {
                    //Number has been spoken ones --> player says zero
                    newNumber = 0;
                }
                else
                {
                    //Number has been spoken twice (ore more) --> Player says difference between last and second last
                    newNumber = value.previousTurnLast - value.previousTurnSecondLast;
                }
            }
            else
            {
                Console.WriteLine("Error Key not found!");
            }

            return newNumber;
        }

        public Dictionary<int, PreviousTurn> AddNumber(Dictionary<int, PreviousTurn> numbers, int number, int thisTurn)
        {
            var turn = new PreviousTurn();

            //If number exist already
            if (numbers.ContainsKey(number))
            {
                //Read current data
                PreviousTurn value = numbers[number];
                //Shift turn info
                turn.previousTurnSecondLast = value.previousTurnLast;
                turn.previousTurnLast = thisTurn;

                //Update info
                numbers[number] = turn;
            }
            else
            {
                //Define turn info
                turn.previousTurnSecondLast = 0;
                turn.previousTurnLast = thisTurn;

                //Add info
                numbers.Add(number, turn);
            }

            return numbers;

        }

        public (Dictionary<int, PreviousTurn>, int, int) ReadInput(string input)
        {
            //Function: Add input to dictionary
            //Return: Dictionary with info, last read number, turn number


            var numbers = new Dictionary<int, PreviousTurn>();
         
            //Convert input to array of integers.
            string[] inputNumbers = input.Split(',');

            int turnNr = 1; //start at turn one
            foreach (string inputNumber in inputNumbers)
            {
                numbers = AddNumber(numbers, int.Parse(inputNumber), turnNr);
                turnNr++; //Next turn
            }

            return (numbers, int.Parse(inputNumbers[inputNumbers.Length - 1]), turnNr);
        }


        public struct PreviousTurn
        {
            public int previousTurnLast;
            public int previousTurnSecondLast;

        }

        public string GetInput(bool testInput)
        {

            var myInput = new Inputs.Day15();
            return (testInput) ? myInput.testInput : myInput.input;
        }

    }



}
