﻿using System;
using System.Collections;
using System.Diagnostics;


namespace Axle.Verification
{
    /// <summary>
    /// Extension methods to the <see cref="ArgumentReference{T}"/> class that enable verification for arguments 
    /// of type <see cref="IEnumerable" />.
    /// </summary>
    /// <seealso cref="IEnumerable"/>
    public static class EnumerableVerifier
    {
        [DebuggerStepThrough]
        public static ArgumentReference<T> IsNotNullOrEmpty<T>(this ArgumentReference<T> argument, string message) where T: IEnumerable
        {
            var e = argument.IsNotNull().Value.GetEnumerator();
            try
            {
                if (!e.MoveNext())
                {
                    throw new ArgumentException(message ?? string.Format("Argument `{0}` cannot be an empty collection.", argument.Name), argument.Name);
                }
                return argument;
            }
            finally
            {
                if (e is IDisposable d)
                {
                    d.Dispose();
                }
            }
        }

        [DebuggerStepThrough]
        public static ArgumentReference<T> IsNotNullOrEmpty<T>(this ArgumentReference<T> argument) where T: IEnumerable
        {
            return IsNotNullOrEmpty(argument, null);
        }
    }
}