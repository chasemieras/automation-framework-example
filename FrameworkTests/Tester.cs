using AutomationFramework;
using AutomationFramework.Attributes;
using AutomationFramework.Config;
using Xunit.Abstractions;

namespace FrameworkTests
{
    public class Tester(ITestOutputHelper output, ReportFixture report) : AbstractTestCase(output, report), IClassFixture<ReportFixture>
    {
        public override string TestClassName => "egg";

        //todo add docker compose of grid
        //todo look at Relative Locators
        //todo make it so browser type can change per run if wanted

        [Mobile]
        [Fact]
        public void Test1()
        {
            GenerateTestNode("test", "test");
            HtmlReport.Info($"{FrameworkConfiguration.Config.ScreenSize.Type}<br>{Browser.WindowSize.Width}x{Browser.WindowSize.Height}<br>{Browser.ScreenSize.Type}");
        }
    }
}