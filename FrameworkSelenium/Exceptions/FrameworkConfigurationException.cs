using FrameworkSelenium.Config;
using System;

namespace FrameworkSelenium.Exceptions
{
    /// <summary>
    /// Thrown when the <see cref="FrameworkConfiguration"/> errors
    /// </summary>
    /// <param name="message">Why the error was thrown</param>
    public class FrameworkConfigurationException(string message) : Exception(message)
    {
    }
}