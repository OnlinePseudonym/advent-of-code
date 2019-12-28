using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Models
{
    static class Utilities
    {
        public static int[] digitArr(int n)
        {
            var result = new int[6];
            for (int i = result.Length - 1; i >= 0; i--)
            {
                result[i] = n % 10;
                n /= 10;
            }
            return result;
        }
    }
}
