using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Guaraci.Core.Numeric.Sequences
{
    /// <summary>
    /// Represents a sequence defined by f(x) = A + x * D
    /// where A and D are integer constants.
    /// </summary>
    public class ArithmeticSequence : ISequence
    {
        public static ArithmeticSequence MultipleSequence(int n) => new ArithmeticSequence(0, n);

        public readonly long A;
        public readonly long D;

        public ArithmeticSequence(long a, long d)
        {
            A = a;
            D = d;
        }

        public IEnumerable<long> GetTerms(int n)
        {
            var val = new long[n];
            for (int i = 0; i < n; i++)
            {
                val[i] = GetTerm(i);
            }
            return val.AsEnumerable();
        }
        public IEnumerable<long> GetTermsBellow(long n)
        {
            var vals = new List<long>();
            long t = A;
            var i = 1;
            while(t < n)
            {
                vals.Add(t);
                t = GetTerm(i);
                i++;
            }
            return vals.AsEnumerable();
        }
        public long GetTerm(int n)
        {
            return A + n * D;
        }
        public long Sum(int end)
        {
            return Sum(0, end);
        }
        public long Sum(int start, int end)
        {
            var n = end - start + 1;
            return ((GetTerm(start) + GetTerm(end)) * n)/2;
        }

        

        
    }
}
