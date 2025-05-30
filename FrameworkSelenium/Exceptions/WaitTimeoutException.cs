using FrameworkSelenium.Selenium.Waits;
using System;

namespace FrameworkSelenium.Exceptions
{
    /// <summary>
    /// Throws when the <see cref="Wait"/>s timeout
    /// </summary>
    /// <param name="timeout">The time that was waited</param>
    /// <param name="lastError">The most recent error from the timeout</param>
    public class WaitTimeoutException(TimeSpan timeout, Exception lastError) : 
        Exception($"Timed out after {timeout.TotalSeconds} seconds waiting for condition to succeed without error.\n" +
            $"Last error: {lastError.GetType()}\n" +
            $"Error message: {lastError.Message}")
    {
    }
}
