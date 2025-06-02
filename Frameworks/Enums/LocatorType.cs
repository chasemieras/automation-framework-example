using OpenQA.Selenium;

namespace FrameworkSelenium.Enums
{
    /// <summary>
    /// An enum representation of the <see cref="By"/>
    /// </summary>
    public enum LocatorType
    {
        /// <summary>
        /// <see cref="By.Id(string)"/>
        /// </summary>
        Id,

        /// <summary>
        /// <see cref="By.Name(string)"/>
        /// </summary>
        Name,

        /// <summary>
        /// <see cref="By.CssSelector(string)"/>
        /// </summary>
        CssSelector,

        /// <summary>
        /// <see cref="By.XPath(string)"/>
        /// </summary>
        XPath,

        /// <summary>
        /// <see cref="By.ClassName(string)"/>
        /// </summary>
        Class,

        /// <summary>
        /// <see cref="By.TagName(string)"/>
        /// </summary>
        TagName,

        /// <summary>
        /// <see cref="By.LinkText(string)"/>
        /// </summary>
        LinkText,

        /// <summary>
        /// <see cref="By.PartialLinkText(string)"/>
        /// </summary>
        PartialLinkText
    }

}
