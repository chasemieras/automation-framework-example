using FrameworkSelenium.Config;
using FrameworkSelenium.Enums;
using FrameworkSelenium.Exceptions;
using FrameworkSelenium.Selenium.Alerts;
using FrameworkSelenium.Selenium.Elements;
using FrameworkSelenium.Selenium.Locators;
using FrameworkSelenium.Selenium.WebDrivers;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace FrameworkSelenium.Selenium.Browsers
{
    /// <summary>
    /// Generates a driver to be used during testing
    /// </summary>
    public class Browser : IBrowser
    {
        private readonly IWebDriver _driver;
        private readonly DriverType _driverType;

        public Browser() 
        {
            Environment.SetEnvironmentVariable("SE_AVOID_STATS", "true");
            Environment.SetEnvironmentVariable("SE_CLEAR_CACHE", "true");
            Environment.SetEnvironmentVariable("SE_LANGUAGE_BINDING", "DotNet");

            _driverType = FrameworkConfiguration.Config.DriverType;

            _driver = DriverHelper.GetDriver(_driverType);

            if (FrameworkConfiguration.Config.ScreenSize == null)
                ScreenSize = ScreenSize.Desktop;
            else
                ScreenSize = FrameworkConfiguration.Config.ScreenSize;

            //todo make attributes that set the size
            //todo look at Relative Locators
            //todo look at waits
            //todo look at IElement
            //todo find vars

            _driver.Manage().Window.Size = new Size(ScreenSize.Width, ScreenSize.Height);

            Environment.SetEnvironmentVariable("SE_SCREEN_WIDTH", $"{ScreenSize.Width}");
            Environment.SetEnvironmentVariable("SE_SCREEN_HEIGHT", $"{ScreenSize.Height}");
            _driver.Manage().Timeouts().PageLoad = FrameworkConfiguration.Config.DefaultPageTimeout;
        }

        #region Variables

        /// <inheritdoc />
        public string Title => _driver.Title;

        /// <inheritdoc />
        public string CurrentUrl => _driver.Url;

        /// <inheritdoc />
        public string PageSource => _driver.PageSource;

        private Actions ActionBuilder => new(_driver);
        private IJavaScriptExecutor JavaScriptExecutor => (IJavaScriptExecutor)_driver;

        #endregion

        #region Navigation

        /// <inheritdoc />
        public void Navigate(string url) => _driver.Navigate().GoToUrl(url);

        /// <inheritdoc />
        public void Back() => _driver.Navigate().Back();

        /// <inheritdoc />
        public void Forward() => _driver.Navigate().Forward();

        /// <inheritdoc />
        public void Refresh() => _driver.Navigate().Refresh();

        #endregion

        #region Alert Interaction

        /// <inheritdoc />
        public Alerts.IAlert SwitchToAlert() => new Alert(_driver.SwitchTo().Alert());

        /// <inheritdoc />
        public void AcceptAlert() => _driver.SwitchTo().Alert().Accept();

        /// <inheritdoc />
        public void DismissAlert() => _driver.SwitchTo().Alert().Dismiss();

        /// <inheritdoc />
        public bool IsAlertPresent()
        {
            try
            {
                _driver.SwitchTo().Alert();
                SwitchToDefaultContent();
                return true;
            }
            catch 
            {
                return false;
            }
        }

        #endregion

        #region iFrame Handling

        /// <inheritdoc />
        public void SwitchToFrame(Locator locatorForFrame)
        {
            if (locatorForFrame.Type is LocatorType.LinkText or LocatorType.Name or LocatorType.PartialLinkText)
                throw new LocatorMisuseException($"The locator {locatorForFrame.ToBy} is of type '{locatorForFrame.Type}', which cannot be used by iFrames");

            _driver.SwitchTo().Frame(_driver.FindElement(locatorForFrame.ToBy));
        }

        /// <inheritdoc />
        public void SwitchToFrame(int indexOfFrame) => _driver.SwitchTo().Frame(indexOfFrame);

        /// <inheritdoc />
        public void SwitchToDefaultContent() => _driver.SwitchTo().DefaultContent();

        #endregion

        #region Cookie Handling

        /// <inheritdoc />
        public void AddCookie(string name, string value) => _driver.Manage().Cookies.AddCookie(new Cookie(name, value));

        /// <inheritdoc />
        public Cookie GetCookie(string name) => _driver.Manage().Cookies.GetCookieNamed(name);

        /// <inheritdoc />
        public void DeleteCookie(string name) => _driver.Manage().Cookies.DeleteCookieNamed(name);

        /// <inheritdoc />
        public void DeleteAllCookies() => _driver.Manage().Cookies.DeleteAllCookies();

        /// <inheritdoc />
        public ReadOnlyCollection<Cookie> GetAllCookies() => _driver.Manage().Cookies.AllCookies;

        /// <inheritdoc />
        public bool DoesCookieExist(string name)
        {
            try
            {
                GetCookie(name);
                return true;
            }
            catch 
            { 
                return false;
            }
        }

        #endregion

        #region Window + Tab Interaction

        /// <inheritdoc />
        public ScreenSize ScreenSize { get; }

        /// <inheritdoc />
        public Size WindowSize => _driver.Manage().Window.Size;

        public string DriverType => throw new NotImplementedException();

        /// <inheritdoc />
        public string GetCurrentWindowHandle() => _driver.CurrentWindowHandle;

        /// <inheritdoc />
        public ReadOnlyCollection<string> GetAllWindowHandles() => _driver.WindowHandles;

        /// <inheritdoc />
        public void CloseCurrentWindow() => _driver.Close();

        /// <inheritdoc />
        public void SwitchToWindow(string windowHandle) => _driver.SwitchTo().Window(windowHandle);

        /// <inheritdoc />
        public void SwitchToNewWindow() => _driver.SwitchTo().NewWindow(WindowType.Window);

        /// <inheritdoc />
        public void SwitchToNewTab() => _driver.SwitchTo().NewWindow(WindowType.Tab);

        /// <inheritdoc />
        public void Quit() => DriverHelper.QuitDriver();

        #endregion

        #region Scrolling

        /// <inheritdoc />
        public void ScrollToBottom() => PerformScroll(Keys.End);

        /// <inheritdoc />
        public void ScrollToTop() => PerformScroll(Keys.Home);

        /// <summary>
        /// Used by <see cref="ScrollToTop"/> and <see cref="ScrollToBottom"/> to scroll
        /// </summary>
        /// <param name="key">The <see cref="Keys"/> that does a scroll action</param>
        private void PerformScroll(string key) 
        {
            ActionBuilder.SendKeys(key).Build().Perform();
            System.Threading.Thread.Sleep(500);
        }

        #endregion

        #region JavaScript

        /// <inheritdoc />
        public void ExecuteJavaScript(string javaScriptToRun) => JavaScriptExecutor.ExecuteScript(javaScriptToRun);

        /// <inheritdoc />
        public object ExecuteJavaScriptThatReturns(string javaScriptToRun) => JavaScriptExecutor.ExecuteScript(javaScriptToRun);

        #endregion

        #region Other Methods

        public void SendKeys(string keys)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            Dispose();
        }

        #endregion

        #region Element Interaction

        public IElement GetElement(ILocator locator)
        {
            //todo add check if there is more than 1, 0
            var e = _driver.FindElements(locator.ToBy);
            if (e.Count > 1)
                throw new TooManyElementsException($"{}");
            else if (e.Count == 0)
                throw new NoElementsException($"No elements were found with the given locator:\nType: {locator.Type}\nValue: {locator.Value}");
            return new Element(, _driver);
        }

        public List<IElement> GetElements(ILocator locator)
        {
            // todo add check if there is 0
            ReadOnlyCollection<IWebElement> elements = _driver.FindElements(locator.ToBy());
            List<IElement> list = [];
            foreach (IWebElement elem in elements)
                list.Add(new Element(elem, _driver));
            
            return list;
        }

        public bool ElementExist(ILocator locator)
        {
            throw new NotImplementedException();
        }

        public bool ElementsExist(ILocator locator)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
