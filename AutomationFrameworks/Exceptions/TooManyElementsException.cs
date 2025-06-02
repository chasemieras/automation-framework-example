using System;
using AutomationFramework.Selenium.Locators;

namespace AutomationFramework.Exceptions
{
    /// <summary>
    /// Thrown when there are too many elements found with the given <see cref="ILocator"/>
    /// </summary>
    /// <param name="elementCount">The number of the elements found</param>
    /// <param name="locator">The <see cref="ILocator"/> used that caused this error</param>
    public class TooManyElementsException(int elementCount, ILocator locator) : 
        Exception($"{elementCount} elements were found with the given locator, try to make the locator more specific\n" +
            $"Type: {locator.Type}\nValue: {locator.Value}")
    {
    }
}
