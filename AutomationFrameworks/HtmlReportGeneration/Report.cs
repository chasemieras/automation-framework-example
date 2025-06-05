using System;
using System.Text.RegularExpressions;
using System.Threading;
using AutomationFramework.Exceptions;
using AventStack.ExtentReports;
using Xunit;
using Xunit.Abstractions;

namespace AutomationFramework.HtmlReportGeneration
{
    /// <summary>
    /// Manages the current test being ran
    /// </summary>
    public abstract class Report : XunitContextBase
    {
        /// <summary>
        /// Ensures that the <see cref="BaseReport"/> has an HTML report attached to it
        /// </summary>
        /// <param name="output">An <see cref="ITestOutputHelper"/> for xunit logging</param>
        /// <param name="report">The HTML Report being referenced</param>
        public Report(ITestOutputHelper output, ReportGenerator report) : base(output)
        {
            BaseReport = report;
        }

        #region Create and manage tests to HTML report

        private static ReportGenerator BaseReport { get; set; }
        private static readonly ThreadLocal<ExtentTest> _currTest = new();
        private static readonly object _lock = new();

        /// <summary>
        /// Allows the user to interact with the current test being ran on the HTML report
        /// </summary>
        public ExtentTest HtmlReport
        {
            get
            {
                lock (_lock)
                {
                    if (_currTest.Value is null)
                        throw new HtmlReportException("Test node was not generated. Call GenerateTestNode() at the start of your test.");

                    return _currTest.Value;
                }
            }
            private set => _currTest.Value = value;
        }

        #endregion

        /// <summary>
        /// This is how the test's class will appear in the HTML report. 
        /// You can modify this in a test class so it has a prettier name.
        /// </summary>
        public virtual string TestClassName => Context.ClassName;

        /// <summary>
        /// Ensure that the current test has a node generated
        /// </summary>
        protected bool Generated { get; private set; }

        /// <summary>
        /// This should be ran at the start of every test to ensure a <see cref="ExtentTest.CreateNode(string, string)"/> is generated
        /// </summary>
        /// <param name="testName">The name of the test</param>
        /// <param name="authorName">The name of the author</param>
        /// <param name="description">The description of the test</param>
        /// <exception cref="FrameworkException">thrown if you call it more than once</exception>
        public void GenerateTestNode(string testName, string authorName, string description = "")
        {
            if (Generated)
                throw new FrameworkException("Test was already generated, do not use this method more than one time");

            string xunitTestName = Context.UniqueTestName;

            if (!testName.Equals(xunitTestName))
            {
                Match numberInTestName = new Regex(@"\d+", RegexOptions.Compiled).Match(testName);
                if (numberInTestName.Success)
                {
                    string numberFound = numberInTestName.Value;
                    if (numberFound.Length <= 2)
                    {
                        if (!testName.Equals("test"))
                            testName = $"{testName} | Test #{numberFound}";
                        else
                            testName += $" #{numberFound}";
                    }
                }
            }

            ExtentTest testSuite = BaseReport.GetOrCreateTest(TestClassName);
            HtmlReport = testSuite.CreateNode(testName, description).AssignAuthor(authorName);

            Generated = true;
        }

        /// <inheritdoc/>
        public override void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <inheritdoc/>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                BaseReport.Dispose();
        }
    }
}
