using FrameworkSelenium.Enums;
using OpenQA.Selenium;

namespace FrameworkSelenium.Selenium.Locator
{
    public interface ILocator
    {
        By ToBy();
        LocatorType Type { get; }
    }

    public class Locator : ILocator
    {
        private readonly By _by;

        public LocatorType Type { get; }

        private Locator(By by, LocatorType type)
        {
            _by = by;
            Type = type;
        }

        public By ToBy() => _by;

        public static Locator Id(string id) => new Locator(By.Id(id), LocatorType.Id);
        public static Locator Name(string name) => new Locator(By.Name(name), LocatorType.Name);
        public static Locator Css(string selector) => new Locator(By.CssSelector(selector), LocatorType.CssSelector);
        public static Locator XPath(string xpath) => new Locator(By.XPath(xpath), LocatorType.XPath);
        public static Locator ClassName(string className) => new Locator(By.ClassName(className), LocatorType.ClassName);
        public static Locator TagName(string tagName) => new Locator(By.TagName(tagName), LocatorType.TagName);
        public static Locator LinkText(string text) => new Locator(By.LinkText(text), LocatorType.LinkText);
        public static Locator PartialLinkText(string text) => new Locator(By.PartialLinkText(text), LocatorType.PartialLinkText);
    }
//todo add relative locators

}
