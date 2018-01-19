﻿using System;


namespace Axle.Conversion.Parsing
{
    public partial class ParseException : FormatException
    {
        private const string MessageFormat = "Unable to parse an instance of {0} from the string '{1}'.";
        private const string MessageFormatExact = MessageFormat + " The expected value format is '{2}'.";

        public ParseException() : base() { }
        public ParseException(string message) : base(message) { }
        public ParseException(string message, Exception inner) : base(message, inner) { }
        public ParseException(string value, Type type) : this(string.Format(MessageFormat, type.FullName, value)) { }
        public ParseException(string value, Type type, Exception inner) : this(string.Format(MessageFormat, type.FullName, value), inner) { }
        public ParseException(string value, string format, Type type) : this(string.Format(MessageFormatExact, type.FullName, value, format)) { }
        public ParseException(string value, string format, Type type, Exception inner) : this(string.Format(MessageFormatExact, type.FullName, value, format), inner) { }
    }
}