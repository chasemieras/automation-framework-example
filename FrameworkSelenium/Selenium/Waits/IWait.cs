using FrameworkSelenium.Selenium.Elements;
using OpenQA.Selenium;
using System;

namespace FrameworkSelenium.Selenium.Waits
{
    public interface IWait
    {
                IElement UntilElementExists(IBrowser context, ILocator locator, TimeSpan timeout = default, int pollingInterval = 500);
        IElement UntilElementExists(IElement context, ILocator locator, TimeSpan timeout = default, int pollingInterval = 500);

        bool Until(IBrowser context, Func<IBrowser, bool> condition, TimeSpan timeout = default, int pollingInterval = 500);
        bool Until(IElement context, Func<IElement, bool> condition, TimeSpan timeout = default, int pollingInterval = 500);

        void UntilSuccessful(IBrowser context, Func<IElement, bool> condition, TimeSpan timeout = default, int pollingInterval = 500);
        void UntilSuccessful(IElement context, Func<IElement, bool> condition, TimeSpan timeout = default, int pollingInterval = 500);
    }
}
