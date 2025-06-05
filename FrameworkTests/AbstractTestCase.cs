using AutomationFramework;
using AutomationFramework.HtmlReportGeneration;
using Xunit.Abstractions;

namespace FrameworkTests
{
    public class AbstractTestCase(ITestOutputHelper output, ReportGenerator report) : FrameworkManager(output, report)
    {
        //todo make a config
        //todo make an index
    }
}