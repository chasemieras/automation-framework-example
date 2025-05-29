using FrameworkSelenium.Enums;
using FrameworkSelenium.Exceptions;
using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;

namespace FrameworkSelenium.Config
{
    public class FrameworkConfiguration
    {
        private static readonly ThreadLocal<FrameworkConfiguration> _config = new();
        private static readonly object _lock = new();

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
            DefaultElementTimeout = TimeSpan.FromSeconds(config.DefaultElementTimeout);
            DefaultPageTimeout = TimeSpan.FromSeconds(config.DefaultPageTimeout);
            HeadlessMode = false;

            DesktopHeight = config.DesktopHeight;
            DesktopWidth = config.DesktopWidth;
            MobileHeight = config.MobileHeight;
            MobileWidth = config.MobileWidth;
            TabletHeight = config.TabletHeight;
            TabletWidth = config.TabletWidth;
        }

        public DriverType DriverType { get; set; }
        public EnvironmentType EnvironmentType { get; set; }
        public TimeSpan DefaultElementTimeout { get; set; }
        public TimeSpan DefaultPageTimeout{ get; set; }
        public ScreenSize ScreenSize { get; set; }
        public bool HeadlessMode { get; set; }

        public int DesktopHeight { get; }
        public int DesktopWidth { get; }
        public int MobileHeight { get; }
        public int MobileWidth { get; }
        public int TabletHeight { get; }
        public int TabletWidth { get; }
    }
}