using FnDbAccess.FnTypes;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace FnDbAccess.Extensions
{
    using static FnExtension;

    public static class FnDbAcess
    {
        public static async Task<int> ExecuteNonQueryAsync(this IDbConnection conn, SqlCommandType commandType, SqlCommandText commandText, Action<IDbDataParameter[]> returnParams = null, IDbDataParameter[] sqlParameters = null)
            => await UsingAsync(new SqlCommand(), async cmd =>
            {
                cmd.Connection = (SqlConnection)conn;
                cmd.CommandType = commandType;
                cmd.CommandText = commandText;
                cmd.CommandTimeout = 5000;
                if (sqlParameters != null)
                {
                    foreach (SqlParameter parm in sqlParameters)
                    {
                        if ((parm.Direction == ParameterDirection.InputOutput || parm.Direction == ParameterDirection.Output) && parm.Value == null) { parm.Value = DBNull.Value; }
                        cmd.Parameters.Add(parm);
                    }
                }

                int result = await cmd.ExecuteNonQueryAsync();
                returnParams?.Invoke(sqlParameters);

                return result;
            });

        public static int ExecuteNonQuery(this IDbConnection conn, SqlCommandType commandType, SqlCommandText commandText, Action<IDbDataParameter[]> returnParams = null, IDbDataParameter[] sqlParameters = null)
         =>
            Using(new SqlCommand(), cmd =>
            {
                cmd.Connection = (SqlConnection)conn;
                cmd.CommandType = commandType;
                cmd.CommandText = commandText;
                cmd.CommandTimeout = 5000;
                if (sqlParameters != null)
                {
                    foreach (SqlParameter parm in sqlParameters)
                    {
                        if ((parm.Direction == ParameterDirection.InputOutput || parm.Direction == ParameterDirection.Output) && parm.Value == null) { parm.Value = DBNull.Value; }
                        cmd.Parameters.Add(parm);
                    }
                }

                int result = cmd.ExecuteNonQuery();
                returnParams?.Invoke(sqlParameters);

                return result;
            });

        public static async Task ExecuteReaderAsync(this IDbConnection conn, SqlCommandType commandType, SqlCommandText commandText, Action<IDataReader, int> dataMapper, params SqlParameter[] sqlParameters)
            =>
            await UsingAsync(new SqlCommand(), async cmd =>
            {
                int resultSet = 0;
                cmd.Connection = (SqlConnection)conn;
                cmd.CommandType = commandType;
                cmd.CommandText = commandText;
                cmd.CommandTimeout = 5000;
                if (sqlParameters != null)
                {
                    foreach (SqlParameter parm in sqlParameters)
                    {
                        if ((parm.Direction == ParameterDirection.InputOutput || parm.Direction == ParameterDirection.Output) && parm.Value == null) { parm.Value = DBNull.Value; }
                        cmd.Parameters.Add(parm);
                    }
                }

                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                while (true)
                {
                    while (await reader.ReadAsync())
                    {
                        dataMapper?.Invoke(reader, resultSet);
                    }

                    resultSet++;

                    if (reader.IsClosed || !reader.NextResultAsync().Result)
                        break;

                }

                return resultSet;
            });

        public static void ExecuteReader(this IDbConnection conn, SqlCommandText commandText, SqlCommandType commandType, Action<IDataReader, int> dataMapper, params SqlParameter[] sqlParameters)
            =>
            Using(new SqlCommand(), cmd =>
            {
                int resultSet = 0;
                cmd.Connection = (SqlConnection)conn;
                cmd.CommandType = commandType;
                cmd.CommandText = commandText;
                cmd.CommandTimeout = 5000;
                if (sqlParameters != null)
                {
                    foreach (SqlParameter parm in sqlParameters)
                    {
                        if ((parm.Direction == ParameterDirection.InputOutput || parm.Direction == ParameterDirection.Output) && parm.Value == null) { parm.Value = DBNull.Value; }
                        cmd.Parameters.Add(parm);
                    }
                }

                IDataReader reader = cmd.ExecuteReader();
                while (true)
                {
                    while (reader.Read())
                    {
                        dataMapper?.Invoke(reader, resultSet);
                    }

                    resultSet++;

                    if (reader.IsClosed || !reader.NextResult())
                        break;

                }

                return resultSet;

            });
    }
}
