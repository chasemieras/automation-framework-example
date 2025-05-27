using FluentAssertions;
using FrameworkSelenium.Config;
using FrameworkSelenium.Exceptions;
using System.Reflection;
using System.Text.Json;
using Xunit.Abstractions;

namespace UnitTests
{
    public class FrameworkConfigurationUnitTests
    {
        private readonly ITestOutputHelper _output;

        public FrameworkConfigurationUnitTests(ITestOutputHelper output)
        {
            _output = output;
        }

        private FrameworkConfiguration ClearConfig(string path)
        {
            Type configType = typeof(FrameworkConfiguration);
            FieldInfo? threadLocalField = configType.GetField("_config", BindingFlags.Static | BindingFlags.NonPublic);

            ThreadLocal<FrameworkConfiguration>? threadLocal = threadLocalField.GetValue(null) as ThreadLocal<FrameworkConfiguration>;
            threadLocal.Value = null;

            Environment.SetEnvironmentVariable("FRAMEWORK_CONFIG", path);

            return FrameworkConfiguration.Config;
        }

        #region Positive Tests

        [Fact]
        public void TestThatEnvVarPasses()
        {
            string basePath = AppContext.BaseDirectory;
            string path = Path.Combine(basePath, @"..\..\..\Json\config.json");
            string fullPath = Path.GetFullPath(path);
            Environment.SetEnvironmentVariable("FRAMEWORK_CONFIG", fullPath);
            ClearConfig(fullPath).Should().NotBeNull();
        }

        #endregion

        #region Negative Tests

        [Fact]
        public void TestThatNoEnvVarFails()
        {
            Assert.Throws<FrameworkConfigurationException>(() => ClearConfig("").DriverType);
        }

        [Fact]
        public void TestThatBadPathFails()
        {
            Assert.Throws<DirectoryNotFoundException>(() => ClearConfig("bad/path/here"));
        }

        [Fact]
        public void TestThatBadJsonFails()
        {
            string basePath = AppContext.BaseDirectory;
            string path = Path.Combine(basePath, @"..\..\..\Json\bad.json");
            string fullPath = Path.GetFullPath(path);
            Assert.Throws<JsonException>(() => ClearConfig(fullPath));
        }

        [Fact]
        public void TestThatBadConfigFails()
        {
            string basePath = AppContext.BaseDirectory;
            string path = Path.Combine(basePath, @"..\..\..\Json\badConfig.json");
            string fullPath = Path.GetFullPath(path);
            Assert.Throws<JsonException>(() => ClearConfig(fullPath));
        }

        #endregion

    }
}
