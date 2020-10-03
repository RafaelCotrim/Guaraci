using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Guaraci.Core.Numeric
{
    public static partial class ExtraMath
    {
        /// <summary>
        /// Calculates the number of permuatiions of a set of n elements where r are chosen each time
        /// </summary>
        /// <param name="n">Total number of elements</param>
        /// <param name="k">Legth of the permutation</param>
        /// <returns></returns>
        public static long Permutation(long n, long k)
        {
            if (n < 0 || k < 0)
                throw new ArgumentException();

            if (n < k)
                throw new ArgumentException("The number of elements must be equal or greater than the legth of the permutation");

            return Factorial(n, k);
        }
        public static BigInteger BigPermutation(long n, long k)
        {
            if (n < 0 || k < 0)
                throw new ArgumentException();

            if (n < k)
                throw new ArgumentException("The number of elements must be equal or greater than the legth of the permutation");

            return BigFactorial(n, k);
        }

        public static long Combination(long n, long k)
        {
            if (n < 0 || k < 0)
                throw new ArgumentException();

            if (n < k)
                throw new ArgumentException("The number of elements must be equal or greater than the legth of the permutation");

            return Factorial(n, k) / Factorial(k);
        }
        public static BigInteger BigCombination(long n, long k)
        {
            if (n < 0 || k < 0)
                throw new ArgumentException();

            if (n < k)
                throw new ArgumentException("The number of elements must be equal or greater than the legth of the permutation");

            return BigFactorial(n, k) / BigFactorial(k);
        }

    }
}
