using System.Collections.Generic;
using AdventOfCode.Enums;

namespace AdventOfCode.Models
{

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
}
