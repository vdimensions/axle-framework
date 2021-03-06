using System;


namespace Axle.Conversion.Parsing
{
    /// <summary>
    /// An abstract class to aid the implementation of a custom parser.
    /// Supports optional input validation prior parsing.
    /// </summary>
    /// <typeparam name="T">
    /// The result type of the parsing.
    /// </typeparam>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public abstract class AbstractParser<T> : IParser<T>
    {
        object IParser.Parse(string value, IFormatProvider formatProvider) => Parse(value, formatProvider);
        object IParser.Parse(string value) => Parse(value);

        bool IParser.TryParse(string value, IFormatProvider formatProvider, out object result)
        {
            if (TryParse(value, formatProvider, out T genericResult))
            {
                result = genericResult;
                return true;
            }
            result = null;
            return false;
        }
        bool IParser.TryParse(string value, out object result)
        {
            if (TryParse(value, out var genericResult))
            {
                result = genericResult;
                return true;
            }
            result = null;
            return false;
        }

        /// <summary>
        /// Parses a <see cref="string"/> to the specified type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="value">
        /// The string value to be parsed.
        /// </param>
        /// <returns>
        /// An instance of <typeparamref name="T" />.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the string cannot be recognized
        /// as a valid provider for and instance of <typeparamref name="T"/>.
        /// </exception>
        public T Parse(string value) => Parse(value, null);

        /// <inheritdoc />
        public T Parse(string value, IFormatProvider formatProvider)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (!Validate(value, formatProvider))
            {
                throw new ParseException(value, typeof(T));
            }

            try
            {
                return DoParse(value, formatProvider);
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ParseException(value, typeof(T), ex);
            }
        }

        /// <summary>
        /// Converts the specified string representation of a logical value to its <typeparamref name="T"/> equivalent. 
        /// A return value indicates whether the conversion succeeded or failed.
        /// </summary>
        /// <param name="value">
        /// A string containing the value to convert.
        /// </param>
        /// <param name="output">
        /// When this method returns, contains the <typeparamref name="T"/> value equivalent to 
        /// the string passed in <paramref name="value" />, if the conversion succeeded, or the default
        /// value for <typeparamref name="T"/> if the conversion failed. 
        /// The conversion fails if the <paramref name="value"/> parameter is null or is not of the correct format.
        /// This parameter is passed uninitialized.
        /// </param>
        /// <returns>
        /// true if value was converted successfully; otherwise, false.
        /// </returns>
        public virtual bool TryParse(string value, out T output) => TryParse(value, null, out output);

        /// <inheritdoc />
        public virtual bool TryParse(string value, IFormatProvider formatProvider, out T output)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (!Validate(value, formatProvider))
            {
                output = default(T);
                return false;
            }
            var result = true;
            try
            {
                output = DoParse(value, formatProvider);
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch
            {
                output = default(T);
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Attempts to create an instance of the specified type,
        /// but does not perform any validation of the input string.
        /// <remarks>
        /// This method is intended to be used after a string validation was performed.
        /// </remarks> 
        /// </summary>
        /// <param name="value">
        /// The string value to be parsed.
        /// </param>
        /// <param name="formatProvider">
        /// A <see cref="IFormatProvider">format provider</see> used to assist parsing and/or provide culture-specific format recognition. 
        /// </param>
        /// <returns>
        /// An instance of <typeparamref name="T" />.
        /// </returns>
        protected abstract T DoParse(string value, IFormatProvider formatProvider);

        /// <summary>
        /// Performs validation of the input string to ensure
        /// that safe parsing is possible.
        /// </summary>
        /// <param name="value">The value to be validated.</param>
        /// <param name="formatProvider">A format provider used to assist parsing and/or provide culture-specific format recognition.</param>
        /// <returns>true if the value can be parsed to the specified type; false otherwise</returns>
        public virtual bool Validate(string value, IFormatProvider formatProvider) => true;

        T IConverter<string, T>.Convert(string source) => Parse(source);
        bool IConverter<string, T>.TryConvert(string source, out T target) => TryParse(source, out target);

        /// <inheritdoc />
        public Type TargetType => typeof(T);
    }
}
