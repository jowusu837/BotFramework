using System;

namespace BotFramework.Exceptions
{
    public class DriverNotFoundException : Exception
    {
        public DriverNotFoundException(string message)
            : base(message)
        {
        }
    }
}