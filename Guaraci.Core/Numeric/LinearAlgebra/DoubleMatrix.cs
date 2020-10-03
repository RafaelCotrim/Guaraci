using System;
using System.Collections.Generic;
using System.Text;

namespace Guaraci.Core.Numeric.LinearAlgebra
{
    public class DoubleMatrix : Matrix<double>
    {
        public DoubleMatrix(int size) : base(size)
        {
        }

        public DoubleMatrix(int rows, int columns) : base(rows, columns)
        {
        }

        public override object Clone()
        {

            return new DoubleMatrix(Rows, Columns);
        }

        protected override Matrix<double> DoAdd(Matrix<double> first, Matrix<double> second)
        {
            throw new NotImplementedException();
        }
    }
}
