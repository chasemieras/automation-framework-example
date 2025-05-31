using FrameworkSelenium.Config;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace FrameworkSelenium.Selenium.WebDrivers
{
    public class Chrome : IDriver
    {

        public  DriverOptions GenerateDriverOptions
        {
            get 
            {
                ChromeOptions options = new();

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

        public IWebDriver GenerateWebDriver(DriverOptions options) => new ChromeDriver(options as ChromeOptions);
    }
}
