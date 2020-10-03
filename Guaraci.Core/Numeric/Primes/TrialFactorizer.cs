using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Guaraci.Core.Numeric.Primes
{
    public class TrialFactorizer : BaseFactorizer
    {
        public override IEnumerable<long> Factorize(long n)
        {
            if (n < 0)
                throw new ArgumentOutOfRangeException();

            if (n == 1 || n == 0)
                return new long[] { }.AsEnumerable();

            var factors = new List<long>();

            while (n % 2 == 0)
            {
                n /= 2;
                factors.Add(2);
            }

            for (var i = 3; i * i <= n; i += 2)
            {
                while (n % i == 0)
                {
                    n /= i;
                    factors.Add(i);

                    if (n == 1)
                        return factors.AsEnumerable();
                }
            }

            if(n != 1)
                factors.Add(n);

            return factors.AsEnumerable();

        }
    }
}
