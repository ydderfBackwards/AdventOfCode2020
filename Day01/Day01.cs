using System;
using System.IO;
using System.Linq;


namespace AdventOfCode2020
{
    public class Day01
    {
        public string SolvePart1(string input)
        {
            int result;
            int target = 2020;
            //Convert input to array of integers.
            //OLD: string[] strNumbers = input.Split(Environment.NewLine);
            //OLD: int[] numbers = Array.ConvertAll(strNumbers, int.Parse);

            int[] numbers = Array.ConvertAll(input.Split(Environment.NewLine), int.Parse);

            //Sort the array
            Array.Sort(numbers);

            //Get array length
            int arrayLength = numbers.Length;

            //Loop through array
            for (var i = 0; i < arrayLength; i++)
            {
                for (var j = 0; j < arrayLength; j++)
                {
                    //Check sum
                    if ((numbers[i] + numbers[j]) == target)
                    {
                        result = numbers[i] * numbers[j];
                        return result.ToString();
                    }

                    //Check if result is more than 2020. If so, stop with this loop (because numbers are sorted, the next result will be bigger)
                    if ((numbers[i] + numbers[j]) > target)
                    {
                        break;
                    }

                }
            }



            return "ERROR: No result found";
        }



        public string SolvePart2(string input)
        {
            int result;
            int target = 2020;
            //Convert input to array of integers.
            //OLD: string[] strNumbers = input.Split(Environment.NewLine);
            //OLD: int[] numbers = Array.ConvertAll(strNumbers, int.Parse);

            int[] numbers = Array.ConvertAll(input.Split(Environment.NewLine), int.Parse);

            //Sort the array
            Array.Sort(numbers);

            //Get array length
            int arrayLength = numbers.Length;

            //Get first (smallest) value.
            int smallestValue = numbers[0];

            //Loop through array
            for (var i = 0; i < arrayLength; i++)
            {
                for (var j = 0; j < arrayLength; j++)
                {
                    //If sum + smalles value is already to high, don't check this combination.
                    if ((numbers[i] + numbers[j] + smallestValue) <= target)
                    {


                        for (var k = 0; k < arrayLength; k++)
                        {
                            //Check sum
                            if ((numbers[i] + numbers[j] + numbers[k]) == target)
                            {
                                result = numbers[i] * numbers[j] * numbers[k];
                                return result.ToString();
                            }

                            //Check if result is more than 2020. If so, stop with this loop (because numbers are sorted, the next result will be bigger)
                            if ((numbers[i] + numbers[j] + numbers[k]) > target)
                            {
                                break;
                            }
                        }
                    }
                }
            }



            return "ERROR: No result found";
        }


        public string GetInput(bool testInput)
        {

            var myInput = new Inputs.Day01();
            return (testInput) ? myInput.testInput : myInput.input;
        }

    }



}
