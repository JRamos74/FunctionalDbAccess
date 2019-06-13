using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FnDbAccess.FnTypes
{
    /// <summary>
    /// Provides a wrapper for the CommandType / SqlCommandType
    /// </summary>
    public class SqlCommandType
    {
        CommandType Value { get; }

        public SqlCommandType(CommandType commandType)
        {
            Value = commandType;
        }

        public static implicit operator CommandType(SqlCommandType commandType) => commandType.Value;
        public static implicit operator SqlCommandType(CommandType commandType) => new SqlCommandType(commandType);
    }
}
