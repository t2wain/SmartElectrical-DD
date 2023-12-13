using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SELDictionary.ModelConfig
{
    /// <summary>
    /// The purpose of this interceptor is to remove the double-quote
    /// from the generated SQL statement by EntityFramework.
    /// </summary>
    public class QueryCommandInterceptor : DbCommandInterceptor
    {
        public override InterceptionResult<int> NonQueryExecuting(
           DbCommand command,
           CommandEventData eventData,
           InterceptionResult<int> result)
        {
            // TODO: Add your business logic
            // var dbName = connection.Database;
            // var commandText = command.CommandText;
            command.CommandText = command.CommandText.Replace("\"", string.Empty);
            return result;
        }

        public override ValueTask<InterceptionResult<int>> NonQueryExecutingAsync(
            DbCommand command,
            CommandEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            // TODO: Add your business logic
            // var dbName = connection.Database;
            // var commandText = command.CommandText;
            command.CommandText = command.CommandText.Replace("\"", string.Empty);
            return new ValueTask<InterceptionResult<int>>(result);
        }

        public override InterceptionResult<DbDataReader> ReaderExecuting(
            DbCommand command,
            CommandEventData eventData,
            InterceptionResult<DbDataReader> result)
        {
            // TODO: Add your business logic
            // var dbName = connection.Database;
            // var commandText = command.CommandText;
            command.CommandText = command.CommandText.Replace("\"", string.Empty);
            return result;
        }

        public override ValueTask<InterceptionResult<DbDataReader>> ReaderExecutingAsync(
            DbCommand command,
            CommandEventData eventData,
            InterceptionResult<DbDataReader> result,
            CancellationToken cancellationToken = default)
        {
            // TODO: Add your business logic
            // var dbName = connection.Database;
            // var commandText = command.CommandText;
            command.CommandText = command.CommandText.Replace("\"", string.Empty);
            return new ValueTask<InterceptionResult<DbDataReader>>(result);
        }

        public override InterceptionResult<object> ScalarExecuting(
           DbCommand command,
           CommandEventData eventData,
           InterceptionResult<object> result)
        {
            // TODO: Add your business logic
            // var dbName = connection.Database;
            // var commandText = command.CommandText;
            command.CommandText = command.CommandText.Replace("\"", string.Empty);
            return result;
        }

        public override ValueTask<InterceptionResult<object>> ScalarExecutingAsync(
            DbCommand command,
            CommandEventData eventData,
            InterceptionResult<object> result,
            CancellationToken cancellationToken = default)
        {
            // TODO: Add your business logic
            // var dbName = connection.Database;
            // var commandText = command.CommandText;
            command.CommandText = command.CommandText.Replace("\"", string.Empty);
            return new ValueTask<InterceptionResult<object>>(result);
        }
    }
}
