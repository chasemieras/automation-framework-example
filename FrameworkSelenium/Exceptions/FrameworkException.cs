using System;

namespace FrameworkSelenium.Exceptions
{
    /// <summary>
    /// Default error for when the framework messes up
    /// </summary>
    /// <param name="message">Why this was thrown</param>
    public class FrameworkException(string message) : Exception(message)
    {
    }
}