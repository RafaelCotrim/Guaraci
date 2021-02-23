using Guaraci.Core.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guaraci.Core.Numeric.LinearAlgebra
{
    public abstract class Vector<T> : IEnumerable<T>, ICloneable<Vector<T>>, IEquatable<Vector<T>> where T : struct
    {
        private readonly T[] _storage;

        public int Length => _storage.Length;
        public T this[int i]
        {
            get => _storage[i];
            set => _storage[i] = value;
        }

        protected Vector(int length)
        {
            _storage = new T[length];
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Length; i++)
            {
                yield return this[i];
            }
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public abstract Vector<T> Clone();
        object ICloneable.Clone() => Clone();
        public override string ToString()
        {
            var str = "[";
            foreach (var n in this)
            {
                str = $"{str} {n}";
            }
            return str += " ]";
        }
        public bool Equals(Vector<T>? other)
        {
            if (other is null)
                return false;

            if (Length != other.Length)
                return false;

            for (int i = 0; i < Length; i++)
            {
                if (!this[i]!.Equals(other[i]))
                    return false;
            }
            return true;
        }
        
        public void CopyTo(Vector<T> vector)
        {
            if (vector.Length != Length)
                throw new ArgumentException();

            for (int i = 0; i < Length; i++)
            {
                vector[i] = this[i];
            }
        }
        public void Apply(Func<T, T> func)
        {
            for (int i = 0; i < Length; i++)
            {
                this[i] = func(this[i]);
            }
        }
        public void Apply(Func<int, T, T> func)
        {
            for (int i = 0; i < Length; i++)
            {
                this[i] = func(i, this[i]);
            }
        }

        #region Public Operaterations
        public void Negate() => DoNegate();
        public void Add(Vector<T> other)
        {
            if (other.Length != Length)
                throw new ArgumentException();
            DoAdd(other);
        }
        public void Subtract(Vector<T> other)
        {
            if (other.Length != Length)
                throw new ArgumentException();
            DoAdd(-other);
        }
        public void Multiply(T value) => DoMultiply(value);
        public void Divide(T value) => DoDivide(value);
        #endregion

        #region Suport operation methods
        protected abstract void DoNegate();
        protected abstract void DoAdd(Vector<T> other);
        protected abstract void DoMultiply(T value);
        protected abstract void DoDivide(T value);
        #endregion

        #region Operators
        public static Vector<T> operator -(Vector<T> vector)
        {
            var v = vector.Clone();
            v.Negate();
            return v;
        }
        public static Vector<T> operator +(Vector<T> first, Vector<T> second)
        {
            var v = first.Clone();
            v.Add(second);
            return v;
        }
        public static Vector<T> operator -(Vector<T> first, Vector<T> second)
        {
            return first + (-second);
        }
        public static Vector<T> operator *(Vector<T> vector, T value)
        {
            var v =vector.Clone();
            v.Multiply(value);
            return v;
        }
        public static Vector<T> operator *(T value, Vector<T> vector) => vector * value;
        public static Vector<T> operator /(Vector<T> vector, T value)
        {
            var v = vector.Clone();
            v.Divide(value);
            return v;
        }
        #endregion
    }
}
