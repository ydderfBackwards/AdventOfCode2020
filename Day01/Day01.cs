using System;
using System.IO;
using System.Linq;


namespace AdventOfCode2020
{
    public class Day01
    {
        public string SolvePart1(string input)
        {




            return "Test result 1";
        }



        public string SolvePart2(string input)
        {
            return input;//"Test result 2";
        }


        public string GetInput(bool testInput)
        {
            
            var myInput = new Inputs.Day01();
            return (testInput) ? myInput.testInput : myInput.input;
        }

    }



}
