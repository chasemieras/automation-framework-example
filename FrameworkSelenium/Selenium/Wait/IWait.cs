using FrameworkSelenium.Selenium.Elements;
using FrameworkSelenium.Selenium.Locator;
using OpenQA.Selenium;
using System;

namespace FrameworkSelenium.Selenium.Wait
{
    public interface IWait
    {
        IElement UntilElementExists(ILocator locator, TimeSpan? timeout = null);
        bool Until(Func<IWebDriver, bool> condition, TimeSpan? timeout = null);
    }
}
