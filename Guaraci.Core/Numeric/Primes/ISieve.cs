using System.Collections.Generic;

namespace Guaraci.Core.Numeric.Primes
{
    public interface ISieve
    {
        IEnumerable<long> Search(long max);
    }
}