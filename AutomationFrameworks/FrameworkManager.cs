using System;
using System.Collections.Generic;
using System.Diagnostics;
using AutomationFramework.Config;
using AutomationFramework.Exceptions;
using AutomationFramework.Framework;
using AutomationFramework.HtmlReportGeneration;
using AutomationFramework.Selenium;
using Xunit.Abstractions;

namespace AutomationFramework
{
    /// <summary>
    /// Handles creating the <see cref="Browser"/> for testing and linking it with the HTML report
    /// </summary>
    public abstract class FrameworkManager : Report
    {
        /// <summary>
        /// The driver being used during testing
        /// </summary>
        protected IBrowser Browser { get; private set; }
        private readonly Stopwatch _sw;

        /// <summary>
        /// Ensures that the <see cref="Browser"/> is generated and attached to the test for interacting with the chosen driver
        /// </summary>
        /// <param name="output">An <see cref="ITestOutputHelper"/> for xunit logging</param>
        /// <param name="report">The HTML Report being referenced</param>
        public FrameworkManager(ITestOutputHelper output, ReportGenerator report) : base(output, report)
        {
            ConfigureTestRun();
            Browser = new Browser();
            _sw = Stopwatch.StartNew();
        }

        private void ConfigureTestRun()
        {
            List<string> attributes = Attributes;

            if (attributes.Count == 0)
            {
                FrameworkConfiguration.Config.ScreenSize = ScreenSize.Desktop;
                return;
            }

            // Set headless mode if "Headless" attribute is present
            if (attributes.Exists(a => a.Equals("Headless", StringComparison.OrdinalIgnoreCase)))
                FrameworkConfiguration.Config.HeadlessMode = true;

            // Check for both Mobile and Tablet attributes
            bool hasMobile = attributes.Exists(a => a.Equals("Mobile", StringComparison.OrdinalIgnoreCase));
            bool hasTablet = attributes.Exists(a => a.Equals("Tablet", StringComparison.OrdinalIgnoreCase));
            if (hasMobile && hasTablet)
                throw new InvalidOperationException("A test cannot have both [Mobile] and [Tablet] attributes.");

            // Set screen size if "Mobile" or "Tablet" attribute is present
            if (hasMobile)
                FrameworkConfiguration.Config.ScreenSize = ScreenSize.Mobile;
            else if (hasTablet)
                FrameworkConfiguration.Config.ScreenSize = ScreenSize.Tablet;
            else
                FrameworkConfiguration.Config.ScreenSize = ScreenSize.Desktop;
        }

        private List<string> Attributes
        {
            get
            {
                try
                {
                    return Context.Test.TestCase.Traits["Type"];
                }
                catch
                {
                    return [];
                }
            }
        }

        ///<inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            if (!disposing) return;

            _sw.Stop();
            try
            {
                bool error = false;
                if (!Generated)
                {
                    GenerateTestNode(Context.UniqueTestName, "No Author Assigned");
                    error = true;
                }
                HtmlReport.AssignDevice(); //todo Emulated Device + Screen Size
                HtmlReport.AssignDevice(); //todo Driver + OS

                Exception lastLoggedException = Context.TestException;

                if (lastLoggedException is not null)
                {
                    //todo add logging of exception
                }

                HtmlReport.Info($"<b>Test Duration:</b> {_sw.Elapsed}");

                if (error) throw new HtmlReportException("Test node was not generated. Call GenerateTestNode() at the start of your test.");
            }
            catch
            {
                throw;
            }
            finally
            {
                Browser.Dispose();
                Browser = null;

                base.Dispose(disposing);
            }
        }
    }
}
