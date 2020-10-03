using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Guaraci.Core.Numeric.Primes
{
    public class AtikinSieve: ISieve
    {
        public IEnumerable<long> Search(long max)
        {
            var found = new List<long>();
            found.Add(2);
            found.Add(3);

            if (max < 5)
                return found.Where(x => x < max);


            var sieve = new bool[max+1];

            for (var i = 0; i * i < max; i++)
            {
                for (var j = 0; j * j < max; j++)
                {

                    var n = 4 * (i * i) + (j * j);

                    if (n <= max && (n % 12 == 1 || n % 12 == 5))
                        sieve[n] ^= true;

                    n = 3 * (i * i) + (j * j);
                    if (n <= max && n % 12 == 7)
                        sieve[n] ^= true;

                    n = 3 * (i * i) - (j * j);
                    if (n <= max && i > j && n % 12 == 11)
                        sieve[n] ^= true;
                }
            }

            for (var i = 5; i * i < max; i++)
            {
                if (sieve[i])
                {
                    var k = i * i;
                    for (int j = k; j < max; j += k)
                        sieve[j] = false;
                }
            }

            for (var i = 5; i < max; i++)
            {
                if (sieve[i])
                {
                    found.Add(i);
                }
            }

            return found.AsEnumerable();
        }
    }
}
