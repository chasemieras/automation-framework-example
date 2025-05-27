using System;

namespace FrameworkSelenium.Exceptions
{
    public class FrameworkException(string message) : Exception(message)
    {
    }
}