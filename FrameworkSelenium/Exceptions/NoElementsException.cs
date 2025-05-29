using System;

namespace FrameworkSelenium.Exceptions
{
    public class NoElementsException(ILocator locator) : 
        Exception($"No elements were found with the given locator:\n" +
            $"Type: {locator.Type}\nValue: {locator.Value}")
    {
    }
}
