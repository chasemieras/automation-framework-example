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
            //driver type
            //environment type

            //tablet sizing
            //mobile sizing

            //timeout for elements
            //timeout for pages

            //headless mode

            JsonSerializerOptions options = new()
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }  // Enables string <-> enum mapping
            };

            const string envVarName = "FRAMEWORK_CONFIG";
            string pathToConfigJson = Environment.GetEnvironmentVariable(envVarName);
            if (string.IsNullOrEmpty(pathToConfigJson))
                throw new FrameworkConfigurationException($"No environment variable was found with the name: '{envVarName}'");

            Config config;
            try
            {
                string jsonString = File.ReadAllTextAsync(pathToConfigJson).GetAwaiter().GetResult();
                config = JsonSerializer.Deserialize<Config>(jsonString, options);
            }
            catch
            {
                throw;
            }

        }

        public BrowserType BrowserType { get; set; }

    }
}