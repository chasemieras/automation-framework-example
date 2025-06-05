using System.Drawing;
using AutomationFramework.Selenium;

namespace AutomationFramework.Framework
{
    /// <summary>
    /// The interface for the WebElement wrapper
    /// </summary>
    public interface IElement : IElementFinder
    {

        //todo add HTTP checker for link | Shadowroot | Select Element

        #region Variables

        /// <summary>
        /// Indicates if the element is displayed on the page
        /// </summary>
        bool IsDisplayed { get; }

        /// <summary>
        /// Indicates if the element is enabled
        /// </summary>
        bool IsEnabled { get; }

        /// <summary>
        /// Indicates if the element is displayed and enabled
        /// </summary>
        bool IsInteractable { get; }

        /// <summary>
        /// Indicates whether the element is selected (for checkboxes, radio buttons, etc.)
        /// </summary>
        bool IsSelected { get; }

        /// <summary>
        /// Gets the upper left of the element in relation to the upper left of the current window
        /// </summary>
        Point Location { get; }

        #endregion

        #region Click Interactions

        /// <summary>
        /// Clicks on the element
        /// </summary>
        void Click();

        /// <summary>
        /// Right clicks on the element
        /// </summary>
        void RightClick();

        #endregion

        #region Attributes

        /// <summary>
        /// Gets the value of a specified attribute of the element
        /// </summary>
        /// <param name="attribute">the attribute you want the value of</param>
        /// <returns>a string that is the value of the attribute</returns>
        string GetAttribute(string attribute);

        /// <summary>
        /// Checks if a specified attribute exists on the element
        /// </summary>
        /// <param name="attribute"></param>
        /// <returns><b>True</b>: The attribute exists | <b>False</b>: The attribute does not exist or returned null</returns>
        bool AttributeExists(string attribute);

        /// <summary>
        /// Gets the value of the src attribute of the element, typically used for images or iframes
        /// </summary>
        string Src { get; }

        /// <summary>
        /// Gets the value of the href attribute of the element, typically used for links
        /// </summary>
        string Href { get; }

        /// <summary>
        /// Gets the value of the value attribute of the element, typically used for input fields
        /// </summary>
        string Value { get; }

        /// <summary>
        /// Gets the value of the class attribute of the element
        /// </summary>
        string ClassName { get; }

        /// <summary>
        /// Gets the value of the id attribute of the element
        /// </summary>
        string Id { get; }

        /// <summary>
        ///  Gets the value of the name attribute of the element
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the value of the title attribute of the element
        /// </summary>
        string Title { get; }

        /// <summary>
        /// Gets the value of the inner HTML of the element
        /// </summary>
        string InnerHtml { get; }

        /// <summary>
        /// Gets the value of the outer HTML of the element
        /// </summary>
        string OuterHtml { get; }

        /// <summary>
        /// Gets the value of the target attribute of the element, typically used for offsite links
        /// </summary>
        string Target { get; }

        /// <summary>
        /// Gets the value of the alt text attribute of the element, typically used for images
        /// </summary>
        string AltText { get; }

        /// <summary>
        /// Gets the tag name of the element
        /// </summary>
        string TagName { get; }

        /// <summary>
        /// Gets the value of the class attribute of the element
        /// </summary>
        string Class { get; }

        /// <summary>
        /// Gets the value of the rel attribute of the element, typically used for offsite links
        /// </summary>
        string Rel { get; }

        /// <summary>
        /// Gets the value of the checked attribute of the element, typically used for checkboxes or radio buttons
        /// </summary>
        bool Checked { get; }

        #endregion

        #region Styling

        /// <summary>
        /// Gets the CSS styling of the element
        /// </summary>
        string Styling { get; }

        /// <summary>
        /// Gets the value of a specific CSS property of the element
        /// </summary>
        /// <param name="propertyName">the specific property you want to check</param>
        /// <returns>the value of the given property</returns>
        string GetCssValue(string propertyName);

        #endregion

        #region Text Interaction

        /// <summary>
        /// Gets the visible text of the element
        /// </summary>
        string Text { get; }

        /// <summary>
        /// Gets some kind of text from the element, checking 
        /// <see cref="Text"/>, <see cref="Title"/>, 
        /// <see cref="AltText"/>, <see cref="Name"/>, or
        /// <see cref="Value"/> in that order
        /// </summary>
        string ExtractString { get; }

        /// <summary>
        /// Send the given text to the element
        /// </summary>
        /// <param name="text">the text or keys from the keyboard to send the element</param>
        void SendKeys(string text);

        /// <summary>
        /// Sends an Enter key to the element
        /// </summary>
        void SendEnter();

        /// <summary>
        /// Clears the text of the element
        /// </summary>
        void Clear();

        /// <summary>
        /// Submits the text of the element
        /// </summary>
        void Submit();

        #endregion

        #region Other Methods

        /// <summary>
        /// Scrolls the page to the element
        /// </summary>
        void ScrollToElement();

        /// <summary>
        /// Gets the pseudoelement of the element
        /// </summary>
        /// <param name="pseudoElement">the pseudoelement you want to grab, typically before or after</param>
        /// <param name="property">the property you want to grab from, typically content</param>
        /// <returns>the given <paramref name="property"/> of the <paramref name="pseudoElement"/> from the element</returns>
        public string GetPseudoelement(string pseudoElement = "before", string property = "content");

        #endregion

    }
}
