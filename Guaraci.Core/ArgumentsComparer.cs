using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Guaraci.Core.Optimization
{
    class ArgumentsComparer : IEqualityComparer<object[]>
    {
        public bool Equals([AllowNull] object[] x, [AllowNull] object[] y)
        {
            if (ReferenceEquals(x, y))
                return true;

            if (x!.Length != y!.Length)
                return false;

            for (int i = 0; i < x.Length; i++)
            {
                if (!x[i].Equals(y[i]))
                    return false;
            }

            return true;

        }

        public int GetHashCode([DisallowNull]object[] obj)
        {
            var hash = new HashCode();
            foreach (var o in obj)
            {
                hash.Add(o);
            }
            return hash.ToHashCode();
        }
    }
}
