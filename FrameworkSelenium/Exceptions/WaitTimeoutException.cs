using System;

namespace FrameworkSelenium.Exceptions
{
    public class WaitTimeoutException(TimeSpan timeout, Exception lastError) : 
        Exception($"Timed out after {timeout.TotalSeconds} seconds waiting for condition to succeed without error.\n" +
            $"Last error: {lastError.GetType()}\n" +
            $"Error message: {lastError.Message}")
    {
    }
}
