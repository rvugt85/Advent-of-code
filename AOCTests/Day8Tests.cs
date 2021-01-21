using AOC_2020;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace AOCTests
{
    [TestClass]
    public class Day8Tests
    {
        public Day8 Day8 = new Day8();

        public Instruction[] Instructions = new Instruction[]
            {
            new Instruction{ Operation = Operation.nop, Argument = 0, Visits = 0 },
            new Instruction{ Operation = Operation.acc, Argument = 1, Visits =0 },
            new Instruction{ Operation = Operation.jmp, Argument = 4, Visits =0 },
            new Instruction{ Operation = Operation.acc, Argument = 3, Visits =0 },
            new Instruction{ Operation = Operation.jmp, Argument = -3, Visits =0 },
            new Instruction{ Operation = Operation.acc, Argument = -99, Visits = 0 },
            new Instruction{ Operation = Operation.acc, Argument =1, Visits =0 },
            new Instruction{ Operation = Operation.jmp, Argument =-4, Visits =0 },
            new Instruction{ Operation = Operation.acc, Argument = 6, Visits =0 }
            };

        [TestMethod]
        [DataRow("nop +0", Operation.nop, 0)]
        [DataRow("acc -99", Operation.acc, -99)]
        [DataRow("jmp +7", Operation.jmp, 7)]
        public void InstructionCreationTest(string line, Operation expectedOp, int expectedArgument)
        {
            var actualInstruction = Day8.CreateInstruction(line);

            Assert.AreEqual(expectedOp, actualInstruction.Operation);
            Assert.AreEqual(expectedArgument, actualInstruction.Argument);
            Assert.AreEqual(0, actualInstruction.Visits);    
        }

        [TestMethod]
        public void InstructionExecutionTest()
        {
            var actualResult = Day8.ProcessInstructions(Instructions);

            Assert.AreEqual(5, actualResult.Accumulator);
            Assert.AreEqual(false, actualResult.NormalTermination);
        }

        [TestMethod]
        public void FixProgramTest()
        {
            var actualResult = Day8.FixProgram(Instructions);

            Assert.AreEqual(8, actualResult);
        }
    }
}
