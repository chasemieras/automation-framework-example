using AutomationFramework.Enums;
using OpenQA.Selenium;

namespace AutomationFramework.Selenium.Locators
{
    /// <summary>
    /// The wrapped <see cref="By"/> for the framework
    /// </summary>
    public class Locator : ILocator
    {
        /// <inheritdoc/>
        public LocatorType Type { get; }

        /// <inheritdoc/>
        public string Value { get; }

        /// <inheritdoc/>
        public By ToBy => _by;

        private readonly By _by;

        private Locator(By by, LocatorType type, string value)
        {
            _by = by;
            Type = type;
            Value = value;
        }

        /// <summary>
        /// Creates a new <see cref="Locator"/> that is of <see cref="LocatorType.Id"/>
        /// </summary>
        /// <param name="id">the string that represents the ID</param>
        /// <returns><see cref="Locator"/> that is of type <see cref="LocatorType.Id"/></returns>
        public static Locator Id(string id) => new(By.Id(id), LocatorType.Id, id);

        /// <summary>
        /// Creates a new <see cref="Locator"/> that is of <see cref="LocatorType.Name"/>
        /// </summary>
        /// <param name="name">the string that represents the name</param>
        /// <returns><see cref="Locator"/> that is of type <see cref="LocatorType.Name"/></returns>
        public static Locator Name(string name) => new(By.Name(name), LocatorType.Name, name);

        /// <summary>
        /// Creates a new <see cref="Locator"/> that is of <see cref="LocatorType.CssSelector"/>
        /// </summary>
        /// <param name="selector">the string that represents the CSS Selector</param>
        /// <returns><see cref="Locator"/> that is of type <see cref="LocatorType.CssSelector"/></returns>
        public static Locator CssSelector(string selector) => new(By.CssSelector(selector), LocatorType.CssSelector, selector);

        /// <summary>
        /// Creates a new <see cref="Locator"/> that is of <see cref="LocatorType.XPath"/>
        /// </summary>
        /// <param name="xpath">the string that represents the xpath to the element</param>
        /// <returns><see cref="Locator"/> that is of type <see cref="LocatorType.XPath"/></returns>
        public static Locator XPath(string xpath) => new(By.XPath(xpath), LocatorType.XPath, xpath);

        /// <summary>
        /// Creates a new <see cref="Locator"/> that is of <see cref="LocatorType.Class"/>
        /// </summary>
        /// <param name="className">the string that represents the class of the HTML element
        ///<para>Note: the whole class must be used</para> </param>
        /// <returns><see cref="Locator"/> that is of type <see cref="LocatorType.Class"/></returns>        
        public static Locator Class(string className) => new(By.ClassName(className), LocatorType.Class, className);

        /// <summary>
        /// Creates a new <see cref="Locator"/> that is of <see cref="LocatorType.TagName"/>
        /// </summary>
        /// <param name="tagName">the string that represents the HTML tag name</param>
        /// <returns><see cref="Locator"/> that is of type <see cref="LocatorType.TagName"/></returns>
        public static Locator TagName(string tagName) => new(By.TagName(tagName), LocatorType.TagName, tagName);

        /// <summary>
        /// Creates a new <see cref="Locator"/> that is of <see cref="LocatorType.LinkText"/>
        /// </summary>
        /// <param name="text">the string that represents the link text</param>
        /// <returns><see cref="Locator"/> that is of type <see cref="LocatorType.LinkText"/></returns>
        public static Locator LinkText(string text) => new(By.LinkText(text), LocatorType.LinkText, text);

        /// <summary>
        /// Creates a new <see cref="Locator"/> that is of <see cref="LocatorType.PartialLinkText"/>
        /// </summary>
        /// <param name="text">the string that represents the partial link text</param>
        /// <returns><see cref="Locator"/> that is of type <see cref="LocatorType.PartialLinkText"/></returns>
        public static Locator PartialLinkText(string text) => new(By.PartialLinkText(text), LocatorType.PartialLinkText, text);
    }
}
