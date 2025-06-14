﻿using AutomationFramework.Config;
using AutomationFramework.Enums;
using AutomationFramework.Exceptions;
using AutomationFramework.Framework;
using AutomationFramework.Framework.Waits;
using AutomationFramework.Selenium.Locators;
using AutomationFramework.Selenium.WebDrivers;
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
    /// Generates a driver to be used during testing
    /// </summary>
    public class Browser : IBrowser
    {
        private readonly IWebDriver _driver;
        private readonly DriverType _driverType;

        /// <summary>
        /// Creates a <see cref="IBrowser"/>, which spins up a <see cref="IWebDriver"/> instance
        /// </summary>
        internal Browser()
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

        /// <inheritdoc />
        public string DriverType => _driverType.ToString();

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
        public Framework.IAlert SwitchToAlert => new Alert(_driver.SwitchTo().Alert());

        /// <inheritdoc />
        public void AcceptAlert() => _driver.SwitchTo().Alert().Accept();

        /// <inheritdoc />
        public void DismissAlert() => _driver.SwitchTo().Alert().Dismiss();

        /// <inheritdoc />
        public bool IsAlertPresent
        {
            get
            {
                try
                {
                    _driver.SwitchTo().Alert();
                    return true;
                }
                catch (NoAlertPresentException)
                {
                    return false;
                }
            }
        }

        #endregion

        #region iFrame Handling

        /// <inheritdoc />
        public void SwitchToFrame(ILocator locatorForFrame)
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
        public ReadOnlyCollection<Cookie> GetAllCookies => _driver.Manage().Cookies.AllCookies;

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

        /// <inheritdoc />
        public string GetCurrentWindowHandle => _driver.CurrentWindowHandle;

        /// <inheritdoc />
        public ReadOnlyCollection<string> GetAllWindowHandles => _driver.WindowHandles;

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

        /// <inheritdoc />
        public void SendKeys(string keys) =>
            ActionBuilder.SendKeys(keys).Build().Perform();

        /// <inheritdoc />
        public string ScreenShot =>
            ((ITakesScreenshot)_driver).GetScreenshot().AsBase64EncodedString;

        /// <inheritdoc />
        public void Dispose() => Quit();

        #endregion

        #region Element Interaction

        /// <inheritdoc />
        public IElement GetElement(ILocator locator)
        {
            ReadOnlyCollection<IWebElement> elements = _driver.FindElements(locator.ToBy);

            if (elements.Count > 1)
                throw new TooManyElementsException(elements.Count, locator);
            else if (elements.Count == 0)
                throw new NoElementsException(locator);

            return new Element(elements.First(), _driver);
        }

        /// <inheritdoc />
        public List<IElement> GetElements(ILocator locator)
        {
            ReadOnlyCollection<IWebElement> elements = _driver.FindElements(locator.ToBy);

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
            Wait<IBrowser> wait = new(this, defaultTimeout == TimeSpan.Zero ? TimeSpan.FromSeconds(1) : defaultTimeout);
            IElement result = wait.UntilElementExists(locator);
            if (checkIfInteractable)
                return result.IsInteractable;

            return true;
        }

        /// <inheritdoc />
        public bool ElementsExist(ILocator locator, bool checkIfInteractable = true, TimeSpan defaultTimeout = default)
        {
            Wait<IBrowser> wait = new(this, defaultTimeout == TimeSpan.Zero ? TimeSpan.FromSeconds(1) : defaultTimeout);
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
