using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Guaraci.Core.Numeric.Primes
{
    public class CachedFactorizer: BaseFactorizer, ISieve
    {   
        private SortedSet<long> _memory = new SortedSet<long>();
        private long _fullySeached = 0;

        public readonly ISieve Sieve;
        public readonly IFactorizer Factorizer;

        public CachedFactorizer() :  this(new AtikinSieve(), new TrialFactorizer())
        {

        }

        public CachedFactorizer(ISieve sieve, IFactorizer factorizer)
        {
            Sieve = sieve;
            Factorizer = factorizer;
            _memory.Add(2);
            _memory.Add(3);
            _memory.Add(5);
            _memory.Add(7);
            _fullySeached = 7;
        }

        public IEnumerable<long> Search(long max)
        {
            if (_fullySeached >= max)
                return _memory.GetViewBetween(0, max).AsEnumerable();

            var primes = Sieve.Search(max);
            _memory.UnionWith(primes);
            SetFullySearched(max);
            return primes;
        }
        
        public override IEnumerable<long> Factorize(long n)
        {

            // Trivial cases

            if (n < 0)
                throw new ArgumentOutOfRangeException();

            if (n == 1 || n == 0)
                return new long[] { };

            if (_memory.Contains(n))
                return new long[] { n };

            // Remove factors already calculated

            var factors = new List<long>();
            foreach (var prime in _memory)
            {
                if (n < _fullySeached && prime * prime > n)
                    break;

                while (n % prime == 0)
                {
                    factors.Add(prime);
                    n /= prime;

                    if (n == 1)
                        break;
                }
            }

            if (n == 1)
                return factors.AsEnumerable();
                

            if (_memory.Contains(n))
            {
                factors.Add(n);
                return factors.AsEnumerable();
            }
                

            // Caculate new factors

            var newPrimes = Factorizer.Factorize(n);
            _memory.UnionWith(newPrimes);

            factors.AddRange(newPrimes);
            return factors.AsEnumerable();
        }
        

        private void SetFullySearched(long n)
        {
            _fullySeached = n > _fullySeached ? n : _fullySeached;
        }
    }
}
