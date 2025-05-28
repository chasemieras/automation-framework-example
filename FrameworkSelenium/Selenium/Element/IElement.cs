using FrameworkSelenium.Selenium.Locator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkSelenium.Selenium.Elements
{
    public interface IElement
    {
        IElement GetElement(ILocator locator);
        List<IElement> GetElements(ILocator locator);
        void Click();
        void SendKeys(string text);
        string Text { get; }
        string GetAttribute(string attribute);
    }
}
