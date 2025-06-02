using System;
using FrameworkSelenium.Selenium.Locators;

namespace FrameworkSelenium.Exceptions
{
    /// <summary>
    /// Thrown when the given <see cref="ILocator"/> finds no elements
    /// </summary>
    /// <param name="locator">The <see cref="ILocator"/> used that caused this error</param>
    public class NoElementsException(ILocator locator) : 
        Exception($"No elements were found with the given locator:\n" +
            $"Type: {locator.Type}\nValue: {locator.Value}")
    {
    }
}
