using System;
using System.IO;
using System.Linq;


namespace AdventOfCode2020
{
    public class Day02
    {
        public string SolvePart1(string input)
        {
            int numberOK = 0;
            //Convert input to array of integers.
            string[] lines = input.Split(Environment.NewLine);

            foreach (string line in lines)
            {
                char[] separatingStrings = { ':', '-', ' ' }; //Char used for splitting
                string[] data = line.Split(separatingStrings); //Split line to string array
                int min = int.Parse(data[0]);   
                int max = int.Parse(data[1]);
                char letter = data[2][0];   //convert string to char
                string password = data[4];

                int count = 0;

                //Count letter in password
                foreach(char pwLetter in password)
                {
                    if (pwLetter == letter)
                    {
                        count ++;
                    }
                }

                //Check if count is between limits
                if( (min <= count) && (max >= count))
                {
                    numberOK++;
                }

            }

            return numberOK.ToString();
        }



        public string SolvePart2(string input)
        {
            int numberOK = 0;
            //Convert input to array of integers.
            string[] lines = input.Split(Environment.NewLine);

            foreach (string line in lines)
            {
                char[] separatingStrings = { ':', '-', ' ' }; //Char used for splitting
                string[] data = line.Split(separatingStrings); //Split line to string array
                int min = int.Parse(data[0]);   
                int max = int.Parse(data[1]);
                char letter = data[2][0];   //convert string to char
                string password = data[4];

                //Convert to zero based
                min--;
                max--;

                //Check with XOR
                if( (data[4][min] == letter) ^ (data[4][max] == letter))
                {
                    numberOK++;

                }
   

            }

            return numberOK.ToString();
        }


        public string GetInput(bool testInput)
        {

            var myInput = new Inputs.Day02();
            return (testInput) ? myInput.testInput : myInput.input;
        }

    }



}
