using System;

namespace FrameworkSelenium.Exceptions
{
    /// <summary>
    /// Thrown when a <see cref="ILocator"/> is not used correctly
    /// </summary>
    public class LocatorMisuseException(string message) : Exception(message) //todo see if I can change this to ILocator
    {
    }
}
