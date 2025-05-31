using OpenQA.Selenium;

namespace FrameworkSelenium.Selenium.WebDrivers
{
    public interface IDriver
    {
        public abstract DriverOptions GenerateDriverOptions { get; }
        public abstract IWebDriver GenerateWebDriver(DriverOptions options);

    }
}
