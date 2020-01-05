using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Models
{
    struct Permutation : IEnumerable<int>
    {
        public static Permutation Empty { get { return empty; } }
        private static Permutation empty = new Permutation(new int[] { });
        private int[] permutation;
        private Permutation(int[] permutation)
        {
            this.permutation = permutation;
        }
        private Permutation(IEnumerable<int> permutation) : this(permutation.ToArray()) {}

        public static IEnumerable<Permutation> HamiltonianPermutations(int n)
        {
            if (n < 0)
            {
                throw new ArgumentOutOfRangeException("n");
            }

            return HamiltonianPermutationsIterator(n);
        }

        private static IEnumerable<Permutation> HamiltonianPermutationsIterator(int n)
        {
            if (n == 0)
            {
                yield return Empty;
                yield break;
            }
            var forwards = false;
            foreach (var permutation in HamiltonianPermutationsIterator(n - 1))
            {
                for (int i = 0; i < n; i += 1)
                {
                    var position = forwards ? i : n - i - 1;
                    yield return new Permutation(permutation.InsertAt(position, n -1));
                }
                forwards = !forwards;
            }
        }

        public int this[int index]
        {
            get { return permutation[index]; }
        }

        public IEnumerator<int> GetEnumerator()
        {
            foreach (int item in permutation)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public int Count { get { return this.permutation.Length; } }

        public override string ToString()
        {
            return string.Join<int>(",", permutation);
        }
    }
}
