﻿using System.Data;
using MySql.Data.MySqlClient;


namespace Axle.Data.MySql.Conversion
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    internal sealed class MySqlSingleConverter : MySqlSameTypeConverter<float?>
    {
        public MySqlSingleConverter() : base(DbType.Single, MySqlDbType.Float, true) { }
    }
}