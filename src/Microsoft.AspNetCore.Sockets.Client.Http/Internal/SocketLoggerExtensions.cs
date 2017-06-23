using System;
using System.Net.WebSockets;
using Microsoft.Extensions.Logging;

namespace Microsoft.AspNetCore.Sockets.Internal
{
    internal static class SocketLoggerExtensions
    {
        // Category: Shared with LongPollingTransport, WebSocketsTransport and ServerSentEventsTransport
        private static readonly Action<ILogger, DateTime, Exception> _startTransport =
            LoggerMessage.Define<DateTime>(LogLevel.Information, 0, "{time}: Starting transport.");

        private static readonly Action<ILogger, DateTime, Exception> _transportStopped =
            LoggerMessage.Define<DateTime>(LogLevel.Debug, 1, "{time}: Transport stopped.");

        private static readonly Action<ILogger, DateTime, Exception> _startReceive =
            LoggerMessage.Define<DateTime>(LogLevel.Information, 2, "{time}: Starting receive loop.");

        private static readonly Action<ILogger, DateTime, Exception> _receiveStopped =
            LoggerMessage.Define<DateTime>(LogLevel.Information, 8, "{time}: Receive loop stopped.");

        private static readonly Action<ILogger, DateTime, Exception> _receiveCanceled =
            LoggerMessage.Define<DateTime>(LogLevel.Debug, 7, "{time}: Receive loop canceled.");

        private static readonly Action<ILogger, DateTime, Exception> _transportStopping =
            LoggerMessage.Define<DateTime>(LogLevel.Information, 3, "{time}: Transport is stopping.");

        private static readonly Action<ILogger, DateTime, Exception> _sendStarted =
            LoggerMessage.Define<DateTime>(LogLevel.Information, 9, "{time}: Starting the send loop.");

        private static readonly Action<ILogger, DateTime, Exception> _sendStopped =
            LoggerMessage.Define<DateTime>(LogLevel.Information, 14, "{time}: Send loop stopped.");

        private static readonly Action<ILogger, DateTime, Exception> _sendCanceled =
            LoggerMessage.Define<DateTime>(LogLevel.Debug, 13, "{time}: Send loop canceled.");

        // Category: WebSocketsTransport

        private static readonly Action<ILogger, DateTime, WebSocketCloseStatus?, Exception> _webSocketClosed =
            LoggerMessage.Define<DateTime, WebSocketCloseStatus?>(LogLevel.Information, 4, "{time}: Websocket closed by the server. Close status {closeStatus}.");

        private static readonly Action<ILogger, DateTime, WebSocketMessageType, int, bool, Exception> _messageReceived =
            LoggerMessage.Define<DateTime, WebSocketMessageType, int, bool>(LogLevel.Debug, 5, "{time}: Message received. Type: {messageCount}, size: {count}, EndOfMessage: {endOfMessage}.");

        private static readonly Action<ILogger, DateTime, int, Exception> _messageToApp =
            LoggerMessage.Define<DateTime, int>(LogLevel.Information, 6, "{time}: Passing message to application. Payload size: {count}.");

        private static readonly Action<ILogger, DateTime, int, Exception> _receivedFromApp =
            LoggerMessage.Define<DateTime, int>(LogLevel.Debug, 10, "{time}: Received message from application. Payload size: {count}.");

        private static readonly Action<ILogger, DateTime, Exception> _sendMessageCanceled =
            LoggerMessage.Define<DateTime>(LogLevel.Information, 11, "{time}: Sending a message canceled.");

        private static readonly Action<ILogger, DateTime, Exception> _errorSendingMessage =
            LoggerMessage.Define<DateTime>(LogLevel.Error, 12, "{time}: Error while sending a message.");

        private static readonly Action<ILogger, DateTime, Exception> _closingWebSocket =
            LoggerMessage.Define<DateTime>(LogLevel.Information, 15, "{time}: Closing WebSocket.");

        private static readonly Action<ILogger, DateTime, Exception> _closingWebSocketFailed =
            LoggerMessage.Define<DateTime>(LogLevel.Information, 16, "{time}: Closing webSocket failed.");

        // Category: ServerSentEventsTransport

        private static readonly Action<ILogger, DateTime, Exception> _eventStreamEnded =
            LoggerMessage.Define<DateTime>(LogLevel.Debug, 6, "{time}: Server-Sent Event Stream ended.");

        // Category: LongPollingTransport

        private static readonly Action<ILogger, DateTime, Exception> _closingConnection =
            LoggerMessage.Define<DateTime>(LogLevel.Debug, 6, "{time}: The server is closing the connection.");

        private static readonly Action<ILogger, DateTime, Exception> _receivedMessages =
            LoggerMessage.Define<DateTime>(LogLevel.Debug, 6, "{time}: Received messages from the server.");

        private static readonly Action<ILogger, DateTime, Uri, Exception> _errorPolling =
            LoggerMessage.Define<DateTime, Uri>(LogLevel.Error, 6, "{time}: Error while polling '{pollUrl}'.");

        public static void StartTransport(this ILogger logger)
        {
            if (logger.IsEnabled(LogLevel.Information))
            {
                _startTransport(logger, DateTime.Now, null);
            }
        }

        public static void TransportStopped(this ILogger logger, Exception exception)
        {
            if (logger.IsEnabled(LogLevel.Debug))
            {
                _transportStopped(logger, DateTime.Now, exception);
            }
        }

        public static void StartReceive(this ILogger logger)
        {
            if (logger.IsEnabled(LogLevel.Information))
            {
                _startReceive(logger, DateTime.Now, null);
            }
        }

        public static void TransportStopping(this ILogger logger)
        {
            if (logger.IsEnabled(LogLevel.Information))
            {
                _transportStopping(logger, DateTime.Now, null);
            }
        }

        public static void WebSocketClosed(this ILogger logger, WebSocketCloseStatus? closeStatus)
        {
            if (logger.IsEnabled(LogLevel.Information))
            {
                _webSocketClosed(logger, DateTime.Now, closeStatus, null);
            }
        }

        public static void MessageReceived(this ILogger logger, WebSocketMessageType messageType, int count, bool endOfMessage)
        {
            if (logger.IsEnabled(LogLevel.Debug))
            {
                _messageReceived(logger, DateTime.Now, messageType, count, endOfMessage, null);
            }
        }

        public static void MessageToApp(this ILogger logger, int count)
        {
            if (logger.IsEnabled(LogLevel.Information))
            {
                _messageToApp(logger, DateTime.Now, count, null);
            }
        }

        public static void ReceiveCanceled(this ILogger logger)
        {
            if (logger.IsEnabled(LogLevel.Debug))
            {
                _receiveCanceled(logger, DateTime.Now, null);
            }
        }

        public static void ReceiveStopped(this ILogger logger)
        {
            if (logger.IsEnabled(LogLevel.Information))
            {
                _receiveStopped(logger, DateTime.Now, null);
            }
        }

        public static void SendStarted(this ILogger logger)
        {
            if (logger.IsEnabled(LogLevel.Information))
            {
                _sendStarted(logger, DateTime.Now, null);
            }
        }

        public static void ReceivedFromApp(this ILogger logger, int count)
        {
            if (logger.IsEnabled(LogLevel.Debug))
            {
                _receivedFromApp(logger, DateTime.Now, count, null);
            }
        }

        public static void SendMessageCanceled(this ILogger logger)
        {
            if (logger.IsEnabled(LogLevel.Information))
            {
                _sendMessageCanceled(logger, DateTime.Now, null);
            }
        }

        public static void ErrorSendingMessage(this ILogger logger, Exception exception)
        {
            if (logger.IsEnabled(LogLevel.Error))
            {
                _errorSendingMessage(logger, DateTime.Now, exception);
            }
        }

        public static void SendCanceled(this ILogger logger)
        {
            if (logger.IsEnabled(LogLevel.Debug))
            {
                _sendCanceled(logger, DateTime.Now, null);
            }
        }

        public static void SendStopped(this ILogger logger)
        {
            if (logger.IsEnabled(LogLevel.Information))
            {
                _sendStopped(logger, DateTime.Now, null);
            }
        }

        public static void ClosingWebSocket(this ILogger logger)
        {
            if (logger.IsEnabled(LogLevel.Information))
            {
                _closingWebSocket(logger, DateTime.Now, null);
            }
        }

        public static void ClosingWebSocketFailed(this ILogger logger, Exception exception)
        {
            if (logger.IsEnabled(LogLevel.Information))
            {
                _closingWebSocketFailed(logger, DateTime.Now, exception);
            }
        }

        public static void EventStreamEnded(this ILogger logger)
        {
            if (logger.IsEnabled(LogLevel.Debug))
            {
                _eventStreamEnded(logger, DateTime.Now, null);
            }
        }

        public static void ClosingConnection(this ILogger logger)
        {
            if (logger.IsEnabled(LogLevel.Debug))
            {
                _closingConnection(logger, DateTime.Now, null);
            }
        }

        public static void ReceivedMessages(this ILogger logger)
        {
            if (logger.IsEnabled(LogLevel.Debug))
            {
                _receivedMessages(logger, DateTime.Now, null);
            }
        }

        public static void ErrorPolling(this ILogger logger, Uri pollUrl, Exception exception)
        {
            if (logger.IsEnabled(LogLevel.Error))
            {
                _errorPolling(logger, DateTime.Now, pollUrl, exception);
            }
        }
    }
}
