﻿using System.Data;
using System.Data.SqlTypes;


namespace Axle.Data.SqlClient.Conversion
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    internal sealed class SqlAnsiStringFixedLengthConverter : SqlDbTypeConverter<string, SqlString>
    {
        public SqlAnsiStringFixedLengthConverter() : base(DbType.AnsiStringFixedLength, SqlDbType.Char) { }

        protected override string GetNotNullSourceValue(SqlString value) => value.Value;
        protected override SqlString GetNotNullDestinationValue(string value) => new SqlString(value);

        protected override bool IsNull(SqlString value) => value.IsNull;

        protected override string SourceNullEquivalent => null;
        protected override SqlString DestinationNullEquivalent => SqlString.Null;
    }
}