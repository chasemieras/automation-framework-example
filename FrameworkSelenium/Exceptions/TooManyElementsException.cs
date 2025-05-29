using System;

namespace FrameworkSelenium.Exceptions
{
    public class TooManyElementsException(int elementCount, ILocator locator) : 
        Exception($"{elementCount} elements were found with the given locator, try to make the locator more specific\n" +
            $"Type: {locator.Type}\nValue: {locator.Value}")
    {
    }
}
