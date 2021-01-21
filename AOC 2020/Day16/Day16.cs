using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC_2020
{
    public class Day16
    { 
        MainProgram mainProgram = new MainProgram();

        List<string> _paragraphs;
        Dictionary<string, List<int>> _rules;
        Dictionary<int, List<int>> _tickets = new Dictionary<int, List<int>>();

        public Day16(string path)
        {
            _paragraphs = mainProgram.ConvertFileToParagraphs(path);

            _rules = ProcessRules(_paragraphs[0]);
        }

        public int RunDay16Part1()
        {
            int ticketScanningErrorRate = 0;
            int index = 0;

            var unprocessedTickets = mainProgram.SplitParagraphByLineEndings(_paragraphs[2]);
            unprocessedTickets.Remove("nearby tickets:");

            foreach(var unprocessedTicket in unprocessedTickets)
            {
                var processedTicket = CreateTicket(unprocessedTicket);
                var error = ScanTicket(processedTicket);
                ticketScanningErrorRate += error;
                if(error == 0)
                {
                    _tickets.Add(index, processedTicket);
                    index++;
                }
            }

            return ticketScanningErrorRate;
        }

        public Dictionary<string, int> RunDay16Part2(string fieldname)
        {
            var myValidatedTicket = new Dictionary<string, int>();
            var fieldOptions = new Dictionary<string, List<int>>();

            var myTicketString = mainProgram.SplitParagraphByLineEndings(_paragraphs[1])[1];
            var myTicket = CreateTicket(myTicketString);

            for (int fieldIndex = 0; fieldIndex < _tickets.First().Value.Count; fieldIndex++)
            {
                for (int ruleIndex = 0; ruleIndex < _rules.Count(); ruleIndex++)
                {
                    var isPossibleRule = true;

                    var rule = _rules.ElementAt(ruleIndex);
                    for (int ticketIndex = 0; ticketIndex < _tickets.Count && isPossibleRule; ticketIndex++)
                    {
                        var field = _tickets[ticketIndex][fieldIndex];

                        if (!rule.Value.Contains(field))
                            isPossibleRule = false;
                    }

                    if (isPossibleRule)
                    {
                        if (fieldOptions.ContainsKey(rule.Key))
                            fieldOptions[rule.Key].Add(fieldIndex);
                        else
                            fieldOptions.Add(rule.Key, new List<int> { fieldIndex });
                    }
                }
            }

            while (fieldOptions.Any(f => f.Key.Contains(fieldname)))
            {
                var easyFields = fieldOptions.Where(o => o.Value.Count == 1).ToList();
                foreach (var easyField in easyFields)
                {
                    var fieldValue = easyField.Value.First();
                    var fieldKey = easyField.Key;
                    myValidatedTicket.Add(fieldKey, myTicket[fieldValue]);
                    fieldOptions.Remove(fieldKey);
                    foreach (var option in fieldOptions)
                    {
                        if (option.Value.Contains(fieldValue))
                            option.Value.Remove(fieldValue);
                    }
                }
            }


            return myValidatedTicket;
        }

        private Dictionary<string, List<int>> ProcessRules(string rulesString)
        {
            var processedRules = new Dictionary<string, List<int>>();

            var rules = mainProgram.SplitParagraphByLineEndings(rulesString);
            foreach(var rule in rules)
            {
                var validValues = new List<int>();
                var nameAndValues = mainProgram.SplitLinesByCharacters(rule, ':');

                var valueRanges = nameAndValues[1].Split(new string[] { "or" }, StringSplitOptions.RemoveEmptyEntries);

                foreach(var valueRange in valueRanges)
                {
                    var borders = valueRange.Split('-');
                    var minValue = int.Parse(borders[0]);
                    var maxValue = int.Parse(borders[1]);
                    var values = Enumerable.Range(minValue, maxValue - minValue + 1);
                    foreach(var value in values)
                    {
                        validValues.Add(value);
                    }
                }

                processedRules.Add(nameAndValues[0], validValues);
            }

            return processedRules;
        }

        private List<int> CreateTicket(string unprocessedTicket)
        {
            var processedTicket = new List<int>();
            var fields = unprocessedTicket.Split(',');
            foreach (var field in fields)
            {
                processedTicket.Add(int.Parse(field));
            }
            return processedTicket;
        }

        private int ScanTicket(List<int> ticket)
        {
            var errorRate = 0;
            foreach(var value in ticket)
            {
                var isValid = false;
                for (int index = 0; index < _rules.Count && isValid == false; index ++)
                {
                    var rule = _rules.ElementAt(index);

                    if (rule.Value.Contains(value))
                        isValid = true;
                }
                
                if (isValid == false)
                    errorRate += value;
            }

            return errorRate;
        }
    }
}
