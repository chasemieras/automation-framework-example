using FrameworkSelenium.Selenium.Elements;
using OpenQA.Selenium;
using System;

namespace FrameworkSelenium.Selenium.Waits
{
    public interface IWait
    {
        IElement UntilElementExists(ILocator locator, TimeSpan? timeout = null);
        bool Until(Func<IWebDriver, bool> condition, TimeSpan? timeout = null);
    }
}
