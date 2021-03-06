﻿#if NETSTANDARD || NET20_OR_NEWER
using System;
#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System.Runtime.Serialization;
#endif

using Axle.Verification;


namespace Axle.Conversion
{
    /// <summary>
    /// An exception thrown whenever a conversion form object of one given type fails to convert to an instance of another type.
    /// </summary>
    /// <seealso cref="IConverter{TSource,TTarget}"/>
    /// <seealso cref="ITwoWayConverter{TSource,TTarget}"/>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public class ConversionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConversionException"/> class.
        /// </summary>
        public ConversionException() {}
        /// <summary>
        /// Initializes a new instance of the <see cref="ConversionException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">
        /// The message that describes the error.
        /// </param>
        public ConversionException(string message) : base(message) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="ConversionException"/> class with a specified error message
        /// and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">
        /// The message that describes the error.
        /// </param>
        /// <param name="inner">
        /// The exception that is the cause of the current exception, or a <c>null</c> reference (<c>Nothing</c> in Visual Basic)
        /// if no inner exception is specified.
        /// </param>
        public ConversionException(string message, Exception inner) : base(message, inner) {}

        /// <summary>
        /// Initializes a new instance of <see cref="ConversionException"/> to represent the failure of converting the given types.
        /// </summary>
        /// <param name="sourceType">
        /// The type of the source object that failed to convert.
        /// </param>
        /// <param name="destinationType">
        /// The destination type of the failed conversion.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Either the <paramref name="sourceType"/> or <paramref name="destinationType"/> is <c>null</c>.
        /// </exception>
        public ConversionException(Type sourceType, Type destinationType) : this(
            string.Format(
                "Cannot convert an instance of {0} to {1}.",
                Verifier.IsNotNull(Verifier.VerifyArgument(sourceType, nameof(sourceType))).Value.FullName,
                Verifier.IsNotNull(Verifier.VerifyArgument(destinationType, nameof(destinationType))).Value.FullName),
            null) {}
        /// <summary>
        /// Initializes a new instance of <see cref="ConversionException"/> to represent the failure of converting the given types,
        /// and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="sourceType">
        /// The type of the source object that failed to convert.
        /// </param>
        /// <param name="destinationType">
        /// The destination type of the failed conversion.
        /// </param>
        /// <param name="inner">
        /// The exception that is the cause of the current exception, or a <c>null</c> reference (<c>Nothing</c> in Visual Basic)
        /// if no inner exception is specified.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Either the <paramref name="sourceType"/> or <paramref name="destinationType"/> is <c>null</c>.
        /// </exception>
        public ConversionException(Type sourceType, Type destinationType, Exception inner) : this(
            string.Format(
                "Cannot convert an instance of {0} to {1}.",
                Verifier.IsNotNull(Verifier.VerifyArgument(sourceType, nameof(sourceType))).Value.FullName,
                Verifier.IsNotNull(Verifier.VerifyArgument(destinationType, nameof(destinationType))).Value.FullName),
            inner) {}

        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        /// <summary>
        /// Initializes a new instance of the <see cref="ConversionException"/> class with serialized data.
        /// </summary>
        /// <param name="info">
        /// The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The <see cref="StreamingContext"/> that contains contextual information about the source or destination.
        /// </param>
        protected ConversionException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        #endif
    }
}
#endif