using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guaraci.Core.Numeric.LinearAlgebra.Double
{
    public class Vector : Vector<double>
    {
        public Vector(int length) : base(length)
        {
        }

        public override Vector Clone()
        {
            var v = new Vector(Length);
            CopyTo(v);
            return v;
        }
        public void Round() => Apply(x => Math.Round(x));

        protected override void DoAdd(Vector<double> other)
        {
            for (int i = 0; i < Length; i++)
            {
                this[i] += other[i];
            }
        }
        protected override void DoMultiply(double value)
        {
            for (int i = 0; i < Length; i++)
            {
                this[i] *= value;
            }
        }
        protected override void DoNegate() => DoMultiply(-1);
        protected override void DoDivide(double value) => DoMultiply(1 / value);
    
        public static Vector Random(int length)
        {
            var v = new Vector(length);
            var rand = new Random();
            for (int i = 0; i < v.Length; i++)
            {
                v[i] = rand.NextDouble();
            }
            return v;
        }
        public static Vector RandomInt(int length, int max)
        {
            var v = Random(length);
            v.Multiply(max);
            v.Round();
            return v;
        }
    }
}
