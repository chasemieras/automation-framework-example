using FrameworkSelenium.Config;
using FrameworkSelenium.Enums;
using FrameworkSelenium.Selenium.Drivers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkSelenium.Selenium.Browser
{
    public class Browser
    {

        private readonly IWebDriver _driver;
        private readonly DriverType _driverType;

        internal Browser() 
        {
            Environment.SetEnvironmentVariable("SE_AVOID_STATS", "true");
            Environment.SetEnvironmentVariable("SE_CLEAR_CACHE", "true");
            Environment.SetEnvironmentVariable("SE_LANGUAGE_BINDING", "DotNet");

            _driverType = FrameworkConfiguration.Config.DriverType;

            _driver = DriverHelper.GetDriver(_driverType);

        }

    }
}
