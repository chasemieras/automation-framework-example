using FrameworkSelenium.Selenium.Elements;
using FrameworkSelenium.Selenium.Locator;
using System.Collections.Generic;

namespace FrameworkSelenium.Selenium.Browser
{
    public interface IBrowser : IElementFinder
    {
        #region Variables

        string Title { get; }
        string CurrentUrl { get; }
        string PageSource { get; }

        #endregion

        #region Navigation

        void Navigate(string url);

        void Back();

        void Forward();

        void Refresh();

        #endregion

        #region Alert Interaction

        IAlert SwitchToAlert();

        void AcceptAlert();

        void DismissAlert();

        /// <summary>
        /// Checks if an alert is present.
        /// </summary>
        bool IsPresent();

        #endregion

        #region iFrame Handling

        void SwitchToFrame(IElement frame);

        void SwitchToFrame(int indexOfFrame);

        void SwitchToDefaultContent();

        #endregion

        #region Cookie Handling

        void AddCookie(string name, string value);

        Cookie GetCookie(string name);

        void DeleteCookie(string name);

        void DeleteAllCookies();

        System.Collections.ObjectModel.ReadOnlyCollection<OpenQA.Selenium.Cookie> GetAllCookies();

        #endregion

        #region Window + Tab Interaction

        string GetCurrentWindowHandle();

        System.Collections.ObjectModel.ReadOnlyCollection<string> GetAllWindowHandles();

        void CloseCurrentWindow();

        void SwitchToWindow(string windowHandle);

        void SwitchToNewWindow();

        void SwitchToNewTab();

        void Quit();

        #endregion
        
    }
}
