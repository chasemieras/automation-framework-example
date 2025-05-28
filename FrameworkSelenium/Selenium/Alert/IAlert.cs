using FrameworkSelenium.Selenium.Elements;
using FrameworkSelenium.Selenium.Locator;
using System.Collections.Generic;

namespace FrameworkSelenium.Selenium.Browser
{
    public interface IAlert
    {
        /// <summary>
        /// Accepts the alert.
        /// </summary>
        void Accept();

        /// <summary>
        /// Dismisses the alert.
        /// </summary>
        void Dismiss();

        /// <summary>
        /// Gets the text of the alert.
        /// </summary>
        /// <returns>The text of the alert.</returns>
        string GetText();

        /// <summary>
        /// Sends keys to the alert.
        /// </summary>
        /// <param name="keys">The keys to send.</param>
        void SendKeys(string keys);
    }
}
