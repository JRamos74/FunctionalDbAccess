using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FnDbAccess.FnTypes
{
    /// <summary>
    /// Class provides an object for the connection string
    /// </summary>
    public class ConnectionString
    {
        string Value { get; }
        public ConnectionString(string value) { Value = value; }

        public static implicit operator string(ConnectionString connectionString) => connectionString.Value;

        public static implicit operator ConnectionString(string s) => new ConnectionString(s);
        public override string ToString() => Value;

    }
}
