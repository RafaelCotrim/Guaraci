using System;
using System.Collections.Generic;
using System.Text;

namespace Guaraci.Core.Numeric.Sequences
{
    public static class SequenceUtilities
    {
        public static long Triangle(long n)
        {
            return n * (n + 1) / 2;
        }

        public static long Collatz(long n)
        {
            if (n < 0)
                throw new ArgumentOutOfRangeException();

            if (n == 0 || n == 1)
                return n;

            if (n % 2 == 0)
                return n / 2;

            return 3 * n + 1;
        }
    }
}
