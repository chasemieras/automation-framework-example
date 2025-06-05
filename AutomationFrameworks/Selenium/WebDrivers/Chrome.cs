using System;
using AutomationFramework.Config;
using AutomationFramework.Exceptions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V135.Page;

namespace AutomationFramework.Selenium.WebDrivers
{
    /// <summary>
    /// Represents a Chrome web driver implementation for Selenium
    /// </summary>
    public class Chrome : IDriver
    {

        /// <summary>
        /// Generates the driver options for Chrome WebDriver
        /// </summary>
        public DriverOptions GenerateDriverOptions
        {
            get
            {
                ChromeOptions options = new();

                if (FrameworkConfiguration.Config.HeadlessMode)
                    options.AddArgument("--headless");

                if (FrameworkConfiguration.Config.ScreenSize is not null && FrameworkConfiguration.Config.ScreenSize.Type is not ScreenSize.SizeType.Desktop)
                {
                    DeviceOperatingSystem system;
                    if (new Random().Next(0, 2) == 0)
                        system = DeviceOperatingSystem.Android;
                    else
                        system = DeviceOperatingSystem.Apple;
                    EmulatedDevice device;

                    switch (FrameworkConfiguration.Config.ScreenSize.Type)
                    {
                        case ScreenSize.SizeType.Mobile:
                            device = EmulatedDevices.GetRandomDevice(system, DeviceType.Tablet);
                            FrameworkConfiguration.Config.MobileHeight = device.Height;
                            FrameworkConfiguration.Config.MobileWidth = device.Width;
                            break;
                        case ScreenSize.SizeType.Tablet:
                            device = EmulatedDevices.GetRandomDevice(system, DeviceType.Tablet);
                            FrameworkConfiguration.Config.TabletHeight = device.Height;
                            FrameworkConfiguration.Config.TabletWidth = device.Width;
                            break;
                        default:
                            throw new FrameworkConfigurationException($"Unsupported screen size type: {FrameworkConfiguration.Config.ScreenSize.Type}");
                    }

                    options.EnableMobileEmulation(device.Name);
                    FrameworkConfiguration.Config.EmulatedDevice = device;
                }

                return options;
            }
        }

        /// <summary>
        /// Generates a WebDriver instance that is Chrome based on the provided options.
        /// </summary>
        /// <param name="options">The <see cref="DriverOptions"/> that will be used in generating the <see cref="IWebDriver"/></param>
        /// <returns>An <see cref="IWebDriver"/></returns>
        public IWebDriver GenerateWebDriver(DriverOptions options) => new ChromeDriver(options as ChromeOptions);
    }
}
