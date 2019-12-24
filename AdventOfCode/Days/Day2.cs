using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Days
{
    class Day2
    {
        public IList<int> InputList { get; set; } = new List<int>() { 1, 0, 0, 3, 1, 1, 2, 3, 1, 3, 4, 3, 1, 5, 0, 3, 2, 10, 1, 19, 1, 5, 19, 23, 1, 23, 5, 27, 2, 27, 10, 31, 1, 5, 31, 35, 2, 35, 6, 39, 1, 6, 39, 43, 2, 13, 43, 47, 2, 9, 47, 51, 1, 6, 51, 55, 1, 55, 9, 59, 2, 6, 59, 63, 1, 5, 63, 67, 2, 67, 13, 71, 1, 9, 71, 75, 1, 75, 9, 79, 2, 79, 10, 83, 1, 6, 83, 87, 1, 5, 87, 91, 1, 6, 91, 95, 1, 95, 13, 99, 1, 10, 99, 103, 2, 6, 103, 107, 1, 107, 5, 111, 1, 111, 13, 115, 1, 115, 13, 119, 1, 13, 119, 123, 2, 123, 13, 127, 1, 127, 6, 131, 1, 131, 9, 135, 1, 5, 135, 139, 2, 139, 6, 143, 2, 6, 143, 147, 1, 5, 147, 151, 1, 151, 2, 155, 1, 9, 155, 0, 99, 2, 14, 0, 0 };

        public IList<int> CalculateOutput(IList<int> inputList)
        {
            RestoreProgramState(inputList);

            for (var i = 0; i < inputList.Count; i += 4)
            {
                var instruction = new Instruction(inputList, i);
                var proccessed = instruction.ProcessInstruction();

                if (proccessed == -1) break;
            };

            return inputList;
        }

        private void RestoreProgramState(IList<int> inputList)
        {
            inputList[1] = 12;
            inputList[2] = 2;
        }
    }

    class Instruction
    {
        public OpCode OpCode { get; set; }
        public int FirstTerm { get; set; }
        public int SecondTerm { get; set; }
        public int TargetIndex { get; set; }
        public IList<int> InputList { get; set; }

        public Instruction(IList<int> inputList, int startIndex)
        {
            OpCode = (OpCode)inputList[startIndex];
            FirstTerm = inputList[startIndex + 1];
            SecondTerm = inputList[startIndex + 2];
            TargetIndex = inputList[startIndex + 3];
            InputList = inputList;
        }

        public int ProcessInstruction()
        {
            switch (OpCode)
            {
                case OpCode.Addition:
                    InputList[TargetIndex] = InputList[FirstTerm] + InputList[SecondTerm];
                    break;
                case OpCode.Multiplication:
                    InputList[TargetIndex] = InputList[FirstTerm] * InputList[SecondTerm];
                    break;
                case OpCode.Halt:
                    return -1;
                default:
                    break;
            }

            return 0;
        }
    }

    enum OpCode
    {
        Addition = 1,
        Multiplication = 2,
        Halt = 99
    }
}
