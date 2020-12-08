using System;
using System.IO;
using System.Linq;
using System.Collections.Generic; //For list
using System.Text.RegularExpressions; // For regex

namespace AdventOfCode2020
{
    public class Day08
    {
        public string SolvePart1(string input)
        {

            //Convert input to array of integers.
            string[] lines = input.Split(Environment.NewLine);

            Instruction[] commandList = new Instruction[lines.Length];

            int i = 0;
            //Store all instructions in commandlist
            foreach (string line in lines)
            {
                string[] data = line.Split(' ');
                commandList[i].command = data[0];
                commandList[i].value = int.Parse(data[1]);
                i++;
            }

            int nrOffCommands = i;

            //Run complete commandlist
            var (acc, infloop) = RunBootCode(commandList, nrOffCommands);

            return acc.ToString();

        }




        public string SolvePart2(string input)
        {
            //Convert input to array of integers.
            string[] lines = input.Split(Environment.NewLine);

            Instruction[] commandList = new Instruction[lines.Length];

            int i = 0;
            //Store all instructions in commandlist
            foreach (string line in lines)
            {
                string[] data = line.Split(' ');
                commandList[i].command = data[0];
                commandList[i].value = int.Parse(data[1]);
                i++;
            }

            int nrOffCommands = i;
            Instruction[] commandListCheck = new Instruction[lines.Length];
            long result=0;
            

            //Loop to all commands and change one by one
            for( i=0; i < nrOffCommands; i++)
            {
                //Copy base data
                commandListCheck = commandList.ToArray();

                //Replace one command
                if(commandListCheck[i].command == "nop" ) {commandListCheck[i].command = "jmp";}
                if(commandListCheck[i].command == "jmp" ) {commandListCheck[i].command = "nop";}

                //Check result
                 var (acc, infloop) = RunBootCode(commandListCheck, nrOffCommands);

                //If not a infinitive loop --> good program found --> done. 
                if(!infloop)
                {
                    result = acc;
                    break;
                }

            }

            return result.ToString();

        }

        public (long acc, bool infLoop) RunBootCode(Instruction[] commandList, int nrOffCommands)
        {
            bool doubleCommand = false;
            bool lastCommand = false;
            int i = 0;
            long acc = 0;
           

            //Loop until we gat a 
            while (!doubleCommand && !lastCommand)
            {
                //Check if we are not outside the array
                if(i >= nrOffCommands)
                {
                    doubleCommand = true;
                    break;
                }

                //Read command before modifying
                string command = commandList[i].command;

                //Modify the command to "err" to detect a double command
                commandList[i].command = "err";

                //Check if we read the last command
                if(i == nrOffCommands-1)
                {
                    lastCommand = true;
                }

                //Handle the command
                switch (command)
                {
                    case "nop":
                        i++;
                        break;

                    case "acc":
                        acc += commandList[i].value;
                        i++;
                        break;

                    case "jmp":
                        i += commandList[i].value;
                        break;

                    case "err":
                        doubleCommand = true;
                        break;
                }
            }

            return (acc, doubleCommand);

        }
        public struct Instruction
        {
            public string command;
            public int value;
        }

        public string GetInput(bool testInput)
        {

            var myInput = new Inputs.Day08();
            return (testInput) ? myInput.testInput : myInput.input;
        }

    }



}
