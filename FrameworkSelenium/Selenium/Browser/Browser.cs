using FrameworkSelenium.Config;
using FrameworkSelenium.Enums;
using FrameworkSelenium.Selenium.Drivers;
using FrameworkSelenium.Selenium.Elements;
using FrameworkSelenium.Selenium.Locator;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace FrameworkSelenium.Selenium.Browser
{
    public class Browser : IBrowser, IDisposable
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

            //_driver.Manage().Window.Size = new System.Drawing.Size();
            //Environment.SetEnvironmentVariable("SE_SCREEN_HEIGHT", $"");
            //Environment.SetEnvironmentVariable("SE_SCREEN_WIDTH", $"");
            _driver.Manage().Timeouts().PageLoad = FrameworkConfiguration.Config.DefaultPageTimeout;
            //_driver.Quit();
            
        }

        public IElement GetElement(ILocator locator)
        {
            //todo add check if there is more than 1, 0
            //todo add scroll
            return new Element(_driver.FindElement(locator.ToBy()), _driver);
        }

        public List<IElement> GetElements(ILocator locator)
        {
            // todo add check if there is 0
            //todo add scroll
            var elements = _driver.FindElements(locator.ToBy());
            var list = new List<IElement>();
            foreach (var elem in elements)
            {
                list.Add(new Element(elem, _driver));
            }
            return list;
        }

        public void Navigate(string url) => _driver.Navigate().GoToUrl(url);

        public void Quit() => DriverHelper.QuitDriver();
        
    }
}
