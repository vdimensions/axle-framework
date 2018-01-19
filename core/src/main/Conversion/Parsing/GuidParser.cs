﻿using System;


namespace Axle.Conversion.Parsing
{
    /// <summary>
    /// A class that can parse <see cref="string">string</see> representations of 
    /// a <see cref="Guid">globally unique identifier</see> to a valid <see cref="Guid"/> value.
    /// </summary>
    #if !netstandard
    [Serializable]
    #endif
    //[Stateless]
    public sealed class GuidParser : AbstractParser<Guid>
    {
        protected override Guid DoParse(string value, IFormatProvider formatProvider) { return new Guid(value); }
    }
}