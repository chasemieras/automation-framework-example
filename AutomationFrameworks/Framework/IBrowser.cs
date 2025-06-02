using AutomationFramework.Selenium;
using AutomationFramework.Selenium.Locators;
using OpenQA.Selenium;
using System;
using System.Collections.ObjectModel;
using System.Drawing;

namespace AutomationFramework.Framework
{
    /// <summary>
    /// The interface for the WebDriver wrapper
    /// </summary>
    public interface IBrowser : IElementFinder, IDisposable
    {
        #region Variables

        /// <summary>
        /// Gets the title of the current page
        /// </summary>
        string Title { get; }

        /// <summary>
        /// Gets the url of the current page
        /// </summary>
        string CurrentUrl { get; }

        /// <summary>
        /// Gets the page source of the current page
        /// </summary>
        string PageSource { get; }

        /// <summary>
        /// Gets the current driver type running
        /// </summary>
        string DriverType { get; }

        #endregion

        #region Navigation

        /// <summary>
        /// Takes the browser to the given URL
        /// </summary>
        /// <param name="url">the website you want to test</param>
        void Navigate(string url);

        /// <summary>
        /// Moves the page backwards from past interactions
        /// </summary>
        void Back();

        /// <summary>
        /// Moves the page forward from past interactions
        /// </summary>
        void Forward();

        /// <summary>
        /// Refreshes the browser
        /// </summary>
        void Refresh();

        #endregion

        #region Alert Interaction

        /// <summary>
        ///  Switches the driver to the alert
        /// </summary>
        /// <returns>an <see cref="IAlert"/></returns>
        IAlert SwitchToAlert { get; }

        /// <summary>
        /// Automatically accepts the alert on the page
        /// </summary>
        void AcceptAlert();

        /// <summary>
        /// Automatically dismisses the alert on the page
        /// </summary>
        void DismissAlert();

        /// <summary>
        /// Checks if an alert is present
        /// </summary>
        bool IsAlertPresent { get; }

        #endregion

        #region iFrame Handling

        /// <summary>
        /// Switches to the frame using the given locator
        /// </summary>
        /// <param name="locatorForFrame">Locator to the iFrame desired</param>
        void SwitchToFrame(ILocator locatorForFrame);

        /// <summary>
        /// Switches to the frame using the given frame index
        /// </summary>
        /// <param name="indexOfFrame">the index of the frame you want to switch to</param>
        void SwitchToFrame(int indexOfFrame);

        /// <summary>
        /// Sets the browser back to the original content
        /// </summary>
        void SwitchToDefaultContent();

        #endregion

        #region Cookie Handling

        /// <summary>
        /// Adds a new <see cref="Cookie"/> with the given <paramref name="name"/> and <paramref name="value"/>
        /// </summary>
        /// <param name="name">the name of the cookie you want to add</param>
        /// <param name="value">the value of the cookie you want to add</param>
        void AddCookie(string name, string value);

        /// <summary>
        /// Gets a <see cref="Cookie"/> based on the <paramref name="name"/> given
        /// </summary>
        /// <param name="name">the name of the cookie you want to get</param>
        /// <returns>A <see cref="Cookie"/></returns>
        Cookie GetCookie(string name);

        /// <summary>
        /// Deletes a <see cref="Cookie"/> based on the <paramref name="name"/> given
        /// </summary>
        /// <param name="name">the name of the cookie you want to get</param>
        void DeleteCookie(string name);

        /// <summary>
        /// Deletes all <see cref="Cookie"/>s
        /// </summary>
        void DeleteAllCookies();

        /// <summary>
        /// Gets all <see cref="Cookie"/>s
        /// </summary>
        /// <returns>A ReadOnlyCollection of <see cref="Cookie"/>s</returns>
        ReadOnlyCollection<Cookie> GetAllCookies { get; }

        /// <summary>
        /// Checks that the given <see cref="Cookie"/> <paramref name="name"/> is present
        /// </summary>
        /// <returns><b>True</b>: The <see cref="Cookie"/> is present | <b>False</b>: The <see cref="Cookie"/> is not present</returns>
        bool DoesCookieExist(string name);

        #endregion

        #region Window + Tab Interaction

        /// <summary>
        /// The <see cref="ScreenSize"/> of the browser
        /// </summary>
        ScreenSize ScreenSize { get; }

        /// <summary>
        /// The <see cref="Size"/> of the current window
        /// </summary>
        Size WindowSize { get; }

        /// <summary>
        /// Gets the current window handle
        /// </summary>
        /// <returns>A string that is the current window handle</returns>
        string GetCurrentWindowHandle { get; }

        /// <summary>
        /// Gets all of the window handles
        /// </summary>
        /// <returns>A ReadOnlyCollection of string that are the window handles</returns>
        ReadOnlyCollection<string> GetAllWindowHandles { get; }

        /// <summary>
        /// Closes the current tab, or window if there is only one tab
        /// </summary>
        void CloseCurrentWindow();

        /// <summary>
        /// Switches to a window with the given <paramref name="windowHandle"/>
        /// </summary>
        /// <param name="windowHandle">The name of the window you want to go to</param>
        void SwitchToWindow(string windowHandle);

        /// <summary>
        /// Opens and navigates to a new window
        /// </summary>
        void SwitchToNewWindow();

        /// <summary>
        /// Opens and navigates to a new tab
        /// </summary>
        void SwitchToNewTab();

        /// <summary>
        /// Calls to <see cref="Selenium.WebDrivers.DriverHelper"/> to quit the current browser running
        /// </summary>
        void Quit();

        #endregion

        #region Scrolling

        /// <summary>
        /// Scrolls to the bottom of the page
        /// </summary>
        void ScrollToBottom();

        /// <summary>
        /// Scrolls to the top of the page
        /// </summary>
        void ScrollToTop();

        #endregion

        #region JavaScript

        /// <summary>
        /// Runs the given JavaScript
        /// </summary>
        /// <param name="javaScriptToRun">JS to run</param>
        void ExecuteJavaScript(string javaScriptToRun);

        /// <summary>
        /// Runs JavaScript that returns something
        /// </summary>
        /// <param name="javaScriptToRun">JS to run</param>
        /// <returns>something</returns>
        object ExecuteJavaScriptThatReturns(string javaScriptToRun);

        #endregion

        #region Other Methods

        /// <summary>
        /// Sends given keys to the <see cref="Browser"/>
        /// </summary>
        /// <param name="keys">specific text or <see cref="Keys"/></param>
        void SendKeys(string keys);
            
        #endregion

    }
}
