using FrameworkSelenium.Selenium.Locator;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkSelenium.Selenium.Elements
{
    public class Element : IElement
    {
        private IWebElement _element;
        private IWebDriver _driver;

        public Element(IWebElement element, IWebDriver driver)
        {
            _element = element;
            _driver = driver;
        }

        public IElement GetElement(ILocator locator)
            => new Element(_element.FindElement(locator.ToBy()), _driver);

        public List<IElement> GetElements(ILocator locator)
        {
            var elements = _element.FindElements(locator.ToBy());
            var list = new List<IElement>();
            foreach (var elem in elements)
                list.Add(new Element(elem, _driver));
            return list;
        }

        public void Click()
        {
            _element.Click();
        }

        public void SendKeys(string text)
        {
            _element.SendKeys(text);
        }

        public string Text => _element.Text;

        public string GetAttribute(string attribute)
        {
            return _element.GetAttribute(attribute);
        }
    }
}
