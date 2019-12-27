using AdventOfCode.Days;
using System;
using System.Linq;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            var bruteForcePWGenerator = new Day4();

            var possibleCombinations = bruteForcePWGenerator.GetCountOfValidPasswords();

            Console.WriteLine(possibleCombinations);
        }
    }
}
