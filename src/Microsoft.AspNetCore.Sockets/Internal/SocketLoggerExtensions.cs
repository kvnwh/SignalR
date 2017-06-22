using System;
using Microsoft.Extensions.Logging;

namespace Microsoft.AspNetCore.Sockets.Internal
{
    internal static class SocketLoggerExtensions
    {
        // Category: ConnectionManager
        private static readonly Action<ILogger, DateTime, string, Exception> _createdNewConnection =
            LoggerMessage.Define<DateTime, string>(LogLevel.Debug, 0, "{time}: New connection {connectionId} created.");

        private static readonly Action<ILogger, DateTime, string, Exception> _removedConnection =
            LoggerMessage.Define<DateTime, string>(LogLevel.Debug, 1, "{time}: Removing {connectionId} from the list of connections.");

        private static readonly Action<ILogger, DateTime, Exception> _scanningConnections =
            LoggerMessage.Define<DateTime>(LogLevel.Trace, 2, "{time}: Scanning connections for inactive ones.");

        private static readonly Action<ILogger, DateTime, string, Exception> _failedDispose =
            LoggerMessage.Define<DateTime, string>(LogLevel.Error, 3, "{time}: Failed disposing connection {connectionId}.");

        public static void CreatedNewConnection(this ILogger logger, string connectionId)
        {
            if (logger.IsEnabled(LogLevel.Debug))
            {
                _createdNewConnection(logger, DateTime.Now, connectionId, null);
            }
        }

        public static void RemovedConnection(this ILogger logger, string connectionId)
        {
            if (logger.IsEnabled(LogLevel.Debug))
            {
                _removedConnection(logger, DateTime.Now, connectionId, null);
            }
        }

        public static void ScanningConnections(this ILogger logger)
        {
            if (logger.IsEnabled(LogLevel.Trace))
            {
                _scanningConnections(logger, DateTime.Now, null);
            }
        }

        public static void FailedDispose(this ILogger logger, string connectionId, Exception ex)
        {
            if (logger.IsEnabled(LogLevel.Error))
            {
                _failedDispose(logger, DateTime.Now, connectionId, ex);
            }
        }
    }
}
