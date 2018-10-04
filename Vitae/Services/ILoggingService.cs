using System;

namespace Vitae.Services
{
    public interface ILoggingService
    {
        void Log(Exception e, string message = "");
    }
}