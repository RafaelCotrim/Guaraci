using System;
using System.Collections.Generic;
using System.Text;

namespace Guaraci.Core.Numeric.LinearAlgebra.Double
{
    public class Matrix : Matrix<double>
    {
        public Matrix(int size) : base(size) { }
        public Matrix(int rows, int columns) : base(rows, columns) { }
        

        public override Matrix Clone()
        {
            var m = new Matrix(RowCount, ColumnCount);
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    m[i, j] = this[i, j];
                }
            }
            return m;
        }
        public override Vector Row(int i) => (Vector)base.Row(i);
        public override Vector Column(int j) => (Vector)base.Column(j);
        public override Vector VectorOfSameType(int legth) => new Vector(legth);

        protected override void DoNegate()
        {
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    this[i, j] *= -1;
                }
            }
        }
        protected override void DoAdd(Matrix<double> matrix)
        {
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    this[i, j] += matrix[i, j];
                }
            }
        }
        protected override void DoMultiply(double value)
        {
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    this[i, j] *= value;
                }
            }
        }
        protected override void DoMultiply(Matrix<double> other)
        {
            double buffer = 0;
            var temp = new Matrix(RowCount, ColumnCount);

            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < other.ColumnCount; j++)
                {
                    buffer = 0;
                    for (int k = 0; k < ColumnCount; k++)
                    {
                        buffer += this[i, k] * other[k, j];
                    }
                    temp[i, j] = buffer;
                }
            }
            temp.CopyTo(this);
        }
        protected override void DoDivide(double value)
        {
            DoMultiply(1 / value);
        }
    
        public static Matrix Random(int row, int column)
        {
            var m = new Matrix(row, column);
            var rnd = new Random();
            for (int i = 0; i < m.RowCount; i++)
            {
                for (int j = 0; j < m.ColumnCount; j++)
                {
                    m[i, j] = rnd.NextDouble();
                }
            }
            return m;
        }
        public static Matrix Random(int size) => Random(size, size);
        

        
    }
}
