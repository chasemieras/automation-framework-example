using AutomationFramework.Selenium;
using AutomationFramework.Selenium.Locators;
using System;

namespace AutomationFramework.Framework.Waits
{
    /// <summary>
    /// Interface for <see cref="Wait{TContext}"/>, which helps the code to wait for things to happen 🐈‍
    /// </summary>
    public interface IWait<TContext> where TContext : IElementFinder
    {

        /// <summary>
        /// Waits for an element to appear based on the given <paramref name="locator"/>
        /// </summary>
        /// <param name="locator">A <see cref="ILocator"/> to check for</param>
        /// <returns>An <see cref="IElement"/>, if it was found</returns>
        IElement UntilElementExists(ILocator locator);

        /// <summary>
        /// Waits for the <paramref name="condition"/> to be met 
        /// </summary>
        /// <param name="condition">The code you want repeated</param>
        /// <returns><b>True</b>: The <paramref name="condition"/> was met before the timeout | <b>False</b>: The <paramref name="condition"/> was not met before the timeout </returns>
        bool Until(Func<TContext, bool> condition);

        /// <summary>
        /// Waits for the <paramref name="condition"/> to be met and errors if it times out
        /// </summary>
        /// <param name="condition">The code you want repeated</param>
        void UntilSuccessful(Func<TContext, bool> condition);

    }
}
