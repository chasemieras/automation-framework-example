using AutomationFramework.Exceptions;
using AutomationFramework.Framework;
using AutomationFramework.Framework.Waits;
using AutomationFramework.Selenium.Locators;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;

namespace AutomationFramework.Selenium
{
    /// <summary>
    /// Generates an element to use during testing
    /// </summary>
    /// <param name="element">the <see cref="IWebElement"/> that is going to be used</param>
    /// <param name="driver">the current <see cref="IWebDriver"/>/<see cref="IBrowser"/> that is being used</param>
    public class Element(IWebElement element, IWebDriver driver) : IElement
    {
        private readonly IWebElement _element = element;
        private readonly IWebDriver _driver = driver;

        //todo look for NotImplementedException and implement them!

        #region Variables

        private Actions ActionBuilder => new(_driver);
        private IJavaScriptExecutor JavaScriptExecutor => (IJavaScriptExecutor)_driver;

        /// <inheritdoc/>
        public bool IsDisplayed => _element.Displayed;

        /// <inheritdoc/>
        public bool IsEnabled => _element.Enabled;

        /// <inheritdoc/>
        public bool IsInteractable => IsDisplayed && IsEnabled;

        /// <inheritdoc/>
        public bool IsSelected => _element.Selected;

        /// <inheritdoc/>
        public Point Location => _element.Location;

        #endregion

        #region Click Interactions

        /// <inheritdoc/>
        public void Click() => _element.Click();

        /// <inheritdoc/>
        public void RightClick() =>
            ActionBuilder.ContextClick(_element).Build().Perform();

        #endregion

        #region Attributes

        /// <inheritdoc/>
        public string GetAttribute(string attribute) =>
            _element.GetAttribute(attribute);

        /// <inheritdoc/>    
        public bool AttributeExists(string attribute) =>
            !string.IsNullOrEmpty(GetAttribute(attribute));

        /// <inheritdoc/>
        public string Src => GetAttribute("src");

        /// <inheritdoc/>
        public string Href => GetAttribute("href");

        /// <inheritdoc/>
        public string Value => GetAttribute("value");

        /// <inheritdoc/>
        public string ClassName => GetAttribute("class");

        /// <inheritdoc/>
        public string Id => GetAttribute("id");

        /// <inheritdoc/>
        public string Name => GetAttribute("name");

        /// <inheritdoc/>
        public string Title => GetAttribute("title");

        /// <inheritdoc/>
        public string InnerHtml => _element.GetAttribute("innerHTML");

        /// <inheritdoc/>
        public string OuterHtml => _element.GetAttribute("outerHTML");

        /// <inheritdoc/>
        public string Target => GetAttribute("target");

        /// <inheritdoc/>
        public string AltText => GetAttribute("alt");

        /// <inheritdoc/>
        public string TagName => _element.TagName;

        /// <inheritdoc/>
        public string Class => GetAttribute("class");

        /// <inheritdoc/>
        public string Rel => GetAttribute("rel");

        /// <inheritdoc/>
        public bool Checked => bool.Parse(GetAttribute("checked"));

        #endregion

        #region Styling

        /// <inheritdoc/>
        public string Styling => GetAttribute("style");

        /// <inheritdoc/>
        public string GetCssValue(string propertyName) =>
            _element.GetCssValue(propertyName);

        #endregion

        #region Text Interaction

        /// <inheritdoc/>
        public string Text => _element.Text;

        /// <inheritdoc/>
        public string ExtractString
        {
            get
            {
                if (!string.IsNullOrEmpty(Text)) return Text;
                if (!string.IsNullOrEmpty(Title)) return Title;
                if (!string.IsNullOrEmpty(AltText)) return AltText;
                if (!string.IsNullOrEmpty(Name)) return Name;
                if (!string.IsNullOrEmpty(Value)) return Value;
                return string.Empty;
            }
        }

        /// <inheritdoc/>
        public void SendKeys(string text) =>
            _element.SendKeys(text);

        /// <inheritdoc/>
        public void SendEnter() =>
            _element.SendKeys(Keys.Enter);

        /// <inheritdoc/>
        public void Clear() =>
            _element.Clear();

        /// <inheritdoc/>
        public void Submit() =>
            _element.Submit();

        #endregion

        #region Other Methods

        /// <inheritdoc/>
        public void ScrollToElement() =>
            JavaScriptExecutor.ExecuteScript("arguments[0].scrollIntoView(true);", _element);

        /// <inheritdoc/>
        public string GetPseudoelement(string pseudoElement = "before", string property = "content") =>
            JavaScriptExecutor.ExecuteScript(@$"var element = arguments[0]; 
            var pseudo = window.getComputedStyle(element, '::{pseudoElement}'); 
            return pseudo.getPropertyValue('{property}');", _element).ToString();

        #endregion

        #region Element Interaction

        /// <inheritdoc />
        public IElement GetElement(ILocator locator)
        {
            if (locator.Type is Enums.LocatorType.XPath)
                throw new LocatorMisuseException("XPaths are not supported when looking for elements within an element. Please use a different locator type.");

            ReadOnlyCollection<IWebElement> elements = _element.FindElements(locator.ToBy);

            if (elements.Count > 1)
                throw new TooManyElementsException(elements.Count, locator);
            else if (elements.Count == 0)
                throw new NoElementsException(locator);

            return new Element(elements.First(), _driver);
        }

        /// <inheritdoc />
        public List<IElement> GetElements(ILocator locator)
        {
            if (locator.Type is Enums.LocatorType.XPath)
                throw new LocatorMisuseException("XPaths are not supported when looking for elements within an element. Please use a different locator type.");

            ReadOnlyCollection<IWebElement> elements = _element.FindElements(locator.ToBy);

            if (elements.Count == 0)
                throw new NoElementsException(locator);

            List<IElement> list = [];
            foreach (IWebElement elem in elements)
                list.Add(new Element(elem, _driver));
            
            return list;
        }

        /// <inheritdoc />
        public bool ElementExist(ILocator locator, bool checkIfInteractable = true, TimeSpan defaultTimeout = default)
        {
            if (locator.Type is Enums.LocatorType.XPath)
                throw new LocatorMisuseException("XPaths are not supported when looking for elements within an element. Please use a different locator type.");

            Wait<IElement> wait = new(this, defaultTimeout == TimeSpan.Zero ? TimeSpan.FromSeconds(1) : defaultTimeout);
            IElement result = wait.UntilElementExists(locator);
            if (checkIfInteractable)
                return result.IsInteractable;

            return true;
        }

        /// <inheritdoc />
        public bool ElementsExist(ILocator locator, bool checkIfInteractable = true, TimeSpan defaultTimeout = default)
        {
            if (locator.Type is Enums.LocatorType.XPath)
                throw new LocatorMisuseException("XPaths are not supported when looking for elements within an element. Please use a different locator type.");

            Wait<IElement> wait = new(this, defaultTimeout == TimeSpan.Zero ? TimeSpan.FromSeconds(1) : defaultTimeout);
            bool result = false;
            wait.UntilSuccessful(x =>
            {
                List<IElement> elements = x.GetElements(locator);
                if (elements.Count == 0)
                    return result;

                if (elements.Any(x => x.IsInteractable == false))
                    return result;

                result = true;
                return result;
            });
            
            return result;
        }

        #endregion

    }
}
