using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SELDictionary.ModelConfig
{
    public class ConnectionInterceptor : DbConnectionInterceptor
    {
        public override InterceptionResult ConnectionOpening(
            DbConnection connection,
            ConnectionEventData eventData,
            InterceptionResult result)
        {
            // TODO: Add your business logic
            // var dbName = connection.Database;
            return result;
        }

        public override async ValueTask<InterceptionResult> ConnectionOpeningAsync(
            DbConnection connection,
            ConnectionEventData eventData,
            InterceptionResult result,
            CancellationToken cancellationToken = default)
        {
            // TODO: Add your business logic
            // var dbName = connection.Database;
            return result;
        }

        public override void ConnectionOpened(
            DbConnection connection,
            ConnectionEndEventData eventData)
        {
            // TODO: Add your business logic
            // var dbName = connection.Database;
        }

        public override async Task ConnectionOpenedAsync(
            DbConnection connection,
            ConnectionEndEventData eventData,
            CancellationToken cancellationToken = default)
        {
            // TODO: Add your business logic
            // var dbName = connection.Database;
        }
    }
}
