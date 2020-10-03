using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Guaraci.Core.Numeric.Primes
{
    public abstract class BaseFactorizer : IFactorizer
    {
        public abstract IEnumerable<long> Factorize(long n);
        public IEnumerable<(long prime, int power)> PrimePowers(long n)
        {
            return Factorize(n).GroupBy(f => f).Select(f => (f.Key, f.Count())).OrderBy(f => f.Key);
        }
        public bool IsPrime(long n)
        {
            return Factorize(n).Count() == 1;
        }
        public long DivisorCount(long n)
        {
            var powers = PrimePowers(n);
            var p = powers.Select(x => x.power).Sum();
            return (long)Math.Pow(2, p);
        }
        public long DivisorSum(long n)
        {
            var powers = PrimePowers(n);
            long result = 1;
            foreach (var p in powers)
            {
                long sum = 0;
                for (int i = 0; i <= p.power; i++)
                {
                    sum += (long)Math.Pow(p.prime, i);
                }
                result *= sum;
            }
            return result - n;
        }
    }
}
