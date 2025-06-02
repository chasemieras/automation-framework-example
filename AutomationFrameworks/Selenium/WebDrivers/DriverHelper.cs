using AutomationFramework.Enums;
using AutomationFramework.Exceptions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using System.Threading;

namespace AutomationFramework.Selenium.WebDrivers
{
    /// <summary>
    /// Helper class to manage the WebDriver instances
    /// </summary>
    public static class DriverHelper
    {
        private static readonly string[] defaultOptions =
        [
            "--allow-insecure-localhost",
            "--enable-automation",
            "--disable-gpu",
            "--no-sandbox",
            "--disable-dev-shm-usage",
            "--disable-extensions",
            "--disable-infobars",
            "--disable-popup-blocking",
            "--incognito",
            "--disable-translate",
            "--remote-allow-origins=*",
            "--mute-audio",
            "--no-experiments",
            "--no-first-run",
            "--ignore-ssl-errors",
            "--ignore-certificate-errors",
            "--ignore-certificate-errors-spki-list",
            "--dns-prefetch-disable",
            "--disable-prompt-on-repost",
            "--disable-sync",
            "--disable-default-apps",
            "--disable-browser-side-navigation",
            "--disable-notifications"
        ];

        private static readonly ThreadLocal<IWebDriver> _driverThreadLocal = new();
        private static readonly object _lock = new();

        /// <summary>
        /// Gets the WebDriver instance for the specified <see cref="DriverType"/>.
        /// </summary>
        /// <param name="driverType">The <see cref="DriverType"/> that will be used</param>
        /// <returns>An <see cref="IWebDriver"/></returns>
        /// <exception cref="FrameworkException">Thrown if a <see cref="DriverType"/> that isn't accounted for</exception>
        public static IWebDriver GetDriver(DriverType driverType)
        {
            lock (_lock)
            {
                if (_driverThreadLocal.Value == null)
                {
                    IDriver driver;
                    DriverOptions options;
                    switch (driverType)
                    {
                        case DriverType.Chrome:
                            driver = new Chrome();
                            options = driver.GenerateDriverOptions;
                            ((ChromeOptions)options).AddArguments(defaultOptions);
                            break;
                        case DriverType.Edge:
                            driver = new Edge();
                            options = driver.GenerateDriverOptions;
                            ((EdgeOptions)options).AddArguments(defaultOptions);
                            break;
                        case DriverType.Firefox:
                            driver = new Edge();
                            options = driver.GenerateDriverOptions;
                            break;
                        default:
                            throw new FrameworkException($"Driver type '{driverType}' not supported");
                    }

                    _driverThreadLocal.Value = driver.GenerateWebDriver(options);
                }

                return _driverThreadLocal.Value;
            }
        }

        /// <summary>
        /// Quits the WebDriver instance for the current thread, if it exists.
        /// </summary>
        public static void QuitDriver()
        {
            lock (_lock)
            {
                if (_driverThreadLocal.Value == null) return;

                _driverThreadLocal.Value.Quit();
                _driverThreadLocal.Value = null;
            }
        }
    }
}
