using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FnDbAccess.Utilities
{
    public sealed class Parameter
    {
        private Parameter() { }
        static Parameter() { }

        public static Parameter Instance { get; } = new Parameter();

        public SqlParameter Create<T>(string parameterName, T parameterValue, SqlDbType parameterType, int parameterSize = 0, ParameterDirection parameterDirection = ParameterDirection.Input)
        {
            SqlParameter sqlp = new SqlParameter
            {
                ParameterName = parameterName,
                SqlDbType = parameterType,
                Direction = parameterDirection
            };
            if (parameterSize > 0) { sqlp.Size = parameterSize; }

            //check the value for nulls or empty string and pass in a dbnull.value
            if (parameterType == SqlDbType.Date || parameterType == SqlDbType.DateTime)
            {
                if (parameterValue == null) { sqlp.Value = DBNull.Value; return sqlp; } //if null value set it to DBNULL and return
                DateTime.TryParse(parameterValue.ToString(), out DateTime dt);
                if (dt.Year < 1900) { sqlp.Value = System.Data.SqlTypes.SqlDateTime.MinValue; } else { sqlp.Value = parameterValue; }
                return sqlp;
            }

            if (parameterValue == null && parameterType == SqlDbType.Structured)
            {
                sqlp.Value = null;
                return sqlp;
            }

            if (parameterValue == null)
                sqlp.Value = DBNull.Value;
            else
                sqlp.Value = parameterValue;

            return sqlp;
        }
    }
}
