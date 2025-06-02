using AutomationFramework.Config;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;

namespace AutomationFramework.Selenium.WebDrivers
{
    /// <summary>
    /// Represents a Edge web driver implementation for Selenium
    /// </summary>
    public class Edge : IDriver
    {

        /// <summary>
        /// Generates the driver options for Edge WebDriver
        /// </summary>
        public DriverOptions GenerateDriverOptions
        {
            get
            {
                EdgeOptions options = new();

                if (FrameworkConfiguration.Config.HeadlessMode)
                    options.AddArgument("--headless");

                //switch (FrameworkConfiguration.Config.ScreenSize.Type)
                //{
                //    case ScreenSize.SizeType.Mobile:
                //        options.EnableMobileEmulation();
                //        break;
                //    case ScreenSize.SizeType.Tablet:
                //        options.EnableMobileEmulation();
                //        break;
                //}

                return options;
            }
        }

        /// <summary>
        /// Generates a WebDriver instance that is Edge based on the provided options.
        /// </summary>
        /// <param name="options">The <see cref="DriverOptions"/> that will be used in generating the <see cref="IWebDriver"/></param>
        /// <returns>An <see cref="IWebDriver"/></returns>
        public IWebDriver GenerateWebDriver(DriverOptions options) => new EdgeDriver(options as EdgeOptions);
    }
}
