using System;
using System.Collections.Generic;
using System.Text;

namespace Guaraci.Core.Numeric.LinearAlgebra
{
    public abstract class Matrix<T>: ICloneable
    {
        public abstract object Clone();
        protected abstract Matrix<T> DoAdd(Matrix<T> first, Matrix<T> second);



        private T[,] storage;

        public T[,] Storage { get => storage; private set => storage = value; }
        public int Rows => Storage.GetLength(0);
        public int Columns => Storage.GetLength(1);


        public Matrix(int rows, int columns){
            Storage = new T[rows, columns];
        }

        public Matrix(int size)
        {
            Storage = new T[size, size];
        }

        public T At(int row, int column) => Storage[row, column];

        public Matrix<T> Add(Matrix<T> other)
        {
            if (Rows != other.Rows || Columns != other.Columns)
                throw new ArgumentException();

            return DoAdd(this, other);
        }

        public static Matrix<T> operator +(Matrix<T> first, Matrix<T> second)
        {
            var m = (Matrix<T>)first.Clone();
            return m.Add(second);
        } 

    }
}
