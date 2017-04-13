﻿using System;
using System.Collections.Generic;


namespace Axle
{
    /// <summary>
    /// An abstract class to serve as a base to implementing the <see cref="IEqualityComparer{T}" /> interface.
    /// </summary>
    /// <typeparam name="T">
    /// The type of objects to compare. This type parameter is contravariant. That is, you can use either the type 
    /// you specified or any type that is less derived. For more information about covariance and contravariance, 
    /// see "Covariance and Contravariance in Generics".
    /// </typeparam>
    /// <seealso cref="IEqualityComparer{T}" />
    [Serializable]
    public abstract class AbstractEqualityComparer<T> : IEqualityComparer<T>
    {
        #if !DEBUG
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        #endif
        private static readonly bool _isValueType = typeof (T).IsValueType;

        /// <summary>
        /// Determines whether the specified object instances are considered equal. 
        /// </summary>
        /// <param name="x">
        /// The first object to compare. 
        /// </param>
        /// <param name="y">
        /// The second object to compare. 
        /// </param>
        new public bool Equals(object x, object y)
        {
            return ReferenceEquals(x, null)
                ? ReferenceEquals(y, null)
                : ReferenceEquals(y, null)
                    ? false 
                    : ReferenceEquals(x, y) || (
                        x is T 
                            ? (y is T ? this.DoEquals((T) x, (T) y) : y.Equals(x))
                            : (y is T ? x.Equals(y) : object.Equals(x, y)));
        }
        /// <summary>
        /// Determines whether the specified object instances are considered equal. 
        /// </summary>
        /// <param name="x">
        /// The first object to compare. 
        /// </param>
        /// <param name="y">
        /// The second object to compare. 
        /// </param>
        public bool Equals(T x, T y)
        {
            return _isValueType
                ? this.DoEquals(x, y)
                : !ReferenceEquals(x, null)
                    ? ReferenceEquals(y, null)
                        ? false
                        : ReferenceEquals(x, y) || this.DoEquals(x, y)
                    : ReferenceEquals(y, null);
        }

        /// <summary>
        /// Returns a hash code for the specified object.
        /// </summary>
        /// <returns>
        /// A hash code for the specified object.
        /// </returns>
        /// <param name="obj">
        /// The instance of <typeparamref name="T"/> for which a hash code is to be returned.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The type of <paramref name="obj"/> is a reference type and <paramref name="obj"/> is null.
        /// </exception>
        public int GetHashCode(T obj)
        {
            if (ReferenceEquals(obj, null))
            {
                throw new ArgumentNullException("obj");
            }

            return unchecked(this.DoGetHashCode(obj));
        }

        /// <summary>
        /// Returns a hash code for the specified object. 
        /// </summary>
        /// <param name="obj">
        /// The instance of <see cref="T"/> for which a hash code is to be returned.
        /// </param>
        /// <param name="appendTypeHashCode">
        /// Indicates if the hashcode of the underlying type will be automatically
        /// appended to the hashcode generated by the <see cref="GetHashCode(T)" /> method.
        /// </param>
        /// <returns>
        /// A hash code for the specified object.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// The type of <paramref name="obj"/> is a reference type and <paramref name="obj"/> is <c>null</c>.
        /// </exception>
        public int GetHashCode(T obj, bool appendTypeHashCode)
        {
            if (ReferenceEquals(obj, null))
            {
                throw new ArgumentNullException("obj");
            }

            return appendTypeHashCode 
                ? unchecked(this.DoGetHashCode(obj)^GetType().GetHashCode()) 
				: unchecked(this.DoGetHashCode(obj));
        }

        public override bool Equals(object obj) { return base.Equals(obj); }

        public override int GetHashCode() { return base.GetHashCode(); }

        /// <summary>
        /// When overriden in a derived class, determines if the two <typeparamref name="T" /> instaneces are equal.
        /// </summary>
        /// <param name="x">
        /// The first object to compare. 
        /// </param>
        /// <param name="y">
        /// The second object to compare.
        /// </param>
        protected abstract bool DoEquals(T x, T y);

        /// <summary>
        /// When overriden in a derived class, this method produces the hash code of the supplied object. 
        /// </summary>
        /// <param name="obj">
        /// The object whose hash code should be calculated. 
        /// </param>
        /// <returns>
        /// The hash code of the passed by the <paramref name="obj"/> object. 
        /// </returns>
        protected abstract int DoGetHashCode(T obj);
    }
}
