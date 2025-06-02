using AutomationFramework.Framework.Waits;
using System;

namespace AutomationFramework.Exceptions
{
    /// <summary>
    /// Used to for <see cref="Wait{TContext}"/> to loop, do not use this other than in a <see cref="Wait{TContext}"/> loop
    /// </summary>
    public class ForceWaitLoop() : Exception("Don't throw this exception, it is only meant for waits")
    {
    }
}