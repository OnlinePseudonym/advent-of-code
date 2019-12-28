using System;
using System.Collections.Generic;
using AdventOfCode.Enums;

namespace AdventOfCode.Models
{

    class Instruction
    {
        public OpCode OpCode { get; set; }
        public int FirstParameter { get; set; }
        public int FirstMode { get; set; }
        public int SecondParameter { get; set; }
        public int SecondMode { get; set; }
        public int ThirdParameter { get; set; }
        public int ThirdMode { get; set; }
        public int InstructionLength { get; set; }
        public IList<int> Memory { get; set; }

        public Instruction(IList<int> addresses, int pointerInstruction)
        {
            var instructionParameter = Utilities.digitArr(addresses[pointerInstruction]);

            OpCode = (OpCode)getOpCodeFromParameter(instructionParameter);
            FirstParameter = addresses[pointerInstruction + 1];
            FirstMode = instructionParameter.Length > 2 ? instructionParameter[instructionParameter.Length - 3] : 0;
            SecondParameter = addresses[pointerInstruction + 2];
            SecondMode = instructionParameter.Length > 3 ? instructionParameter[instructionParameter.Length - 4] : 0;
            ThirdParameter = addresses[pointerInstruction + 3];
            ThirdMode = instructionParameter.Length > 4 ? instructionParameter[instructionParameter.Length - 5] : 0;
            Memory = addresses;

            if (OpCode == OpCode.Addition || OpCode == OpCode.Multiplication)
            {
                InstructionLength = 4;
            }
            else if (OpCode == OpCode.SaveInput || OpCode == OpCode.Output)
            {
                InstructionLength = 2;
            }
            else
            {
                InstructionLength = 1;
            }
        }

        public int ProcessInstruction()
        {
            switch (OpCode)
            {
                case OpCode.Addition:
                    Memory[ThirdParameter] = getValueOfInstruction(FirstParameter, FirstMode) + getValueOfInstruction(SecondParameter, SecondMode);
                    break;
                case OpCode.Multiplication:
                    Memory[ThirdParameter] = getValueOfInstruction(FirstParameter, FirstMode) * getValueOfInstruction(SecondParameter, SecondMode);
                    break;
                case OpCode.SaveInput:
                    Console.WriteLine("Enter the ID of the system to test: ");
                    var input = int.Parse(Console.ReadLine());
                    Memory[FirstParameter] = input;
                    break;
                case OpCode.Output:
                    Console.WriteLine(getValueOfInstruction(FirstParameter, FirstMode));
                    break;
                case OpCode.Halt:
                    return -1;
                default:
                    break;
            }

            return 0;
        }

        private int getValueOfInstruction(int parameter, int mode)
        {
            if (mode == 0)
            {
                return Memory[parameter];
            }

            return parameter;
        }

        private int getOpCodeFromParameter(int[] parameter)
        {
            var firstDigit = parameter.Length > 1 ? parameter[parameter.Length - 2] : 0;
            var secondDigit = parameter[parameter.Length - 1];

            var opCodeString = firstDigit.ToString() + secondDigit.ToString();
            int.TryParse(opCodeString, out int output);

            return output;
        }
    }
}
