using System;
using System.IO;
using System.Linq;
using System.Collections.Generic; //For list
using System.Text.RegularExpressions; // For regex

namespace AdventOfCode2020
{
    public class Day18
    {
        public string SolvePart1(string input)
        {
            //Convert input to array of integers.
            string[] lines = input.Split(Environment.NewLine);

            long total = 0;
            foreach (string line in lines)
            {
                string expressions = line.Replace(" ", String.Empty);
                var (result, index) = Calculate(expressions, 0);
                total += result;
            }

            return total.ToString();
        }



        public string SolvePart2(string input)
        {
            //Convert input to array of integers.
            string[] lines = input.Split(Environment.NewLine);

            long total = 0;
            foreach (string line in lines)
            {
                string line2 = line.Replace(" ", String.Empty);
                string[] expressions = new string[line2.Length];

                for (int i = 0; i < line2.Length; i++)
                {
                    expressions[i] = line2[i].ToString(); //Convert to string array, with one char for each string, so we can later save value > 9 (with more digits)
                }

                var result = Calculate_V2(expressions);
                total += Int64.Parse(result);
            }
            return total.ToString();
        }

        public string Calculate_V2(string[] expressions)
        {
        GOTO_RECHECK:

            //First check if there is a expression between parentheses
            if (expressions.Any(x => x.Contains("(")))
            {
                int posParOpen = -1;
                int posParClose = -1;
                for (int i = 0; i < expressions.Length; i++)
                {
                    if (expressions[i].Contains("("))
                    {
                        posParOpen = i;
                    }
                    else if (expressions[i].Contains(")"))
                    {
                        posParClose = i;
                        break; //Found closing parenthese ==> handle first the part between parentheses
                    }
                }

                if (posParClose > 0 && posParOpen >= 0)
                {
                    //Create a new array with the part between parentheses.
                    string[] newExpressions = new string[posParClose - posParOpen - 1];
                    Array.Copy(expressions, posParOpen + 1, newExpressions, 0, posParClose - posParOpen - 1);

                    string result = Calculate_V2(newExpressions);//Get result for part between parentheses
                    expressions = ConcatExpressions(expressions, result, posParOpen, posParClose); //Replace the part between parentheses with new result
                    goto GOTO_RECHECK;
                }
            }


        GOTO_RECHECK_ADD:
            //Check if there is add instruction
            if (expressions.Any(x => x.Contains("+")))
            {
                for (int i = 0; i < expressions.Length; i++)
                {
                    if (expressions[i].Contains("+"))
                    {
                        long valueA = Int64.Parse(expressions[i - 1]);
                        long valueB = Int64.Parse(expressions[i + 1]);

                        long result = DoMath(valueA, valueB, '+');

                        var tmpArray = ConcatExpressions(expressions, result.ToString(), i - 1, i + 1);
                        expressions = new string[tmpArray.Length];
                        Array.Copy(tmpArray, expressions, tmpArray.Length);

                        goto GOTO_RECHECK_ADD;
                    }
                }
            }

        GOTO_RECHECK_MULL:
            for (int i = 0; i < expressions.Length; i++)
            {
                if (expressions[i].Contains("*"))
                {
                    long valueA = Int64.Parse(expressions[i - 1]);
                    long valueB = Int64.Parse(expressions[i + 1]);

                    long result = DoMath(valueA, valueB, '*');

                    var tmpArray = ConcatExpressions(expressions, result.ToString(), i - 1, i + 1);
                    expressions = new string[tmpArray.Length];
                    Array.Copy(tmpArray, expressions, tmpArray.Length);
                    goto GOTO_RECHECK_MULL;
                }
            }

            return expressions[0]; //There should be only one value left
        }



        public string[] ConcatExpressions(string[] expressions, string result, int copyStartTo, int copyFromToEnd)
        {
            //Concatenate a array with a string result. 
            //new string = expressions[0..CopyStartTo] + result + expressions[copyFromToEnd+1..end]

            int length = expressions.Length - copyFromToEnd + copyStartTo;
            string[] tmpArray = new string[length];

            //  Console.WriteLine("Expr.Length: {0}, StartTo: {1}, FromEnd: {2}, Length: {3} ", expressions.Length, copyStartTo, copyFromToEnd, length);
            int i = 0, j;
            if (copyStartTo > 0)
            {
                for (i = 0; i < copyStartTo; i++)
                {
                    tmpArray[i] = expressions[i];
                }

            }
            tmpArray[i] = result;
            i++;

            for (j = copyFromToEnd + 1; j < expressions.Length; j++)
            {
                tmpArray[i] = expressions[j];
                i++;
            }

            return tmpArray;
        }



        public void PrintStringArray(string[] arrayToPrint)
        { //Function just for debugging....
            for (int i = 0; i < arrayToPrint.Length; i++)
            {
                Console.Write(arrayToPrint[i]);
            }
            Console.WriteLine();
        }

        public (long, int) Calculate(string expressions, int index) //For Part 1
        {
            long result = 0;
            char instruction = '+'; //Start with add, so the first number is added with the default result (0)
            long number;

            while (index < expressions.Length)
            {
                if (expressions[index].CompareTo('(') == 0)
                {
                    index++;
                    (number, index) = Calculate(expressions, index);         //Get the value inside the () --> go one level down
                    result = DoMath(result, number, instruction);
                }
                else if (expressions[index].CompareTo(')') == 0)
                {
                    break; //This calculation is done --> go one level up
                }
                else if (expressions[index].CompareTo('+') == 0 || expressions[index].CompareTo('*') == 0)
                {
                    instruction = expressions[index]; //store instruction for next calculation
                }
                else
                {
                    if (Char.IsNumber(expressions[index])) //check if char is number (it should be)
                    {
                        number = (long)Char.GetNumericValue(expressions[index]);
                        result = DoMath(result, number, instruction);
                    }
                    else
                    {
                        Console.WriteLine("ERROR: can not parse string to long: {0}", expressions[index]);
                    }
                }

                index++;
            }

            return (result, index);
        }


        public long DoMath(long valueA, long valueB, char instruction)
        {
            long result = 0;
            switch (instruction)
            {
                case '+': result = valueA + valueB; break;
                case '*': result = valueA * valueB; break;
                default: Console.WriteLine("Unknown instruction! {0}", instruction); break;
            }
            // Console.WriteLine("Math: {0}{1}{2}={3}", valueA, instruction, valueB, result);

            return result;
        }

        public string GetInput(bool testInput)
        {

            var myInput = new Inputs.Day18();
            return (testInput) ? myInput.testInput : myInput.input;
        }
    }
}
