using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AOC_2020
{
    public class Day14
    {
        public string path = "\\AOC 2020\\Day14\\InputDay14.txt";

        private MainProgram mainProgram = new MainProgram();

        public void RunDay14Part1()
        {
            var programLines = mainProgram.ConvertFileToLines(path);

            var sumvalues = RunProgram(programLines);

            Console.WriteLine($"Total sum of values left is {sumvalues}");
        }

        public void RunDay14Part2()
        {
            var programLines = mainProgram.ConvertFileToLines(path);

            var sumvalues = RunProgramVersie2(programLines);

            Console.WriteLine($"Total sum of values left is {sumvalues}");
        }

        public long RunProgram(List<string> lines)
        {
            var currentMask = string.Empty;
            var memory = new Dictionary<long, long>();

            foreach (var line in lines)
            {
                var keyValue = line.Split(new string[] { " = " }, StringSplitOptions.None);
                if (keyValue[0] == "mask")
                {
                    currentMask = keyValue[1];
                }
                else
                {
                    var number = Convert.ToInt64(keyValue[1]);
                    var binaryNumber = ToBinary(number);
                    var correctBitBinaryNumber = binaryNumber.PadLeft(36, '0');
                    for (var i = 0; i < correctBitBinaryNumber.Length; i++)
                    {
                        switch (currentMask[i])
                        {
                            case 'X':
                                break;
                            case '1':
                            case '0':
                                correctBitBinaryNumber = correctBitBinaryNumber.Remove(i,1);
                                correctBitBinaryNumber = correctBitBinaryNumber.Insert(i, currentMask[i].ToString());
                                break;
                        }
                    }
                    var correctedBinaryNumber = correctBitBinaryNumber.ToString();
                    string stringTest = (string)correctBitBinaryNumber.ToString();
                    var correctNumber = (long)Convert.ToInt64(correctBitBinaryNumber, 2);

                    var memoryPosition = long.Parse(Regex.Match(keyValue[0], @"[0-9]+").ToString());
                    memory[memoryPosition] = correctNumber;
                }
            }
            long totalSum = 0;
            foreach(var value in memory.Values)
            {
                totalSum += value;
            }

            return totalSum;
        }

        public long RunProgramVersie2(List<string> lines)
        {
            var currentMask = string.Empty;
            var memory = new Dictionary<long, long>();

            foreach (var line in lines)
            {
                var keyValue = line.Split(new string[] { " = " }, StringSplitOptions.None);
                if (keyValue[0] == "mask")
                {
                    currentMask = keyValue[1];
                }
                else
                {
                    var number = Convert.ToInt64(keyValue[1]);
                    var memoryPosition = long.Parse(Regex.Match(keyValue[0], @"[0-9]+").ToString());
                    var binaryNumber = ToBinary(memoryPosition);
                    var correctBitBinaryNumber = binaryNumber.PadLeft(36, '0');
                    var memorypositionsBinary = new List<string>() { correctBitBinaryNumber };
                    for (var i = 0; i < correctBitBinaryNumber.Length; i++)
                    {
                        switch (currentMask[i])
                        {
                            case 'X':
                                foreach(var position in memorypositionsBinary.ToList())
                                {
                                    var value0 = ReplaceSpecificCharacter('0', position, i);
                                    var value1 = ReplaceSpecificCharacter('1', position, i);
                                    memorypositionsBinary.Remove(position);
                                    memorypositionsBinary.AddRange(new List<string> { value0, value1 });
                                }
                                break;
                            case '1':
                                foreach (var position in memorypositionsBinary.ToList())
                                {
                                    var value = ReplaceSpecificCharacter('1', position, i);
                                    memorypositionsBinary.Remove(position);
                                    memorypositionsBinary.Add(value);
                                }
                                break;
                            case '0':
                                break;
                        }
                    }
                    foreach(var binaryPosition in memorypositionsBinary)
                    {
                        var correctPosition = Convert.ToInt64(binaryPosition, 2);
                        memory[correctPosition] = number;
                    }
                }
            }
            long totalSum = 0;
            foreach (var value in memory.Values)
            {
                totalSum += value;
            }

            return totalSum;
        }

        private static string ReplaceSpecificCharacter(char character, string correctBitBinaryNumber, int i)
        {
            correctBitBinaryNumber = correctBitBinaryNumber.Remove(i, 1);
            correctBitBinaryNumber = correctBitBinaryNumber.Insert(i, character.ToString());
            return correctBitBinaryNumber;
        }

        public static string ToBinary(Int64 Decimal)
        {
            // Declare a few variables we're going to need
            Int64 BinaryHolder;
            char[] BinaryArray;
            string BinaryResult = "";

            while (Decimal > 0)
            {
                BinaryHolder = Decimal % 2;
                BinaryResult += BinaryHolder;
                Decimal = Decimal / 2;
            }

            BinaryArray = BinaryResult.ToCharArray();
            Array.Reverse(BinaryArray);
            BinaryResult = new string(BinaryArray);

            return BinaryResult;
        }
    }
}
