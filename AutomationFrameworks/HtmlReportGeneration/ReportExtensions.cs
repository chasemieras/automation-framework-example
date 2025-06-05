using System;
using AutomationFramework.Framework;
using AventStack.ExtentReports;
using OpenQA.Selenium;

namespace AutomationFramework.HtmlReportGeneration
{

    /// <summary>
    /// Custom methods for <see cref="ExtentTest"/>
    /// </summary>
    public static class ReportExtensions
    {
        /// <summary>
        /// Logs an <see cref="Status.Info"/> with a screenshot
        /// </summary>
        /// <param name="report">the current HTML report test</param>
        /// <param name="browser">The current <see cref="IBrowser"/>/driver being used</param>
        /// <param name="details">The text you want to display with the screenshot</param>
        /// <returns>the current <see cref="ExtentTest"/></returns>
        public static ExtentTest InfoWithScreenShot(this ExtentTest report, IBrowser browser, string details) =>
            report.LogWithScreenShot(Status.Info, browser, details);

        /// <summary>
        /// Logs an <see cref="Status.Pass"/> with a screenshot
        /// </summary>
        /// <param name="report">the current HTML report test</param>
        /// <param name="browser">The current <see cref="IBrowser"/>/driver being used</param>
        /// <param name="details">The text you want to display with the screenshot</param>
        /// <returns>the current <see cref="ExtentTest"/></returns>
        public static ExtentTest PassWithScreenShot(this ExtentTest report, IBrowser browser, string details) =>
            report.LogWithScreenShot(Status.Pass, browser, details);

        /// <summary>
        /// Logs an <see cref="Status.Fail"/> with a screenshot
        /// </summary>
        /// <param name="report">the current HTML report test</param>
        /// <param name="browser">The current <see cref="IBrowser"/>/driver being used</param>
        /// <param name="details">The text you want to display with the screenshot</param>
        /// <returns>the current <see cref="ExtentTest"/></returns>
        public static ExtentTest FailWithScreenShot(this ExtentTest report, IBrowser browser, string details) =>
            report.LogWithScreenShot(Status.Fail, browser, details);

        /// <summary>
        /// Logs an <see cref="Status.Warning"/> with a screenshot
        /// </summary>
        /// <param name="report">the current HTML report test</param>
        /// <param name="browser">The current <see cref="IBrowser"/>/driver being used</param>
        /// <param name="details">The text you want to display with the screenshot</param>
        /// <returns>the current <see cref="ExtentTest"/></returns>
        public static ExtentTest WarningWithScreenShot(this ExtentTest report, IBrowser browser, string details) =>
            report.LogWithScreenShot(Status.Warning, browser, details);

        /// <summary>
        /// Logs an <see cref="Status.Skip"/> with a screenshot
        /// </summary>
        /// <param name="report">the current HTML report test</param>
        /// <param name="browser">The current <see cref="IBrowser"/>/driver being used</param>
        /// <param name="details">The text you want to display with the screenshot</param>
        /// <returns>the current <see cref="ExtentTest"/></returns>
        public static ExtentTest SkipWithScreenShot(this ExtentTest report, IBrowser browser, string details) =>
            report.LogWithScreenShot(Status.Skip, browser, details);

        /// <summary>
        /// Logs an <see cref="Status.Error"/> with a screenshot
        /// </summary>
        /// <param name="report">the current HTML report test</param>
        /// <param name="browser">The current <see cref="IBrowser"/>/driver being used</param>
        /// <param name="details">The text you want to display with the screenshot</param>
        /// <returns>the current <see cref="ExtentTest"/></returns>
        public static ExtentTest ErrorWithScreenShot(this ExtentTest report, IBrowser browser, string details) =>
            report.LogWithScreenShot(Status.Error, browser, details);

        /// <summary>
        /// Logs an <paramref name="status"/> with a screenshot
        /// </summary>
        /// <param name="report">the current HTML report test</param>
        /// <param name="status">The <see cref="Status"/> you want this logged as</param>
        /// <param name="browser">The current <see cref="IBrowser"/>/driver being used</param>
        /// <param name="details">The text you want to display with the screenshot</param>
        /// <returns>the current <see cref="ExtentTest"/></returns>
        private static ExtentTest LogWithScreenShot(this ExtentTest report, Status status, IBrowser browser, string details) =>
            report.Log(status, $"{details} <br />", MediaEntityBuilder.CreateScreenCaptureFromBase64String(browser.ScreenShot).Build());

        /// <summary>
        /// Logs an <see cref="Status.Fail"/> with exception details and a screenshot.
        /// </summary>
        /// <param name="report">The current HTML report test</param>
        /// <param name="browser">The current <see cref="IBrowser"/>/driver being used</param>
        /// <param name="exception">The exception to log</param>
        /// <returns>The current <see cref="ExtentTest"/></returns>
        public static ExtentTest ExceptionWithScreenShot(this ExtentTest report, IBrowser browser, Exception exception)
        {
            string details = $"<b>An Exception Occurred!</b><br /><pre>{exception}</pre>";

            report.AssignCategory(exception.GetType().ToString());

            bool isSpecificSeleniumError = exception is WebDriverException or WebDriverTimeoutException or
            NoSuchWindowException or NoAlertPresentException;

            if (!isSpecificSeleniumError)
            {
                string currentUrl = browser.CurrentUrl;
                if (!(currentUrl.Equals("data:,") || currentUrl.EndsWith("blanktab.html")))
                {
                    string title = browser.Title;
                    if (string.IsNullOrEmpty(title))
                        title = "Title was empty";
                    details += $"<hr /> Erroring page: <a href = '{currentUrl}' target='_blank'>{title}</a>";
                }

                return report.LogWithScreenShot(Status.Fail, browser, details);
            }
            else return report.Log(Status.Fail, details);
        }

    }
}
