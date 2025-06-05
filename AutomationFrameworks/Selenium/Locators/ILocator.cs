using AutomationFramework.Enums;
using OpenQA.Selenium;

namespace AutomationFramework.Selenium.Locators
{
    /// <summary>
    /// Interface for defining the wrapped <see cref="By"/> in Selenium
    /// </summary>
    public interface ILocator
    {

        /// <summary>
        /// Converts the <see cref="Locator" /> to a <see cref="By"/> object
        /// </summary>
        By ToBy { get; }

        /// <summary>
        /// The kind of <see cref="Locator"/> being used
        /// </summary>
        LocatorType Type { get; }

        /// <summary>
        /// The value used to find the element in the DOM, 
        /// which corresponds to the kind of <see cref="LocatorType"/> being used
        /// </summary>
        string Value { get; }
    }
}
