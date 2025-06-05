using AutomationFramework;
using AutomationFramework.Config;
using FluentAssertions;
using System.Reflection;
using System.Text.Json;
using Xunit.Abstractions;

namespace UnitTests.Framework
{
    public class FrameworkConfigurationUnitTests(ITestOutputHelper output)
    {
        private readonly ITestOutputHelper _output = output;

        private static FrameworkConfiguration ClearConfig(string path)
        {
            Type configType = typeof(FrameworkConfiguration);
            FieldInfo? threadLocalField = configType.GetField("_config", BindingFlags.Static | BindingFlags.NonPublic);

#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            ThreadLocal<FrameworkConfiguration>? threadLocal = threadLocalField.GetValue(null) as ThreadLocal<FrameworkConfiguration>;
            threadLocal.Value = null;
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            Helper.SetFrameworkConfiguration(path);

            return FrameworkConfiguration.Config;
        }

        #region Positive Tests

        [Fact]
        public void TestThatEnvVarPasses()
        {
            Helper.SetFrameworkConfiguration("config.json");
            FrameworkConfiguration.Config.Should().NotBeNull();
        }

        #endregion

        #region Negative Tests

        [Fact]
        public void TestThatNoEnvVarFails() =>
            Assert.Throws<UnauthorizedAccessException>(() => ClearConfig("").DriverType);
        

        [Fact]
        public void TestThatBadPathFails() =>
            Assert.Throws<DirectoryNotFoundException>(() => ClearConfig("bad/path/here"));
        

        [Fact]
        public void TestThatBadJsonFails() =>
            Assert.Throws<JsonException>(() => ClearConfig("bad.json"));
        

        [Fact]
        public void TestThatBadConfigFails() =>
            Assert.Throws<JsonException>(() => ClearConfig("badConfig.json"));

        #endregion

    }
}
