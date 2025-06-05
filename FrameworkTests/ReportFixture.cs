using AutomationFramework;
using AutomationFramework.HtmlReportGeneration;

namespace FrameworkTests
{
    public class ReportFixture : ReportGenerator
    {
        public ReportFixture()
        {
            // Before all code goes here
            Helper.SetFrameworkConfiguration();

        }

        public override void Dispose()
        {
            // After all or cleanup code goes here

            // DO NOT REMOVE
            base.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}