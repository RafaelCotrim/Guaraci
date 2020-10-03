using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Guaraci.Core.Numeric.Sequences
{
    /// <summary>
    /// Represents a sequence defined by f(x) = P * f(x-1) - Q * f(x-2)
    /// where P and Q are integer constants.
    /// </summary>
    public class LucasSequence: ISequence
    {
        public static LucasSequence Fibonacci => new LucasSequence(1, -1, 0, 1);
        public static LucasSequence Lucas => new LucasSequence(1, -1, 2, 1);

        private readonly List<long> _memory = new List<long>();

        public readonly long P;
        public readonly long Q;
        public LucasSequence(long p, long q, long seed1, long seed2)
        {
            P = p;
            Q = q;
            _memory.Add(seed1);
            _memory.Add(seed2);
        }

        public long GetTerm(int n)
        {
            if (_memory.Count > n)
                return _memory[n];

            checked
            {
                var val = P * GetTerm(n - 1) - (Q * GetTerm(n - 2));
                _memory.Add(val);
                return val;
            }
        }
        
        public IEnumerable<long> GetTerms(int n)
        {
            if (_memory.Count < n)
                GetTerm(n);

            return _memory.Take(n);
        }

        public IEnumerable<long> GetTermsBellow(long n)
        {
            if (_memory.Last() > n)
                return _memory.Where(x => x < n);

            long v = 0;
            var index = 0;
            do
            {
                index = _memory.Count;
                v = GetTerm(index);
            } while (v < n);
            return _memory.Take(index);
        }

    }
}
