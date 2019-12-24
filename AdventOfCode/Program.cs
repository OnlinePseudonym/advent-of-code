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
            var proccessedInputs = intCodeComputer.CalculateOutput(intCodeComputer.Memory);

            Console.WriteLine(proccessedInputs.First());
        }
    }
}
