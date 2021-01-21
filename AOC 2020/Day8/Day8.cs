using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace AOC_2020
{
    public class Day8
    {
        public string path = "\\AOC 2020\\Day8\\InputDay8.txt";

        public MainProgram mainProgram = new MainProgram();

        public void RunDay8Part1()
        {
            Instruction[] Instructions = CreateInstructions();

            var result = ProcessInstructions(Instructions);

            Console.WriteLine($"The accumulator is {result.Accumulator} at the moment an instruction is executed a second time");
        }

        public void RunDay8Part2()
        {
            Instruction[] instructions = CreateInstructions();
            var answer = FixProgram(instructions);

            Console.WriteLine($"The accumulator is {answer} after the program terminated correctly");
        }

        public int FixProgram(Instruction[] instructions)
        {
            ProgramResult result = new ProgramResult(); 

            for (int i=0; i < instructions.Length; i++)
            {
                var instruction = instructions[i];

                if (instruction.Operation == Operation.nop)
                {
                    instruction.Operation = Operation.jmp;
                    result = ProcessInstructions(instructions);
                    instruction.Operation = Operation.nop;
                }
                else if (instruction.Operation == Operation.jmp)
                {
                    instruction.Operation = Operation.nop;
                    result = ProcessInstructions(instructions);
                    instruction.Operation = Operation.jmp;
                }
                if(result.NormalTermination == true)
                {
                    return result.Accumulator;
                }

                foreach (var reset in instructions)
                {
                    reset.Visits = 0;
                }
            }            

            throw new ArgumentException("The program couldn't be fixed");
        }

        public Instruction CreateInstruction(string line)
        {
            return new Instruction
            {
                Operation = (Operation)Enum.Parse(typeof(Operation), line.Substring(0, 3)),
                Argument = int.Parse(line.Substring(4, line.Length - 4)),
                Visits = 0
            };
        }

        public ProgramResult ProcessInstructions(Instruction[] instructions)
        {
            bool repeat = false;
            int index = 0;
            ProgramResult result = new ProgramResult { Accumulator = 0, NormalTermination = false };

            try
            {
                while (!repeat)
                { 
                
                    var current = instructions[index];
                    if (current.Visits == 0)
                    {
                        switch (current.Operation)
                        {
                            case Operation.nop:
                                index++;
                                break;
                            case Operation.acc:
                                result.Accumulator += current.Argument;
                                index++;
                                break;
                            case Operation.jmp:
                                index += current.Argument;
                                break;
                        }
                    }
                    else
                    {
                        repeat = true;
                    }

                    current.Visits++;
                }
            }
            catch (Exception e)
            {
                result.NormalTermination = true;
            }

            return result;
        }

        private Instruction[] CreateInstructions()
        {
            var lines = mainProgram.ConvertFileToLines(path);
            Instruction[] Instructions = new Instruction[lines.Count];
            int index = 0;

            foreach (var line in lines)
            {
                Instructions.SetValue(CreateInstruction(line), index);
                index++;
            }

            return Instructions;
        }
    }

    public class Instruction
    {
        public Operation Operation { get; set; }
        public int Argument { get; set; }
        public int Visits { get; set; }
    }

    public class ProgramResult
    {
        public bool NormalTermination { get; set; }
        public int Accumulator { get; set; }
    }

    public enum Operation
    {
        [Description("No operation")]
        nop,
        [Description("Jumps")]
        jmp,
        [Description("Accumulator")]
        acc
    }
}
