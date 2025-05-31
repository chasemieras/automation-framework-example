using FrameworkSelenium.Config;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;

namespace FrameworkSelenium.Selenium.WebDrivers
{
    public class Edge : IDriver
    {

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

        public IWebDriver GenerateWebDriver(DriverOptions options) => new EdgeDriver(options as EdgeOptions);
    }
}
