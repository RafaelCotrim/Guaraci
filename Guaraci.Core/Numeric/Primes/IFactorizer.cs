using System.Collections.Generic;

namespace Guaraci.Core.Numeric.Primes
{
    public interface IFactorizer<T>
    {
        bool IsPrime(T n);
        public IEnumerable<T> Factorize(T n);
        IEnumerable<(T prime, int power)> PrimePowers(T n);
    }

    public interface IFactorizer : IFactorizer<long>
    {
        
    }
}