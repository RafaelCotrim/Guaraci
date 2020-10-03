using Guaraci.Core.Optimization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Guaraci.Core.Numeric.Sequences
{
    public interface ISequence
    {


        /// <summary>
        /// Calculates the value of the n-th number in the sequence.
        /// </summary>
        /// <param name="n">Index of the value.</param>
        /// <returns>N-th value of the sequence.</returns>
        public long GetTerm(int n);

        /// <summary>
        /// Returns the first n numbers of the sequence as a collection.
        /// </summary>
        /// <param name="n">Maximun index.</param>
        /// <returns>The first n numbers of the sequence.</returns>
        public IEnumerable<long> GetTerms(int n);

        public IEnumerable<long> GetTermsBellow(long n);
    }
}
