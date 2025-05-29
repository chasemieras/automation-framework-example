using OpenQA.Selenium;
using System.Collections.Generic;
using System.Drawing;

namespace FrameworkSelenium.Selenium.Elements
{
    public class Element(IWebElement element, IWebDriver driver) : IElement
    {
        private readonly IWebElement _element = element;
        private readonly IWebDriver _driver = driver;

		//todo look for NotImplementedException and implement them!

		public bool IsDisplayed => throw new System.NotImplementedException();

        public bool IsEnabled => throw new System.NotImplementedException();

        public bool IsInteractable => throw new System.NotImplementedException();

        public bool IsSelected => throw new System.NotImplementedException();

        public Point Location => throw new System.NotImplementedException();

        public string Src => throw new System.NotImplementedException();

        public string Href => throw new System.NotImplementedException();

        public string Value => throw new System.NotImplementedException();

        public string ClassName => throw new System.NotImplementedException();

        public string Id => throw new System.NotImplementedException();

        public string Name => throw new System.NotImplementedException();

        public string Title => throw new System.NotImplementedException();

        public string InnerHtml => throw new System.NotImplementedException();

        public string OuterHtml => throw new System.NotImplementedException();

        public string Target => throw new System.NotImplementedException();

        public string Style => throw new System.NotImplementedException();

        public string AltText => throw new System.NotImplementedException();

        public string TagName => throw new System.NotImplementedException();

        public string Class => throw new System.NotImplementedException();

        public string Rel => throw new System.NotImplementedException();

        public bool Checked => throw new System.NotImplementedException();

        public string Styling => throw new System.NotImplementedException();

        public string Text => throw new System.NotImplementedException();

        public string ExtractString => throw new System.NotImplementedException();

        public bool AttributeExists(string attribute, bool checkIfEmpty = true)
        {
            throw new System.NotImplementedException();
        }

        public void Clear()
        {
            throw new System.NotImplementedException();
        }

        public void Click()
        {
            throw new System.NotImplementedException();
        }

        public bool ElementExist(ILocator locator, bool checkIfInteractable)
        {
            throw new System.NotImplementedException();
        }

        public bool ElementsExist(ILocator locator, bool checkIfInteractable)
        {
            throw new System.NotImplementedException();
        }

        public void ExtractText()
        {
            throw new System.NotImplementedException();
        }

        public string GetAttribute(string attribute)
        {
            throw new System.NotImplementedException();
        }

        public IElement GetElement(ILocator locator)
        {
            throw new System.NotImplementedException();
        }

        public List<IElement> GetElements(ILocator locator)
        {
            throw new System.NotImplementedException();
        }

        public string GetPseudoelement()
        {
            throw new System.NotImplementedException();
        }

        public void RightClick()
        {
            throw new System.NotImplementedException();
        }

        public void ScrollToElement()
        {
            throw new System.NotImplementedException();
        }

        public void SendEnter()
        {
            throw new System.NotImplementedException();
        }

        public void SendKeys(string text)
        {
            throw new System.NotImplementedException();
        }

        public void Submit()
        {
            throw new System.NotImplementedException();
        }
    }
}
