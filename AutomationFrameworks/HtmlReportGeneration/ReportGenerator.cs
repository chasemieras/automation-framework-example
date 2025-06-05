using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Config;
using Xunit;

namespace AutomationFramework.HtmlReportGeneration
{
    /// <summary>
    /// Creates an <see cref="ExtentSparkReporter"/> to use during testing
    /// </summary>
    public class ReportGenerator : IDisposable
    {
        private static ExtentReports _extentReports;
        private static readonly object _lock = new();

        /// <summary>
        /// This just makes it so we can grab the exceptions during testing
        /// </summary>
        protected ReportGenerator() =>
            XunitContext.EnableExceptionCapture();

        private static ExtentReports HtmlReport
        {
            get
            {
                lock (_lock)
                {
                    _extentReports ??= ConfigureHtmlReport();
                    return _extentReports;
                }
            }
        }

        private static readonly ConcurrentDictionary<string, ExtentTest> _tests = new();
        private static readonly ThreadLocal<ExtentTest> _currentTest = new();

        /// <summary>
        /// Generates a test based on <paramref name="className"/> if it does not exist yet
        /// </summary>
        /// <param name="className">the class name that contains the test being ran</param>
        /// <returns>An <see cref="ExtentTest"/></returns>
        public ExtentTest GetOrCreateTest(string className)
        {
            ExtentTest test = _tests.GetOrAdd(className, key =>
            {
                return HtmlReport.CreateTest(key);
            });

            _currentTest.Value = test;
            return test;
        }
        
        /// <summary>
        /// Configures the Html Report to what we need for testing
        /// </summary>
        private static ExtentReports ConfigureHtmlReport()
        {
            string resultsPath = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName,
                                                "results");
            if (!Directory.Exists(resultsPath))
                Directory.CreateDirectory(resultsPath);

            string reportName;
            string fileName = "index";
            try
            {
                reportName = Environment.GetEnvironmentVariable("REPORT_NAME");
            }
            catch
            {
                reportName = "Local Testing";
            }

            if (!string.IsNullOrEmpty(reportName))
                fileName = reportName;

            //reportName = reportName.Replace('_', ' ');

            ExtentReports extentReports = new();
            ExtentSparkReporter htmlReport = new($"{resultsPath}/{fileName}.html")
            {
                Config =
                {
                    DocumentTitle = reportName,
                    ReportName = fileName,
                    Theme = Theme.Dark,
                    TimelineEnabled = true
                }
            };

            extentReports.AttachReporter(htmlReport);
            return extentReports;
        }

        /// <inheritdoc/>
        public virtual void Dispose()
        {
            _extentReports.Flush();
            GC.SuppressFinalize(this);
        }

    }
}
