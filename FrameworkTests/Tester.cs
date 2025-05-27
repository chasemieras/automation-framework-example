using FrameworkSelenium.Config;
using FrameworkSelenium.Enums;
using FrameworkSelenium.Selenium.Browser;
using Xunit.Abstractions;

namespace FrameworkTests
{
    public class Tester
    {
        private IBrowser browser { get; set; }

        private readonly ITestOutputHelper _output;

        public Tester(ITestOutputHelper output)
        {
            _output = output;
        }


        [Fact]
        public void Test1()
        {
            string basePath = AppContext.BaseDirectory;
            string path = Path.Combine(basePath, @"..\..\..\config.json");
            string fullPath = Path.GetFullPath(path);
            Environment.SetEnvironmentVariable("FRAMEWORK_CONFIG", fullPath);
            
            _output.WriteLine($"{FrameworkConfiguration.Config.DriverType}\n{FrameworkConfiguration.Config.EnvironmentType}\n{FrameworkConfiguration.Config.DefaultElementTimeout}\n{FrameworkConfiguration.Config.DefaultPageTimeout}");
            browser = new Browser();

        }
    }
}