using System;
using System.IO;

namespace AutomationFramework
{
    /// <summary>
    /// Class for methods that are useful during testing
    /// </summary>
    public static class Helper
    {

        /// <summary>
        /// Sets the framework configuration by looking for a configuration file in the current directory or its parent directories.
        /// </summary>
        /// <param name="configFileName">the name of the json file that is used for testing, defaulting to the name config.json</param>
        /// <returns></returns>
        public static void SetFrameworkConfiguration(string configFileName = "config.json")
        {
            string projectRoot = Directory.GetCurrentDirectory();
            string configPath = Path.Combine(projectRoot, configFileName);

            if (!File.Exists(configPath))
            {
                string parent = Directory.GetParent(projectRoot)?.FullName;
                int attempts = 0;
                while (parent != null && !File.Exists(Path.Combine(parent, configFileName)) && attempts < 3)
                {
                    string jsonFolder = Path.Combine(parent, "json");
                    string JsonFolder = Path.Combine(parent, "Json");
                    string jsonConfig = Path.Combine(jsonFolder, configFileName);
                    string JsonConfig = Path.Combine(JsonFolder, configFileName);

                    if (Directory.Exists(jsonFolder) && File.Exists(jsonConfig))
                    {
                        configPath = jsonConfig;
                        break;
                    }
                    if (Directory.Exists(JsonFolder) && File.Exists(JsonConfig))
                    {
                        configPath = JsonConfig;
                        break;
                    }

                    parent = Directory.GetParent(parent)?.FullName;
                    attempts++;
                }
                if (parent != null && File.Exists(Path.Combine(parent, configFileName)))
                    configPath = Path.Combine(parent, configFileName);
            }

            Environment.SetEnvironmentVariable("FRAMEWORK_CONFIG", configPath);
        }
    }
}
