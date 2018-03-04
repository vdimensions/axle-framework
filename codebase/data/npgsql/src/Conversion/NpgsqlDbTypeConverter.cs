﻿using System.Data;
using System.Diagnostics;

using Axle.Data.Common.Conversion;

using NpgsqlTypes;


namespace Axle.Data.Npgsql.Conversion
{
    #if !NETSTANDARD
    [System.Serializable]
    #endif
    internal abstract class NpgsqlDbTypeConverter<T1, T2> : DbTypeConverter<T1, T2>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly NpgsqlDbType _npgsqlType;

        protected NpgsqlDbTypeConverter(DbType dbType, NpgsqlDbType npgsqlType) : base(dbType)
        {
            _npgsqlType = npgsqlType;
        }

        public NpgsqlDbType NpgsqlType => _npgsqlType;
    }
}