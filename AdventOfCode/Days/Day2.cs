﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Days
{
    class Day2
    {
        public IList<int> Memory { get; set; }

        public IList<int> CalculateOutput(IList<int> memory)
        {
            RestoreProgramState(memory);

            for (var i = 0; i < memory.Count; i += 4)
            {
                var instruction = new Instruction(memory, i);
                var proccessed = instruction.ProcessInstruction();

                if (proccessed == -1) break;
            };

            return memory;
        }

        public void InitilizeMemory()
        {
            Memory = new List<int>() { 1, 0, 0, 3, 1, 1, 2, 3, 1, 3, 4, 3, 1, 5, 0, 3, 2, 10, 1, 19, 1, 5, 19, 23, 1, 23, 5, 27, 2, 27, 10, 31, 1, 5, 31, 35, 2, 35, 6, 39, 1, 6, 39, 43, 2, 13, 43, 47, 2, 9, 47, 51, 1, 6, 51, 55, 1, 55, 9, 59, 2, 6, 59, 63, 1, 5, 63, 67, 2, 67, 13, 71, 1, 9, 71, 75, 1, 75, 9, 79, 2, 79, 10, 83, 1, 6, 83, 87, 1, 5, 87, 91, 1, 6, 91, 95, 1, 95, 13, 99, 1, 10, 99, 103, 2, 6, 103, 107, 1, 107, 5, 111, 1, 111, 13, 115, 1, 115, 13, 119, 1, 13, 119, 123, 2, 123, 13, 127, 1, 127, 6, 131, 1, 131, 9, 135, 1, 5, 135, 139, 2, 139, 6, 143, 2, 6, 143, 147, 1, 5, 147, 151, 1, 151, 2, 155, 1, 9, 155, 0, 99, 2, 14, 0, 0 };
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
        public int FirstParameter { get; set; }
        public int SecondParameter { get; set; }
        public int TargetAddress { get; set; }
        public IList<int> Memory { get; set; }

        public Instruction(IList<int> addresses, int pointerInstruction)
        {
            OpCode = (OpCode)addresses[pointerInstruction];
            FirstParameter = addresses[pointerInstruction + 1];
            SecondParameter = addresses[pointerInstruction + 2];
            TargetAddress = addresses[pointerInstruction + 3];
            Memory = addresses;
        }

        public int ProcessInstruction()
        {
            switch (OpCode)
            {
                case OpCode.Addition:
                    Memory[TargetAddress] = Memory[FirstParameter] + Memory[SecondParameter];
                    break;
                case OpCode.Multiplication:
                    Memory[TargetAddress] = Memory[FirstParameter] * Memory[SecondParameter];
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
