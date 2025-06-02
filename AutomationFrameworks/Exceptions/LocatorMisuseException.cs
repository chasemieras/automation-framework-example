using System;
using AutomationFramework.Selenium.Locators;

namespace AutomationFramework.Exceptions
{
    /// <summary>
    /// Thrown when a <see cref="ILocator"/> is not used correctly
    /// </summary>
    public class LocatorMisuseException(string message) : Exception(message) //todo see if I can change this to ILocator
    {
    }
}
