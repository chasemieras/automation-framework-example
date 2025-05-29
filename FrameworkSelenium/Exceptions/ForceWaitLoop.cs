using System;

namespace FrameworkSelenium.Exceptions
{
    public class ForceWaitLoop() : Exception("Don't throw this exception, it is only meant for waits")
    {
    }
}