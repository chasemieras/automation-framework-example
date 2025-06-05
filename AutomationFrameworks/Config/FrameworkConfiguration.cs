using AutomationFramework.Enums;
using AutomationFramework.Exceptions;
using System;
using System.IO;
using System.Text.Json;
using System.Threading;

namespace AutomationFramework.Config
{
    /// <summary>
    /// Sets up important variables that will be used during testing
    /// </summary>
    public class FrameworkConfiguration
    {
        private static readonly ThreadLocal<FrameworkConfiguration> _config = new();
        private static readonly object _lock = new();

        /// <summary>
        /// A Singleton instance of the <see cref="FrameworkConfiguration"/> class that
        /// provides access to the configuration variables
        /// </summary>
        public static FrameworkConfiguration Config
        {
            get
            {
                lock (_lock)
                {
                    _config.Value ??= new FrameworkConfiguration();

                    return _config.Value;
                }
            }
        }

        private FrameworkConfiguration()
        {
            const string envVarName = "FRAMEWORK_CONFIG";
            string pathToConfigJson = Environment.GetEnvironmentVariable(envVarName);
            if (string.IsNullOrEmpty(pathToConfigJson))
                throw new FrameworkConfigurationException($"No environment variable was found with the name: '{envVarName}'");

            Config config;
            try
            {
                string jsonString = File.ReadAllTextAsync(pathToConfigJson).GetAwaiter().GetResult();
                config = JsonSerializer.Deserialize<Config>(jsonString, JsonSettings.FrameworkConfigSettings);
            }
            catch
            {
                throw;
            }

            DriverType = config.DriverType;
            EnvironmentType = config.EnvironmentType;
            if (EnvironmentType is not EnvironmentType.Local)
                HeadlessMode = true;
            else
                HeadlessMode = false;

            DefaultElementTimeout = TimeSpan.FromSeconds(config.DefaultElementTimeout);
            DefaultPageTimeout = TimeSpan.FromSeconds(config.DefaultPageTimeout);
            EmulatedDevice = null;

            DesktopHeight = config.DesktopHeight;
            DesktopWidth = config.DesktopWidth;
            MobileHeight = config.MobileHeight;
            MobileWidth = config.MobileWidth;
            TabletHeight = config.TabletHeight;
            TabletWidth = config.TabletWidth;
        }

        /// <summary>
        /// The type of driver that is used during testing
        /// </summary>
        public DriverType DriverType { get; }

        /// <summary>
        /// The type of environment that the tests are run in
        /// </summary>
        public EnvironmentType EnvironmentType { get; }

        /// <summary>
        /// The default timeout for elements in seconds
        /// </summary>
        public TimeSpan DefaultElementTimeout { get; }

        /// <summary>
        /// The default timeout for pages in seconds
        /// </summary>
        public TimeSpan DefaultPageTimeout { get; }

        /// <summary>
        /// The <see cref="ScreenSize"/> that the tests will run in
        /// </summary>
        public ScreenSize ScreenSize { get; set; }

        /// <summary>
        /// Determines if the tests will run in headless mode
        /// </summary>
        public bool HeadlessMode { get; set; }

        /// <summary>
        /// The emulated device that will be used during testing
        /// </summary>
        public EmulatedDevice EmulatedDevice { get; set;  }

        /// <summary>
        /// The height of desktop mode
        /// </summary>
        public int DesktopHeight { get; }

        /// <summary>
        /// The width of desktop mode
        /// </summary>
        public int DesktopWidth { get; }

        /// <summary>
        /// The height of the mobile screen size in pixels
        /// </summary>
        public int MobileHeight { get; set; }

        /// <summary>
        /// The width of the mobile screen size in pixels
        /// </summary>
        public int MobileWidth { get; set; }

        /// <summary>
        /// The height of the tablet screen size in pixels
        /// </summary>
        public int TabletHeight { get; set; }

        /// <summary>
        /// The width of the tablet screen size in pixels
        /// </summary>
        public int TabletWidth { get; set; }
    }
}