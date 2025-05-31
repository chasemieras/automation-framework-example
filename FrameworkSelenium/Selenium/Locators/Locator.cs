using FrameworkSelenium.Enums;
using OpenQA.Selenium;

namespace FrameworkSelenium.Selenium.Locators
{
    public class Locator : ILocator
    {
        private readonly By _by;

        public LocatorType Type { get; }
        public string Value { get; }

        private Locator(By by, LocatorType type, string value)
        {
            _by = by;
            Type = type;
            Value = value;
        }

        public By ToBy => _by;

        public static Locator Id(string id) => new (By.Id(id), LocatorType.Id, id);
        public static Locator Name(string name) => new (By.Name(name), LocatorType.Name, name);
        public static Locator Css(string selector) => new (By.CssSelector(selector), LocatorType.CssSelector, selector);
        public static Locator XPath(string xpath) => new (By.XPath(xpath), LocatorType.XPath, xpath);
        public static Locator Class(string className) => new (By.ClassName(className), LocatorType.ClassName, className);
        public static Locator TagName(string tagName) => new (By.TagName(tagName), LocatorType.TagName, tagName);
        public static Locator LinkText(string text) => new (By.LinkText(text), LocatorType.LinkText, text);
        public static Locator PartialLinkText(string text) => new (By.PartialLinkText(text), LocatorType.PartialLinkText, text);
    }
}
