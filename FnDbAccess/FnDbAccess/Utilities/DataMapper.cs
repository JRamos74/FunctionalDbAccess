using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FnDbAccess.Utilities
{
    /// <summary>
    /// Provides an easy way to map data results to object 
    /// Data column names must match property names
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class DataMapper<T>
    {
        static DataMapper() { }
        private DataMapper() { }

        public static DataMapper<T> Instance { get; } = new DataMapper<T>();

        public T MapDataRowToObject(IDataReader reader)
        {
            IEnumerable<string> columnList = reader.GetSchemaTable().Rows.Cast<DataRow>().Select(c => c["ColumnName"].ToString().ToLower());
            T obj = Activator.CreateInstance<T>();
            foreach (string column in columnList)
            {
                if (reader[column] == DBNull.Value) continue; //skip this process if the database column value is null
                var prop = typeof(T).GetProperties().FirstOrDefault(p => p.Name.Equals(column, StringComparison.CurrentCultureIgnoreCase));

                if (prop == null) continue; //no property found for the mapped field skip to next

                Type propType = prop.PropertyType;

                if (propType.IsGenericType && propType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                {
                    if (reader[column] == DBNull.Value)
                        prop.SetValue(obj, null, null);
                    else
                        prop.SetValue(obj, Convert.ChangeType(reader[column], Nullable.GetUnderlyingType(prop.PropertyType)), null);
                }
                else
                {
                    prop.SetValue(obj, Convert.ChangeType(reader[column], propType), null);
                }
            }
            return obj;
        }
    }
}
