using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC_2020
{
    public class Day2
    {
        private string path = "\\AOC 2020\\Day2\\InputDay2.txt";

        private MainProgram mainProgram = new MainProgram();

        public void RunDay2Part1()
        {
            int validPasswordCounts = 0;

            var lines = mainProgram.ConvertFileToLines(path);

            foreach(var line in lines)
            {
                var valid = CheckPassword(line);
                if (valid)
                {
                    validPasswordCounts++;
                }
            }

            Console.WriteLine($"The count of valid passports is {validPasswordCounts} ");
        }

        public void RunDay2Part2()
        {
            int validPasswordCounts = 0;

            var lines = mainProgram.ConvertFileToLines(path);

            foreach (var line in lines)
            {
                var valid = CheckPassword2(line);
                if (valid)
                {
                    validPasswordCounts++;
                }
            }

            Console.WriteLine($"The count of valid passports is {validPasswordCounts} ");
        }

        public bool CheckPassword2(string line)
        {
            var variables = InterpretString(line);

            var condition1 = variables.Password[variables.FirstValue - 1] == variables.Character;
            var condition2 = variables.Password[variables.SecondValue - 1] == variables.Character;

            return ((condition1 || condition2) && !(condition1 && condition2));
        }

        public bool CheckPassword(string line)
        {
            var variables = InterpretString(line);
            
            var count = variables.Password.Count(x => x == variables.Character);

            return (count >= variables.FirstValue) && (count <= variables.SecondValue);
        }

        public Variables InterpretString(string line)
        {
            var variables = new Variables();

            var splitStrings = line.Split('-', ' ', ':');

            variables.FirstValue = int.Parse(splitStrings[0]);
            variables.SecondValue = int.Parse(splitStrings[1]);
            char[] letters = splitStrings[2].ToCharArray();
            if (letters.Count() != 1)
            {
                throw new ArgumentException("Something is wrong");
            }
            variables.Character = letters[0];
            variables.Password = splitStrings[4];

            return variables;
        }

        public class Variables
        {
            public int FirstValue { get; set; }
            public int SecondValue { get; set; }
            public char Character { get; set; }
            public string Password { get; set; }
        }
    }
}
