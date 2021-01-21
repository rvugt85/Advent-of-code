using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC_2020
{
    public class Day21
    {
        MainProgram mainProgram = new MainProgram();

        public Dictionary<string, List<string>> AllergensByPossibleIngredients = new Dictionary<string, List<string>>();
        public List<string> AppearancesOfIngredientsNotContainingAllergens = new List<string>();
        public List<string> AppearancesOfIngredientsInTotal = new List<string>();

        public Day21(string path)
        {
            var paragraphs = mainProgram.ConvertFileToParagraphs(path);
            var foods = mainProgram.SplitParagraphByLineEndings(paragraphs[0]);

            foreach(var food in foods)
            {
                var match = Regex.Match(food, "(.*)\\((.*)\\)");

                var ingredients = match.Groups[1].Value.Trim().Split(' ').ToList();
                var allergens = match.Groups[2].Value.Substring(9).Split(", ").ToList();

                AppearancesOfIngredientsInTotal.AddRange(ingredients);

                //var removedIngredients = new List<string>();
                //var possibleIngredients = new List<string>();

                foreach (var allergen in allergens)
                {
                    if (!AllergensByPossibleIngredients.ContainsKey(allergen))
                        AllergensByPossibleIngredients.Add(allergen, ingredients);
                    else
                    {
                        var ingredientsForThisAllergen = AllergensByPossibleIngredients.GetValueOrDefault(allergen).ToList();

                        AllergensByPossibleIngredients[allergen] = ingredientsForThisAllergen.Where(x => ingredients.Contains(x)).ToList();
                    }
                }
            }

            AppearancesOfIngredientsNotContainingAllergens = AppearancesOfIngredientsInTotal.ToList();

            foreach(var allergenIngredients in AllergensByPossibleIngredients.Values)
            {
                AppearancesOfIngredientsNotContainingAllergens
                    .RemoveAll(x => allergenIngredients.Contains(x));
            }

            GetUniqueIngredients();
        }

        public void RunDay21Part1()
        {
            var answer = GetIngredientAppearance();

            Console.WriteLine($"Ingredients who cannot possibly contain an allergen appear {answer} times in our list");
        }

        public void RunDay21Part2()
        {
            var answer = CreateCanonicalDangerousIngredientList();

            Console.WriteLine($"Canonical dangerous ingredient list: {answer}");
        }

        private void GetUniqueIngredients()
        {
            var ingredientsWithKnownAllergen = new List<string>();

            while(AllergensByPossibleIngredients.Values.Where(x => x.Count != 1).Count() != 0)
            {
                foreach (var ingredientForSingleAllergen in AllergensByPossibleIngredients.Values.Where(x => x.Count == 1))
                {
                    ingredientsWithKnownAllergen.AddRange(ingredientForSingleAllergen);
                }
                foreach (var ingredientForSingleAllergen in AllergensByPossibleIngredients.Values.Where(x => x.Count != 1))
                {
                    ingredientForSingleAllergen.RemoveAll(x => ingredientsWithKnownAllergen.Contains(x));
                }
            }            
        }

        public string CreateCanonicalDangerousIngredientList()
        {
            var canonicalList = string.Empty;
            var orderedKeys = AllergensByPossibleIngredients.Keys.OrderBy(x => x);

            foreach(var key in orderedKeys)
            {
                if (canonicalList == string.Empty)
                    canonicalList = AllergensByPossibleIngredients[key].First();
                else
                {
                    canonicalList = string.Concat(canonicalList, ",", AllergensByPossibleIngredients[key].First());
                }
            }

            return canonicalList;
        }

        public int GetIngredientAppearance()
        {
            return AppearancesOfIngredientsNotContainingAllergens.Count;
        }


        //public void RunDay20Part2()
        //{


        //    Console.WriteLine($"There are {notSeaMonster} waves in the water");
        //}

    }
}

