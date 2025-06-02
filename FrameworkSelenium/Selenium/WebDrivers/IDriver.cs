using OpenQA.Selenium;

namespace FrameworkSelenium.Selenium.WebDrivers
{
    /// <summary>
    /// Interface for WebDriver implementations.
    /// </summary>
    public interface IDriver
    {
        /// <summary>
        /// Used to generate predetermined parts of the webdriver that will be ran
        /// </summary>
        public abstract DriverOptions GenerateDriverOptions { get; }

        /// <summary>
        /// Generates a WebDriver instance based on the provided options.
        /// </summary>
        /// <param name="options">The driver options that will be used during WebDriver generation</param>
        /// <returns>An <see cref="IWebDriver"/></returns>
        public abstract IWebDriver GenerateWebDriver(DriverOptions options);

    }
}
