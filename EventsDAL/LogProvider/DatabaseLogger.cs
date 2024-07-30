using EventsDAL.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsDAL.LogProvider
{
    public class DatabaseLogger : ILogger
    {
       
        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
           return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel != LogLevel.None;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
           
           if(!IsEnabled(logLevel))
            {
                return;
            }
           if (formatter == null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }

            string message = formatter(state, exception);

            if(exception != null)
            {
                LogToDatabase(message, exception);
            }

        }

        private void LogToDatabase(string message, Exception exception)
        {
            string statusCode = GetStatusCodeFromMessage(message);
            using( EventContext context = new EventContext())
            {
                context.ErrorLogs.Add(
                    new ErrorLog
                    {
                        logId = Guid.NewGuid(),
                        Timestamp = DateTime.UtcNow,
                        Errormessage = exception.Message,
                        StackTrace = exception.StackTrace,
                        StatusCode = statusCode
                    }
                    );
                context.SaveChanges();
            }
        }

        private string GetStatusCodeFromMessage(string message)
        {
           string statusCode = string.Empty;
            int startingIndex = message.IndexOf("StatusCode:") + "StatusCode:".Length;
            if (startingIndex >= 0)
            {
                int length = message.IndexOf(",", startingIndex) - startingIndex;
                if (length > 0)
                {
                    statusCode = message.Substring(startingIndex, length);

                }
            }

            return statusCode;
        }
    }
}
