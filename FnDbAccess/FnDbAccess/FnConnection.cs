using FnDbAccess.FnTypes;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace FnDbAccess
{
    using static FnDbAccess.Extensions.FnExtension;

    /// <summary>
    /// Provides functional methods to build database connections
    /// </summary>
    public static class FnConnection
    {
        public static TResult Connect<TResult>(ConnectionString connStr, Func<IDbConnection, TResult> fn)
            => Using(new SqlConnection(connStr), conn => { conn.Open(); return fn(conn); });

        public static async Task<TResult> ConnectAsync<TResult>(ConnectionString connStr, Func<IDbConnection, Task<TResult>> fn)
            => await UsingAsync(new SqlConnection(connStr), async conn => { await conn.OpenAsync(); return await fn(conn); });
    }
}
