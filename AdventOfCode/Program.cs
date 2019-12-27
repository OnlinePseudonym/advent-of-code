﻿using AdventOfCode.Days;
using System;
using System.Linq;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            var lineCalculator = new Day3();

            var fewestSteps = lineCalculator.GetFewestStepsToIntersection();

            Console.WriteLine(fewestSteps);
        }
    }
}
