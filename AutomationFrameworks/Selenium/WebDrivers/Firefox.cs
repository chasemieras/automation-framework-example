using AutomationFramework.Config;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace AutomationFramework.Selenium.WebDrivers
{
    /// <summary>
    /// Represents a Firefox web driver implementation for Selenium
    /// </summary>
    public class Firefox : IDriver
    {

        /// <summary>
        /// Generates the driver options for Firefox WebDriver
        /// </summary>
        public DriverOptions GenerateDriverOptions
        {
            get
            {
                FirefoxOptions options = new();

                if (FrameworkConfiguration.Config.HeadlessMode)
                    options.AddArgument("--headless");

                options.AddArguments("--private");
                options.SetPreference("dom.webnotifications.enabled", false);

                //switch (FrameworkConfiguration.Config.ScreenSize.Type)
                //{
                //    case ScreenSize.SizeType.Mobile:
                //        options.em();
                //        break;
                //    case ScreenSize.SizeType.Tablet:
                //        options.EnableMobileEmulation();
                //        break;
                //}

                return options;
            }
        }

        /// <summary>
        /// Generates a WebDriver instance that is Firefox based on the provided options
        /// </summary>
        /// <param name="options">The <see cref="DriverOptions"/> that will be used in generating the <see cref="IWebDriver"/></param>
        /// <returns>An <see cref="IWebDriver"/></returns>
        public IWebDriver GenerateWebDriver(DriverOptions options) => new FirefoxDriver(options as FirefoxOptions);
    }
}
