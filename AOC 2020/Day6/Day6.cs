using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AOC_2020
{
    public class Day6
    {
        public string path = "\\AOC 2020\\Day6\\InputDay6.txt";

        public MainProgram mainProgram = new MainProgram();

        public void RunDay6Part1()
        {
            var totalSum = 0;

            var groups = mainProgram.ConvertFileToParagraphs(path);

            foreach(var group in groups)
            {
                totalSum += CountofAnswersWithYesInGroup(group);                
            }

            Console.WriteLine($"The total sum of the yes answers per group is {totalSum}");
        }

        public void RunDay6Part2()
        {
            var totalSum = 0;

            var groups = mainProgram.ConvertFileToParagraphs(path);

            foreach (var group in groups)
            {
                totalSum += CountOfAnswersWithAllYesInGroup(group);
            }

            Console.WriteLine($"The total sum of the yes answers per group is {totalSum}");
        }

        public int CountofAnswersWithYesInGroup(string group)
        {
            var answersForGroup = mainProgram.SplitParagraphsByLineEndings(group);

            var answersWithoutSpaces = answersForGroup.Replace(" ", "");
                
            var differentAnswers = answersWithoutSpaces.Distinct();

            return differentAnswers.Count();
        }

        public int CountOfAnswersWithAllYesInGroup(string group)
        {
            var answersWithAllYes = 0;

            var answersForGroup = mainProgram.SplitParagraphsByLineEndingsAndSpaces(group);

            var longestAnswers = answersForGroup.OrderByDescending(x => x.Length).First();

            foreach(var answer in longestAnswers.ToCharArray())
            {
                var answersNeeded = answersForGroup.Count;

                foreach(var personalAnswers in answersForGroup)
                {
                    if (personalAnswers.Contains(answer))
                        answersNeeded += - 1;
                }
                if(answersNeeded == 0)
                {
                    answersWithAllYes++;
                }
            }

            return answersWithAllYes;
        }
    }
}
