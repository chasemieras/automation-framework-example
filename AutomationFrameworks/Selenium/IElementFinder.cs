using AutomationFramework.Framework;
using AutomationFramework.Selenium.Locators;
using System;
using System.Collections.Generic;

namespace AutomationFramework.Selenium
{
    /// <summary>
    /// An interface for the methods for classes that interact with elements
    /// </summary>
    public interface IElementFinder
    {
        /// <summary>
        /// Gets the WebElement based on the <see cref="Locator"/>
        /// </summary>
        /// <param name="locator">a <see cref="Locator"/> to a WebElement you want to find</param>
        /// <returns>An <see cref="IElement"/></returns>
        IElement GetElement(ILocator locator);

        /// <summary>
        /// Gets WebElements based on the <see cref="Locator"/>
        /// </summary>
        /// <param name="locator">a <see cref="Locator"/> to a WebElement you want to find</param>
        /// <returns>A <see cref="List{T}"/> of <see cref="IElement"/>s</returns>
        List<IElement> GetElements(ILocator locator);

        /// <summary>
        /// Checks if the given <see cref="Locator"/> find a WebElement
        /// </summary>
        /// <param name="locator">a <see cref="Locator"/> to a WebElement you want to find</param>
        /// <param name="checkIfInteractable">Set to false to not check if the element is Interactable</param>
        /// <param name="defaultTimeout">The default timeout to wait for the element to exist</param>
        /// <returns><b>True</b>: the element does exist | <b>False</b>: the element does not exist </returns>
        bool ElementExist(ILocator locator, bool checkIfInteractable = true, TimeSpan defaultTimeout = default);

        /// <summary>
        /// Checks if the given <see cref="Locator"/> find a list WebElements
        /// </summary>
        /// <param name="locator">a <see cref="Locator"/> to a WebElement you want to find</param>
        /// <param name="checkIfInteractable">Set to false to not check if the element is Interactable</param>
        /// <param name="defaultTimeout">The default timeout to wait for the element to exist</param>
        /// <returns><b>True</b>: the elements do exist | <b>False</b>: the elements do not exist </returns>
        bool ElementsExist(ILocator locator, bool checkIfInteractable = true, TimeSpan defaultTimeout = default);

    }
}
