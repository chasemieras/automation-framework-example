using FrameworkSelenium.Selenium.Waits;
using System;

namespace FrameworkSelenium.Exceptions
{
    /// <summary>
    /// Used to for <see cref="Wait"/> to loop, do not use this other than in a <see cref="Wait"/> loop
    /// </summary>
    public class ForceWaitLoop() : Exception("Don't throw this exception, it is only meant for waits")
    {
    }
}