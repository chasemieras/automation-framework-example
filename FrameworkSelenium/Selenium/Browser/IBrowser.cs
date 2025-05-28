using FrameworkSelenium.Selenium.Elements;
using FrameworkSelenium.Selenium.Locator;
using System.Collections.Generic;

namespace FrameworkSelenium.Selenium.Browser
{
    public interface IBrowser
    {
        void Navigate(string url);
        IElement GetElement(ILocator locator);
        List<IElement> GetElements(ILocator locator);
        void Quit();
    }
}
