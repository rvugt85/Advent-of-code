using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AOC_2020
{
    public class Day7
    {
        public string path = "\\AOC 2020\\Day7\\InputDay7.txt";

        public MainProgram mainProgram = new MainProgram();

        public void RunDay7Part1()
        {
            var colorAsked = "shiny gold";
            var rules = mainProgram.ConvertFileToLines(path);

            var colors = GetCountOfPossibleColors(rules, colorAsked);

            Console.WriteLine($"The total count of bags that can hold a {colorAsked} bag is {colors}");
        }

        public void RunDay7Part2()
        {
            var colorAsked = "shiny gold";
            var rules = mainProgram.ConvertFileToLines(path);

            var bagCount = GetCountOfBagsNeeded(rules, colorAsked);

            Console.WriteLine($"The total count of bags that should be inside a {colorAsked} bag is {bagCount}");
        }

        public long GetCountOfBagsNeeded(List<string> rules, string colorAsked)
        {
            var dict = RulesInDictionary(rules);

            return GetBagsInsideASpecificBag(dict, colorAsked, 1, 0);
            
        }

        private long GetBagsInsideASpecificBag(Dictionary<string, List<string>> dict, string colorAsked, int countOfBags, long currentSum)
        {
            var itemsForColorBags = dict.Where(i => i.Key == colorAsked);

            foreach(var item in itemsForColorBags)
            {
                foreach(var typeOfBag in item.Value)
                {
                    var count = 0;

                    var numberCharacters = Regex.Match(typeOfBag, @"\d+");
                    if(numberCharacters.Value.Count() != 0)
                    {
                        count = int.Parse(numberCharacters.Value);
                    }
                    var countOfThisBag = count * countOfBags;
                    currentSum += countOfThisBag;
                    var color = Regex.Replace(typeOfBag, @"[\d+]", "").Trim();
                    currentSum = GetBagsInsideASpecificBag(dict, color, countOfThisBag, currentSum);
                }
            }

            return currentSum;
        }

        public int GetCountOfPossibleColors(List<string> rules, string colorAsked)
        {
            return CalculateBagsThatCanContainAColorBag(rules,colorAsked).Distinct().Count();
        }

        public List<string> CalculateBagsThatCanContainAColorBag(List<string> rules, string colorAsked)
        {
            List<string> colors = new List<string>();

            var dict = RulesInDictionary(rules);

            foreach(var item in dict)
            {
                foreach(var typeOfBag in item.Value)
                {
                    if (typeOfBag.Contains(colorAsked))
                    {
                        var color = item.Key.Replace("bags", "").Replace("bag", "");
                        colors.Add(color);
                        colors.AddRange(CalculateBagsThatCanContainAColorBag(rules, color));
                    }
                }
                
            }

            return colors;
        }

        private Dictionary<string, List<string>> RulesInDictionary(List<string> rules)
        {
            var dict = new Dictionary<string, List<string>>();

            foreach (var rule in rules)
            {
                var splitRule = rule.Split(new string[] { " contain" }, StringSplitOptions.None);
                var colorOutside = splitRule[0].Replace(" bags", "");

                var colorsInside = Regex.Split(splitRule[1], @"bags?[,.]").ToList();
                
                dict.Add(colorOutside, colorsInside);
                
            }
            return dict;


        }
           
    }
}
