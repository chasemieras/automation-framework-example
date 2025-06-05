using AutomationFramework.Config;
using System;

namespace AutomationFramework.Exceptions
{
    /// <summary>
    /// Thrown when the <see cref="FrameworkConfiguration"/> errors
    /// </summary>
    /// <param name="message">Why the error was thrown</param>
    public class FrameworkConfigurationException(string message) : Exception(message)
    {
    }
}