using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FnDbAccess.FnTypes
{
    /// <summary>
    /// Provides a wrapper for the SQLCommandText 
    /// </summary>
    public class SqlCommandText
    {
        string Value { get; }
        public SqlCommandText(string text) { Value = text; }

        public static implicit operator string(SqlCommandText commandText) => commandText.Value;

        public static implicit operator SqlCommandText(string text) => new SqlCommandText(text);
        public override string ToString() => Value;

    }
}
