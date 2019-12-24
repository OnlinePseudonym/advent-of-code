using AdventOfCode.Days;
using System;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            var totalFuel = 0;

            var day1 = new Day1();

            foreach (var mass in day1.input) {
                totalFuel += day1.CalculatFuelForMass(mass);
            }

            Console.WriteLine(totalFuel);
        }
    }
}
