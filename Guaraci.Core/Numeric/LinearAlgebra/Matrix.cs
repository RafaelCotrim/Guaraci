using Guaraci.Core.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;

namespace Guaraci.Core.Numeric.LinearAlgebra
{
    public abstract class Matrix<T> : ICloneable<Matrix<T>>, IEnumerable<T> where T : struct
    {
        private T[,] storage;

        #region Acessors
        protected T[,] Storage { get => storage; private set => storage = value; }
        public int RowCount => Storage.GetLength(0);
        public int ColumnCount => Storage.GetLength(1);
        public T this[int row, int column]
        {
            get => At(row, column);
            set => Set(row, column, value);
        }
        #endregion

        #region Constructors
        public Matrix(int rows, int columns)
        {
            storage = new T[rows, columns];
        }
        public Matrix(int size) : this(size, size) { }
        #endregion

        public abstract Matrix<T> Clone();
        object ICloneable.Clone() => Clone();
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    yield return this[i, j];
                }
            }
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();


        public override string ToString()
        {
            var str = "";
            foreach (var r in Rows())
            {
                str = $"{str}{r}\n";
            }
            return str.Trim();
        }
        public abstract Vector<T> VectorOfSameType(int length);
        public T At(int row, int column) => Storage[row, column];
        public void Set(int row, int column, T value) => Storage[row, column] = value;
        public bool HasSameDimensionsAs(Matrix<T> other)
        {
            return RowCount == other.RowCount && ColumnCount == other.ColumnCount;
        }
        public void CopyTo(Matrix<T> other)
        {
            if (!HasSameDimensionsAs(other))
                throw new ArgumentException();

            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    other[i, j] = this[i, j];
                }
            }
        }
        public void Apply(Func<T, T> func)
        {
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    Storage[i, j] = func(Storage[i, j]);
                }
            }
        }
        public virtual Vector<T> Row(int i)
        {
            if (i < 0  || i > RowCount)
                throw new ArgumentOutOfRangeException();

            var v = VectorOfSameType(ColumnCount);
            for (int j = 0; j < ColumnCount; j++)
            {
                v[j] = this[i, j];
            }
            return v;
        }
        public virtual Vector<T> Column(int j)
        {
            if (j < 0 || j > RowCount)
                throw new ArgumentOutOfRangeException();

            var v = VectorOfSameType(RowCount);
            for (int i = 0; i < RowCount; i++)
            {
                v[i] = this[i, j];
            }
            return v;
        }


        public IEnumerable<T> Positions()
        {
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    yield return this[i, j];
                }
            }
        }
        public IEnumerable<Vector<T>> Rows()
        {
            for (int i = 0; i < RowCount; i++)
            {
                yield return Row(i);
            }
        }
        public IEnumerable<Vector<T>> Columns()
        {
            for (int i = 0; i < ColumnCount; i++)
            {
                yield return Column(i);
            }
        }


        #region Public Operators
        public void Negate() => DoNegate();
        public void Transpose()
        {
            var old = Storage;
            Storage = new T[ColumnCount, RowCount];
            for (int i = 0; i < old.GetLength(0); i++)
            {
                for (int j = 0; j < old.GetLength(1); j++)
                {
                    Storage[j, i] = old[i, j];
                }
            }
        }
        public void Add(Matrix<T> other)
        {
            if (!HasSameDimensionsAs(other))
                throw new ArgumentException();
            DoAdd(other);
        }
        public void Subtract(Matrix<T> other)
        {
            if (!HasSameDimensionsAs(other))
                throw new ArgumentException();
            DoAdd(-other);
        }
        public void Multiply(T value)
        {
            DoMultiply(value);
        }
        public void Multiply(Matrix<T> other)
        {
            if (ColumnCount != other.RowCount)
                throw new ArgumentException();

            DoMultiply(other);
        }
        public void Divide(T value)
        {
            DoDivide(value);
        }
        
        #endregion

        #region Suport operation methods
        protected abstract void DoNegate();
        protected abstract void DoAdd(Matrix<T> other);
        protected abstract void DoMultiply(T value);
        protected abstract void DoMultiply(Matrix<T> other);
        protected abstract void DoDivide(T value);
        #endregion


        #region Operators
        public static Matrix<T> operator -(Matrix<T> matrix)
        {
            var m = (Matrix<T>)matrix.Clone();
            m.Negate();
            return m;
        }
        public static Matrix<T> operator +(Matrix<T> first, Matrix<T> second)
        {
            var m = (Matrix<T>)first.Clone();
            m.Add(second);
            return m;
        }
        public static Matrix<T> operator -(Matrix<T> first, Matrix<T> second)
        {
            return first + (-second);
        }
        public static Matrix<T> operator *(Matrix<T> matrix, T value)
        {
            var m = (Matrix<T>)matrix.Clone();
            m.Multiply(value);
            return m;
        }
        public static Matrix<T> operator *(T value, Matrix<T> matrix) => matrix * value;
        public static Matrix<T> operator *(Matrix<T> first, Matrix<T> second)
        {
            var m = (Matrix<T>)first.Clone();
            m.Multiply(second);
            return m;
        }
        public static Matrix<T> operator /(Matrix<T> matrix, T value)
        {
            var m = (Matrix<T>)matrix.Clone();
            m.Divide(value);
            return m;
        }
        #endregion
    }
}
