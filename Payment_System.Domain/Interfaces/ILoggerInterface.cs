using System;
using System.Collections.Generic;
using System.Text;

namespace Payment_System.Domain.Interfaces
{
    public  interface ILoggerInterface
    {
        void LogInformation(string message, params object[] args);
        void LogError(string message, params object[] args);
    }
}
