﻿using System.Data;
using MySql.Data.MySqlClient;


namespace Axle.Data.MySql.Conversion
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    internal sealed class MySqlBooleanConverter : MySqlSameTypeConverter<bool?>
    {
        public MySqlBooleanConverter() : base(DbType.Boolean, MySqlDbType.Bool, true) { }
    }
}