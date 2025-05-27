using FrameworkSelenium.Config;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace FrameworkSelenium.Selenium.Drivers
{
    public class Firefox : InterfaceDriver
    {

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

        public IWebDriver GenerateWebDriver(DriverOptions options) => new FirefoxDriver(options as FirefoxOptions);
    }
}
