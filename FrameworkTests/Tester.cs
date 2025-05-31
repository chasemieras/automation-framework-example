using FrameworkSelenium.Config;
using FrameworkSelenium.Selenium.Browsers;
using Xunit.Abstractions;

namespace FrameworkTests
{
    public class Tester(ITestOutputHelper output)
    {
        private IBrowser Browser { get; set; }

        private readonly ITestOutputHelper _output = output;

        [Fact]
        public void Test1()
        {
            string basePath = AppContext.BaseDirectory;
            string path = Path.Combine(basePath, @"..\..\..\config.json"); //TODO make this work with VS Code
            string fullPath = Path.GetFullPath(path);
            Environment.SetEnvironmentVariable("FRAMEWORK_CONFIG", fullPath);
            
            _output.WriteLine($"{FrameworkConfiguration.Config.DriverType}\n{FrameworkConfiguration.Config.EnvironmentType}\n{FrameworkConfiguration.Config.DefaultElementTimeout}\n{FrameworkConfiguration.Config.DefaultPageTimeout}");
            Browser = new Browser();
            _output.WriteLine($"{Browser.CurrentUrl}");
        }
    }
}