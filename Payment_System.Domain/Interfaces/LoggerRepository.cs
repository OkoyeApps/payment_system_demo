using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Payment_System.Domain.Interfaces
{
    public class LoggerRepository : ILoggerInterface
    {
        private readonly ILogger<LoggerRepository> _logger;

        public LoggerRepository(ILogger<LoggerRepository> logger)
        {
            _logger = logger;
        }
        public void LogError(string message, params object[] args)
        {
            _logger.LogError(message, args);
        }

        public void LogInformation(string message, params object[] args)
        {
            _logger.LogInformation("======================");
            _logger.LogInformation(message, args);
            _logger.LogInformation("======================");
        }
    }
}
