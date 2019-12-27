﻿using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Models;
using System.Text;

namespace AdventOfCode.Days
{
    static class Day2
    {
        public static void LogAnswerToConsole()
        {
            var intcodeComputer = new IntcodeComputer();

            Console.WriteLine(intcodeComputer.FindInputsForOutput(19690720));
        }
    }
}
