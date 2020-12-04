using System;
using System.IO;
using System.Linq;
using System.Collections.Generic; //For list
using System.Text.RegularExpressions; // For regex

namespace AdventOfCode2020
{
    public class Day04
    {
        public string SolvePart1(string input)
        {

            //Convert input to array of integers.
            string[] lines = input.Split(Environment.NewLine);

            //Create list for all passwords
            var passports = new List<string>();
            int nrValidPassports;

            //Convert lines to passport list
            passports = CreatePasswordList(lines);

            //Count the number of valid passports
            nrValidPassports = countNrValidPassportsBasic(passports);

            return nrValidPassports.ToString();
        }



        public string SolvePart2(string input)
        {
            //Convert input to array of integers.
            string[] lines = input.Split(Environment.NewLine);

            //Create list for all passwords
            var passports = new List<string>();
            int nrValidPassports;

            //Convert lines to passport list
            passports = CreatePasswordList(lines);

            //Count the number of valid passports
            nrValidPassports = countNrValidPassportsExtra(passports);

            return nrValidPassports.ToString();

        }


        public List<string> CreatePasswordList(string[] lines)
        {
            //Create list for all passwords
            var passports = new List<string>();
            string passport = "";

            //For all lines 
            foreach (string line in lines)
            {
                //Check for empty line
                if (line.Length == 0)
                {
                    //Empty line --> Add passport to list
                    passports.Add(passport);
                    //Reset passport data
                    passport = "";
                }
                else
                {
                    //Remember passport info for this line
                    passport = passport + line + " ";
                }
            }

            //Add last passport (no empty line at end of file)
            passports.Add(passport);

            return passports;
        }

        public int countNrValidPassportsExtra(List<string> passports)
        {
            // A passport is valid when it contains the following items:
            // byr (Birth Year) - four digits; at least 1920 and at most 2002.
            // iyr (Issue Year) - four digits; at least 2010 and at most 2020.
            // eyr (Expiration Year) - four digits; at least 2020 and at most 2030.
            // hgt (Height) - a number followed by either cm or in:
            // If cm, the number must be at least 150 and at most 193.
            // If in, the number must be at least 59 and at most 76.
            // hcl (Hair Color) - a # followed by exactly six characters 0-9 or a-f.
            // ecl (Eye Color) - exactly one of: amb blu brn gry grn hzl oth.
            // pid (Passport ID) - a nine-digit number, including leading zeroes.
            // cid (Country ID) - ignored, missing or not.

            //Regex pattern for a word starts with a needed item
            //Note: This only works one each item exist not more than ones!!
            string pattern = @"\bbyr:|iyr:|eyr:|hgt:|hcl:|ecl:|pid:";
            var myRegex = new Regex(pattern);
            int nrValidPassports = 0;


            foreach (string checkPassport in passports)
            {
   
                //Check if all 7 items accour
                if (myRegex.Matches(checkPassport).Count == 7)
                {

                    char[] separatingStrings = { ' ' }; //Char used for splitting
                    string[] items = checkPassport.Split(separatingStrings); //Split line to string array

                    int nrValid = 0;

                    for (int i = 0; i < items.Length; i++)
                    {

                        string[] item = items[i].Split(':'); //Split line to string array

                        switch (item[0])
                        {
                            case "byr":
                                if (int.Parse(item[1]) >= 1920 && int.Parse(item[1]) <= 2002)
                                {
                                    nrValid++;

                                }

                                break;

                            case "iyr":
                                if (int.Parse(item[1]) >= 2010 && int.Parse(item[1]) <= 2020)
                                {
                                    nrValid++;
                                }

                                break;

                            case "eyr":
                                if (int.Parse(item[1]) >= 2020 && int.Parse(item[1]) <= 2030)
                                {
                                    nrValid++;
                                }

                                break;

                            case "hgt":
                                string unit = item[1].Substring(item[1].Length - 2);
                                string heightString = item[1].Substring(0, item[1].Length - 2);
                                int height = int.Parse(heightString);

                                if (unit.CompareTo("cm") == 0)
                                {
                                    if (height >= 150 && height <= 193)
                                    {
                                        nrValid++;
                                    }
                                }

                                if (unit.CompareTo("in") == 0)
                                {
                                    if (height >= 59 && height <= 76)
                                    {
                                        nrValid++;
                                    }
                                }

                                break;

                            case "hcl":
                                //^ start at beginning
                                //# first char must be #
                                //[0-f] next can be 0-9 or a-f
                                //{6} must be 6 numbers
                                string patternHC1 = @"^#[0-f]{6}";
                                var myRegexHC1 = new Regex(patternHC1);

                                if (myRegexHC1.Matches(item[1]).Count == 1)
                                {
                                    nrValid++;
                                }

                                break;

                            case "ecl":
                                if (item[1].CompareTo("amb") == 0 || item[1].CompareTo("blu") == 0 || item[1].CompareTo("brn") == 0 || item[1].CompareTo("gry") == 0 || item[1].CompareTo("grn") == 0 || item[1].CompareTo("hzl") == 0 || item[1].CompareTo("oth") == 0)
                                {
                                    nrValid++;
                                }

                                break;

                            case "pid":
                                //^ start at beginning
                                //[0-9] allow numbers 0 t/m 9
                                //{9} must be 9 numbers
                                //$ stop at the end
                                string patternPID = @"^[0-9]{9}$";
                                var myRegexPID = new Regex(patternPID);

                                if (myRegexPID.Matches(item[1]).Count == 1)
                                {
                                    nrValid++;
                                }

                                break;
                        }
                    }

                    //If all 7 are valid
                    if (nrValid == 7)
                    {
                        nrValidPassports++;
                    }

                }

            }

            return nrValidPassports;

        }



        public int countNrValidPassportsBasic(List<string> passports)
        {
            // A passport is valid when it contains the following items:
            // byr (Birth Year)
            // iyr (Issue Year)
            // eyr (Expiration Year)
            // hgt (Height)
            // hcl (Hair Color)
            // ecl (Eye Color)
            // pid (Passport ID)
            // optional: cid (Country ID)

            //Regex pattern for a word starts with a needed item
            //Note: This only works one each item exist not more than ones!!
            string pattern = @"\bbyr:|iyr:|eyr:|hgt:|hcl:|ecl:|pid";
            var myRegex = new Regex(pattern);
            int nrValidPassports = 0;

            foreach (string checkPassport in passports)
            {
                //Check if all 7 items accour
                if (myRegex.Matches(checkPassport).Count == 7)
                {
                    //Console.WriteLine("Check");
                    nrValidPassports++;
                }
            }

            return nrValidPassports;

        }


        public string GetInput(bool testInput)
        {

            var myInput = new Inputs.Day04();
            return (testInput) ? myInput.testInput : myInput.input;
        }

    }



}
