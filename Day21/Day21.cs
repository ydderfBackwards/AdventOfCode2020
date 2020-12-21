using System;
using System.IO;
using System.Linq;
using System.Collections.Generic; //For list
using System.Text.RegularExpressions; // For regex

namespace AdventOfCode2020
{
    public class Day21
    {
        public string SolvePart1(string input)
        {

            var (ingredients, allergens, foods) = ReadInput(input);
            var allergenFreeIngredients = FindAllergenFreeIngredients(ingredients, allergens, foods);
            var result = CountIngredients(allergenFreeIngredients, foods);

            return result.ToString();
        }



        public string SolvePart2(string input)
        {
            var (ingredients, allergens, foods) = ReadInput(input);
            var allergenIngredients = FindAllergenIngredients(ingredients, allergens, foods);
            var result = CreateIngredientString(allergenIngredients);

            return result;
        }

        public string CreateIngredientString(Dictionary<string, string> allergenIngredients)
        {
            //***** My old solution ****//
            // string result = "";
            // foreach (var ingredient in allergenIngredients)
            // {
            //     result += ingredient.Key;
            //     result += ",";
            // }
            // result = result.Substring(0, result.Length - 1); //Remove last ","

            //***** Solution found in solution of Tweakers.net user Woy ****//
            string result = string.Join(",", allergenIngredients.Select(x => x.Key));

            return result;
        }

        public long CountIngredients(List<string> ingredients, List<Food> foods)
        {
            long count = 0;

            foreach (string ingredient in ingredients)
            {
                foreach (Food food in foods)
                {
                    if (food.Ingredients.Any(x => x.Equals(ingredient)))
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        public Dictionary<string, string> FindAllergenIngredients(List<string> ingredients, List<string> allergens, List<Food> foods)
        {
            List<string> allergenFreeIngredients = new List<string>(ingredients);
            Dictionary<string, string> allergenIngredients = new Dictionary<string, string>();

            bool madeChange = true;
            while (madeChange == true) //As long a we find a match, keep checking
            {
                madeChange = false;
                foreach (string allergen in allergens) //Check all unique allergens one by one
                {
                    List<string> possibleIngredients = new List<string>(allergenFreeIngredients);   //Create a list of ingredients who don't have a allergen yet
                    List<string> tmpIngredients = new List<string>();   //Temp list for storing a new list of possible ingredients

                    foreach (Food food in foods)    //Check all foods
                    {
                        if (food.Allergens.Any(x => x.Equals(allergen)))  //If this food has this allergen
                        {
                            foreach (string ingredient in food.Ingredients) //Check all ingredients in this food
                            {
                                if (possibleIngredients.Any(x => x.Equals(ingredient))) //If the ingredient is on the list of possibleIngredients, then store is in the temp list
                                {
                                    tmpIngredients.Add(ingredient);
                                }
                            }

                            possibleIngredients = new List<string>(tmpIngredients); //The new possible ingredients list, is the templist (so all not anymore possible ingredients get removed)
                            tmpIngredients = new List<string>();    //Reset the temp list.
                        }
                    }

                    if (possibleIngredients.Count() == 1) //If there is only one ingredient left ==> this should be the one. Remove it from the ingredients list (so it's not available for the next allergen)
                    {
                        allergenFreeIngredients.Remove(possibleIngredients.First());
                        allergenIngredients.Add(possibleIngredients.First(), allergen);

                        madeChange = true;
                    }
                }
            }
            //Order by allergen and convert result back to dictionary
            return allergenIngredients.OrderBy(i => i.Value).ToDictionary((keyItem) => keyItem.Key, (valueItem) => valueItem.Value);

        }
        public List<string> FindAllergenFreeIngredients(List<string> ingredients, List<string> allergens, List<Food> foods)
        {
            List<string> allergenFreeIngredients = new List<string>(ingredients);
            bool madeChange = true;
            while (madeChange == true) //As long a we find a match, keep checking
            {
                madeChange = false;
                foreach (string allergen in allergens) //Check all unique allergens one by one
                {
                    List<string> possibleIngredients = new List<string>(allergenFreeIngredients);   //Create a list of ingredients who don't have a allergen yet
                    List<string> tmpIngredients = new List<string>();   //Temp list for storing a new list of possible ingredients

                    foreach (Food food in foods)    //Check all foods
                    {
                        if (food.Allergens.Any(x => x.Equals(allergen)))  //If this food has this allergen
                        {
                            foreach (string ingredient in food.Ingredients) //Check all ingredients in this food
                            {
                                if (possibleIngredients.Any(x => x.Equals(ingredient))) //If the ingredient is on the list of possibleIngredients, then store is in the temp list
                                {
                                    tmpIngredients.Add(ingredient);
                                }
                            }

                            possibleIngredients = new List<string>(tmpIngredients); //The new possible ingredients list, is the templist (so all not anymore possible ingredients get removed)
                            tmpIngredients = new List<string>();    //Reset the temp list.
                        }
                    }

                    if (possibleIngredients.Count() == 1) //If there is only one ingredient left ==> this should be the one. Remove it from the ingredients list (so it's not available for the next allergen)
                    {
                        allergenFreeIngredients.Remove(possibleIngredients.First());
                        madeChange = true;
                    }
                }
            }

            return allergenFreeIngredients;
        }

        public (List<string>, List<string>, List<Food>) ReadInput(string input)
        {
            /*******************************************************/
            /* Return: Unique ingredients, Unique allergen, foods  */
            /*******************************************************/

            List<Food> foods = new List<Food>();
            List<string> ingredients = new List<string>();
            List<string> allergens = new List<string>();

            string[] lines = input.Split(Environment.NewLine);

            foreach (string line in lines)
            {
                string[] data = line.Split(" (contains ", StringSplitOptions.RemoveEmptyEntries);

                string[] ingredient = data[0].Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                string[] allergen = data[1].Trim().Split(new char[] { ',', ')' }, StringSplitOptions.RemoveEmptyEntries);


                Food tmpFood = new Food();
                tmpFood.Ingredients = new List<string>();
                tmpFood.Allergens = new List<string>();

                foreach (string p in ingredient)
                {
                    ingredients.Add(p.Trim());
                    tmpFood.Ingredients.Add(p.Trim());
                }

                foreach (string i in allergen)
                {
                    allergens.Add(i.Trim());
                    tmpFood.Allergens.Add(i.Trim());
                }

                foods.Add(tmpFood);
            }

            ingredients = ingredients.Distinct().ToList();
            allergens = allergens.Distinct().ToList();

            return (ingredients, allergens, foods);
        }

        public class Food
        {
            public List<string> Ingredients;
            public List<string> Allergens;

        }

        public string GetInput(bool testInput)
        {

            var myInput = new Inputs.Day21();
            return (testInput) ? myInput.testInput : myInput.input;
        }
    }
}
