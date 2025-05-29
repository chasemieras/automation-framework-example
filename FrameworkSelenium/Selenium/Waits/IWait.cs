using FrameworkSelenium.Selenium.Browsers;
using FrameworkSelenium.Selenium.Elements;
using System;

namespace FrameworkSelenium.Selenium.Waits
{
    /// <summary>
    /// Interface for <see cref="Wait"/>, which helps the code to wait for things to happen 🐈‍
    /// </summary>
    public interface IWait
    {
        /// <summary>
        /// Waits for an element to appear based on the given <paramref name="locator"/>, using the <see cref="IBrowser"/> <paramref name="context"/>
        /// </summary>
        /// <param name="context">A <see cref="IBrowser"/> to use with the <see cref="ILocator"/></param>
        /// <param name="locator">A <see cref="ILocator"/> to check for</param>
        /// <returns>An <see cref="IElement"/>, if it was found</returns>
        IElement UntilElementExists(IBrowser context, ILocator locator);

        /// <summary>
        /// Waits for an element to appear based on the given <paramref name="locator"/>, using the <see cref="IElement"/> <paramref name="context"/>
        /// </summary>
        /// <param name="context">A <see cref="IElement"/> to use with the <see cref="ILocator"/></param>
        /// <param name="locator">A <see cref="ILocator"/> to check for</param>
        /// <returns>An <see cref="IElement"/>, if it was found</returns>
        IElement UntilElementExists(IElement context, ILocator locator);

        /// <summary>
        /// Waits for the <paramref name="condition"/> to be meet using the <see cref="IBrowser"/> <paramref name="context"/>
        /// </summary>
        /// <param name="context">A <see cref="IBrowser"/> to use during the wait</param>
        /// <param name="condition">The code you want repeated</param>
        /// <returns><b>True</b>: The <paramref name="condition"/> was met before the timeout | <b>False</b>: The <paramref name="condition"/> was not met before the timeout </returns>
        bool Until(IBrowser context, Func<IBrowser, bool> condition);

        /// <summary>
        /// Waits for the <paramref name="condition"/> to be meet using the <see cref="IElement"/> <paramref name="context"/>
        /// </summary>
        /// <param name="context">A <see cref="IElement"/> to use during the wait</param>
        /// <param name="condition">The code you want repeated</param>
        /// <returns><b>True</b>: The <paramref name="condition"/> was met before the timeout | <b>False</b>: The <paramref name="condition"/> was not met before the timeout </returns>
        bool Until(IElement context, Func<IElement, bool> condition);

        /// <summary>
        /// Waits for the <paramref name="condition"/> to be meet using the <see cref="IBrowser"/> <paramref name="context"/>, and errors if it times out
        /// </summary>
        /// <param name="context">A <see cref="IBrowser"/> to use during the wait</param>
        /// <param name="condition">The code you want repeated</param>
        void UntilSuccessful(IBrowser context, Func<IBrowser, bool> condition);

        /// <summary>
        /// Waits for the <paramref name="condition"/> to be meet using the <see cref="IBrowser"/> <paramref name="context"/>, and errors if it times out
        /// </summary>
        /// <param name="context">A <see cref="IElement"/> to use during the wait</param>
        /// <param name="condition">The code you want repeated</param>
        void UntilSuccessful(IElement context, Func<IElement, bool> condition);
    }
}
