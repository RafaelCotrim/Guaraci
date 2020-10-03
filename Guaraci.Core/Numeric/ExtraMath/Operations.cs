using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Guaraci.Core.Numeric
{
    public static partial class ExtraMath
    {
        public static long GCD(long a, long b)
        {
            if (b > a)
            {
                var aux = b;
                b = a;
                a = aux;
            }

            while (true)
            {
                var remainder = a % b;
                if (remainder == 0) return b;
                a = b;
                b = remainder;
            };
        }
        public static long GCD(IEnumerable<long> numbers)
        {
            //long result = 1;
            //foreach (var n in numbers.ToString())
            //{
            //    result = GCD(result, n);
            //}
            //return result;
            return numbers.Aggregate((previous, next) => GCD(previous, next));

        }
        public static long LCM(long a, long b)
        {
            return (a * b) / GCD(a, b);
        }
        public static long LCM(IEnumerable<long> numbers)
        {
            return numbers.Aggregate((previous, next) => LCM(previous, next));

        }

        public static long Factorial(long n, long denominator = 1)
        {
            if (n < 0 || denominator < 0)
                throw new ArgumentOutOfRangeException();

            long result = 1;
            checked
            {
                for (long i = n; i > denominator; i--)
                {
                    result *= i;
                }
            }
            return result;
        }
        public static BigInteger BigFactorial(long n, long denominator = 1)
        {
            if (n < 0 || denominator < 0)
                throw new ArgumentOutOfRangeException();

            var result = BigInteger.One;
            for (long i = n; i > denominator; i--)
            {
                result *= i;
            }
            return result;
        }
    }
}
