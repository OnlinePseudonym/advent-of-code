using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Days
{
    class Day4
    {
        private readonly IEnumerable<int> RANGE = Enumerable.Range(264793, 803935 - 264793);

        public int GetCountOfValidPasswords()
        {
            var output = 0;
            foreach (var number in RANGE)
            {
                var password = digitArr(number);

                if (isValidPassword(password))
                {
                    output++;
                }
            }

            return output;
        }

        private static bool isValidPassword(int[] password)
        {
            var output = false;

            for (var i = 1; i < password.Length; i++)
            {
                if (password[i - 1] > password[i]) return false;
                if (hasExactlyTwoEqualAdjacentDigits(password, i)) output = true;
                        
            }

            return output;
        }

        private static bool hasExactlyTwoEqualAdjacentDigits(int[] password, int index)
        {
            var output = false;

            if (
                    password[index - 1] == password[index] &&
                    (index - 2 < 0 || password[index - 2] != password[index]) &&
                    (index + 1 >= password.Length || password[index + 1] != password[index])
                )
            {
                output = true;
            }

            return output;
        }

        private static int[] digitArr(int n)
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
