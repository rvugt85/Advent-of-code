using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC_2020
{
    public class Day19
    {
        MainProgram mainProgram = new MainProgram();

        private List<Rule> Rules = new List<Rule>();
        private List<string> Messages;

        public Day19(string path)
        {
            var paragraphs = mainProgram.ConvertFileToParagraphs(path);

            var ruleLines = mainProgram.SplitParagraphByLineEndings(paragraphs[0]);
            foreach (var ruleLine in ruleLines)
            {
                var splitRule = ruleLine.Split(": ");
                Rules.Add(new Rule { Index = int.Parse(splitRule[0]), RuleText = splitRule[1] });
            }
            Messages = mainProgram.SplitParagraphByLineEndings(paragraphs[1]);
        }

        public void RunDay19Part1()
        {
            var answer = CheckMatchingMessages(0);

            Console.WriteLine($"{answer} messages match rule 0");
        }

        public void RunDay19Part2()
        {
            var answer = CheckMatchingMessagesFromTheMessage();

            Console.WriteLine($"{answer} messages match rule 0 after changes");
        }

        public int CheckMatchingMessagesFromTheMessage()
        {
            var matchingMessages = 0;

            foreach(var message in Messages)
            {
                if (MessageIsValid(message))
                    matchingMessages++;
            }

            return matchingMessages;
        }

        public int CheckMatchingMessages(int index)
        {
            var matchingMessages = 0;

            var possibleOptions = GetOptions(index);

            foreach(var message in Messages)
            {
                if (possibleOptions.Contains(message))
                    matchingMessages++;
            }

            return matchingMessages;
        }

        private bool MessageIsValid(string message)
        {
            var isValid = false;

            var optionsRule42 = GetOptions(42);
            var optionsRule31 = GetOptions(31);

            var optionLength42 = optionsRule42.First().Length;
            var optionLength31 = optionsRule42.First().Length;

            var rule42Matches = 0;
            var rule31Matches = 0;

            while (optionsRule42.Contains(message.Substring(0, optionLength42)))
            {
                rule42Matches++;
                message = message.Remove(0, optionLength42);
                if(message.Length < optionLength42)
                        return false;
            }
            while (optionsRule31.Contains(message.Substring(0, optionLength31)))
            {
                rule31Matches++;
                message = message.Remove(0, optionLength31);
                if (message.Length < optionLength31)
                    break;
            }

            if (string.IsNullOrEmpty(message) && (rule42Matches > rule31Matches))
                isValid = true;
            return isValid;
        }

        private List<string> GetOptions(int index)
        {
            var rule = Rules.Where(r => r.Index == index).Single();

            if (rule.Options.Count != 0)
                return rule.Options;

            Match characterRule = Regex.Match(rule.RuleText, "\\\"(.*)\\\"");

            if (characterRule.Success)
            {
                rule.Options.Add(characterRule.Groups[1].Value);
                return rule.Options;
            }

            else
            {
                var ruleOptions = rule.RuleText.Split(" | ");
                foreach (var ruleOption in ruleOptions)
                {
                    var rulesToFollow = ruleOption.Split(' ');
                    var newMatches = new List<string>();
                    foreach (var ruleToFollow in rulesToFollow)
                    {
                        var matchesToAdd = GetOptions(int.Parse(ruleToFollow));
                        if (newMatches.Count == 0)
                            newMatches.AddRange(matchesToAdd);
                        else
                        {
                            foreach(var option in newMatches.ToArray())
                            {
                                foreach(var matchToAdd in matchesToAdd)
                                {
                                    var newMatch = string.Join("", new string[] { option, matchToAdd });
                                    newMatches.Add(newMatch);
                                }
                                newMatches.Remove(option);
                            }
                        }
                    }

                    rule.Options.AddRange(newMatches);
                }
                return rule.Options;
            }
        }
    }


    public class Rule
    {
        public Rule()
        {
            Options = new List<string>();
        }

        public int Index { get; set; }
        public string RuleText { get; set; }
        public List<string> Options { get; set; }
    }
}

