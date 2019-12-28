using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Enums
{
    enum OpCode
    {
        Addition = 1,
        Multiplication = 2,
        SaveInput = 3,
        Output = 4,
        JumpIfTrue = 5,
        JumpIfFalse = 6,
        LessThan = 7,
        Equals = 8,
        Halt = 99
    }
}
