﻿using System.Collections.Generic;
using System.Data;
using Axle.Data.Common;
using Axle.Data.Conversion;
using Axle.Data.SQLite.Conversion;

namespace Axle.Data.SQLite
{
    using SqliteParameter = System.Data.SQLite.SQLiteParameter;
    
    internal sealed class SQLiteParameterValueSetter : DbParameterValueSetter<SqliteParameter, SQLiteType>
    {
        private class SQLiteDbTypeEqualityComparer : IEqualityComparer<SQLiteType>
        {
            public bool Equals(SQLiteType x, SQLiteType y) => x == y;

            public int GetHashCode(SQLiteType obj) => (int) obj;
        }

        public SQLiteParameterValueSetter() : base(new SQLiteDbTypeEqualityComparer())
        {
            RegisterConverter(new SQLiteInt16Converter());
            RegisterConverter(new SQLiteInt32Converter());
            RegisterConverter(new SQLiteInt64Converter());
            RegisterConverter(new SQLiteSingleConverter());
            RegisterConverter(new SQLiteDoubleConverter());
            RegisterConverter(new SQLiteDecimalConverter());
            RegisterConverter(new SQLiteTextConverter());
            RegisterConverter(new SQLiteDateTimeConverter());
        }

        private void RegisterConverter<T1, T2>(SQLiteDbTypeConverter<T1, T2> converter)
        {
            if (converter.RegisterAbstractDbType)
            {
                RegisterConverter(converter, converter.SQLiteType, converter.DbType);
            }
            else
            {
                RegisterConverter(converter, converter.SQLiteType);
            }
        }

        protected override void SetValue(SqliteParameter parameter, DbType type, object value, IDbValueConverter converter)
        {
            parameter.Value = converter.Convert(value);
            parameter.ResetDbType();
        }

        protected override void SetValue(SqliteParameter parameter, SQLiteType type, object value, IDbValueConverter converter)
        {
            parameter.Value = converter.Convert(value);
        }
    }
}