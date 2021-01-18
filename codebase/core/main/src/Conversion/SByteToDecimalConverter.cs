﻿namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="sbyte"/> and <see cref="decimal"/>.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
    [System.Serializable]
    #endif
    public sealed class SByteToDecimalConverter : AbstractTwoWayConverter<sbyte, decimal>
    {
        /// <inheritdoc />
        protected override decimal DoConvert(sbyte source) => source;

        /// <inheritdoc />
        protected override sbyte DoConvertBack(decimal source) => (sbyte) source;
    }
}
