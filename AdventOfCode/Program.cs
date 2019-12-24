using AdventOfCode.Days;
using System;
using System.Linq;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            var intCodeComputer = new Day2();

            var output = intCodeComputer.FindInputsForOutput(19690720);

            Console.WriteLine(output);
        }
    }
}
