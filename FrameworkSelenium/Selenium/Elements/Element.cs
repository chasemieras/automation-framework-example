using OpenQA.Selenium;
using System.Collections.Generic;

namespace FrameworkSelenium.Selenium.Elements
{
    public class Element(IWebElement element, IWebDriver driver) : IElement
    {
        private readonly IWebElement _element = element;
        private readonly IWebDriver _driver = driver;

        public IElement GetElement(ILocator locator)
            => new Element(_element.FindElement(locator.ToBy), _driver);

        public List<IElement> GetElements(ILocator locator)
        {
            System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> elements = _element.FindElements(locator.ToBy);
            List<IElement> list = new List<IElement>();
            foreach (IWebElement elem in elements)
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
