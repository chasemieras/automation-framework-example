using System;

namespace FrameworkSelenium.Exceptions
{
    public class LocatorMisuseException(string message) : Exception(message)
    {
    }
}
