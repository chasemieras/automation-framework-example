using FrameworkSelenium.Config;
using FrameworkSelenium.Enums;
using FrameworkSelenium.Selenium.Drivers;
using OpenQA.Selenium;
using System;

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

        public void Dispose()
        {
            Dispose();
        }
    }
}
