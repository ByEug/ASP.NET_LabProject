using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LabProject
{
    public class CustomLogger : ILogger
    {
        private string filePath;
        private string filePathError;
        private static object _lock = new object();
        public CustomLogger()
        {
            filePath = "log.txt";
            filePathError = "log_error.txt";
        }
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (formatter != null)
            {
                lock (_lock)
                {
                    File.AppendAllText(filePath, formatter(state, exception) + Environment.NewLine);
                    if (logLevel == LogLevel.Error)
                    {
                        File.AppendAllText(filePathError, formatter(state, exception) + Environment.NewLine);
                        Console.WriteLine($"{formatter(state, exception)}");
                    }
                }
                
            }
        }
    }
}
