using System;
using System.Collections.Generic;
using AdventOfCode.Enums;

namespace AdventOfCode.Models
{

    class Instruction
    {
        private OpCode OpCode;
        private int FirstParameter;
        private int FirstMode;
        private int SecondParameter;
        private int SecondMode;
        private int ThirdParameter;
        private int PointerPosition;
        private IList<int> Memory;
        private bool IsAutomated;
        public int Output { get; private set; }
        private int Input;
        private int Phase;
        public bool PhaseInitialized { get; private set; }
        public bool InputUsed { get; private set; }
        public bool KillProgram { get; private set; }
        public bool Paused { get; private set; }
        public Instruction(IList<int> addresses, int pointerInstruction)
        {
            var instructionParameter = Utilities.digitArr(addresses[pointerInstruction]);

            PointerPosition = pointerInstruction;
            OpCode = (OpCode)getOpCodeFromParameter(instructionParameter);

            FirstParameter = addresses.Count - 1 > PointerPosition ? addresses[PointerPosition + 1] : 0;
            FirstMode = instructionParameter.Length > 2 ? instructionParameter[instructionParameter.Length - 3] : 0;
            SecondParameter = addresses.Count - 1 > PointerPosition + 1 ? addresses[PointerPosition + 2] : 0;
            SecondMode = instructionParameter.Length > 3 ? instructionParameter[instructionParameter.Length - 4] : 0;
            ThirdParameter = addresses.Count - 1 > PointerPosition + 2 ? addresses[PointerPosition + 3] : 0;
            Memory = addresses;
        }
        public Instruction(IList<int> addresses, int pointerInstruction, int input)
            : this(addresses, pointerInstruction)
        {
            IsAutomated = true;
            Input = input;
        }
        public Instruction(IList<int> addresses, int pointerInstruction, int phase, int input, bool phaseInitialized, bool inputUsed)
            : this(addresses, pointerInstruction)
        {
            IsAutomated = true;
            Phase = phase;
            PhaseInitialized = phaseInitialized;
            Input = input;
            InputUsed = inputUsed;
        }

        public bool ProcessInstruction()
        {
            switch (OpCode)
            {
                case OpCode.Addition:
                    AddParameters();
                    break;
                case OpCode.Multiplication:
                    MultiplyParameters();
                    break;
                case OpCode.SaveInput:
                    SaveInput();
                    break;
                case OpCode.Output:
                    OutputFirstParameter();
                    return true;
                case OpCode.JumpIfTrue:
                    JumpIfTrue();
                    break;
                case OpCode.JumpIfFalse:
                    JumpIfFalse();
                    break;
                case OpCode.LessThan:
                    IfLessThan();
                    break;
                case OpCode.Equals:
                    IfEqual();
                    break;
                case OpCode.Halt:
                    KillProgram = true;
                    break;
                default:
                    break;
            }

            return false;
        }

        public int GetPointerPosition()
        {
            return PointerPosition;
        }

        private void AddParameters()
        {
            Memory[ThirdParameter] = getValueAtPosition(1) + getValueAtPosition(2);
            PointerPosition += 4;
        }

        private void MultiplyParameters()
        {
            Memory[ThirdParameter] = getValueAtPosition(1) * getValueAtPosition(2);
            PointerPosition += 4;
        }

        private void SaveInput()
        {
            int input;
            if (IsAutomated)
            {
                if (InputUsed)
                {
                    Paused = true;
                    return;
                }
                if (!PhaseInitialized)
                {
                    PhaseInitialized = true;
                    input = Phase;
                }
                else
                {
                    InputUsed = true;
                    input = Input;
                }
            }
            else
            {
                Console.WriteLine("Enter the ID of the system to test: ");
                input = int.Parse(Console.ReadLine());
            }
            Memory[FirstParameter] = input;
            PointerPosition += 2;
        }

        private void OutputFirstParameter()
        {
            var value = getValueAtPosition(1);
            if (IsAutomated)
            {
                Output = value;
            } else
            {
                Console.WriteLine(value);
            }
            PointerPosition += 2;
        }

        private void StoreOutput()
        {
            var value = getValueAtPosition(1);
            Output = value;
            PointerPosition += 2;
        }

        private void JumpIfTrue()
        {
            if (getValueAtPosition(1) != 0)
            {
                PointerPosition = getValueAtPosition(2);
            }
            else
            {
                PointerPosition += 3;
            }
        }

        private void JumpIfFalse()
        {
            if (getValueAtPosition(1) == 0)
            {
                PointerPosition = getValueAtPosition(2);
            }
            else
            {
                PointerPosition += 3;
            }
        }

        private void IfLessThan()
        {
            if (getValueAtPosition(1) < getValueAtPosition(2))
            {
                Memory[ThirdParameter] = 1;
            } else
            {
                Memory[ThirdParameter] = 0;
            }
            PointerPosition += 4;
        }

        private void IfEqual()
        {
            if (getValueAtPosition(1) == getValueAtPosition(2))
            {
                Memory[ThirdParameter] = 1;
            }
            else
            {
                Memory[ThirdParameter] = 0;
            }
            PointerPosition += 4;
        }

        private int getValueAtPosition(int position)
        {
            if (position == 1)
            {
                return getValueOfInstruction(FirstParameter, FirstMode);
            }
            else if (position == 2)
            {
                return getValueOfInstruction(SecondParameter, SecondMode);
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
