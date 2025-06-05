using System;

namespace AutomationFramework.Exceptions
{
    /// <summary>
    /// Thrown when the html report errors
    /// </summary>
    /// <param name="message">Why the error was thrown</param>
    public class HtmlReportException(string message) : Exception($"There was an issue with the HTML report:\n{message}")
    {
    }
}