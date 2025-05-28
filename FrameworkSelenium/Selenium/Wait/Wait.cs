using FrameworkSelenium.Selenium.Browser;
using FrameworkSelenium.Selenium.Elements;
using FrameworkSelenium.Selenium.Locator;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace FrameworkSelenium.Selenium.Wait
{
    public class Wait : IWait
    {
        private readonly Func<Action, TimeSpan, Exception?> _retry;
        private readonly TimeSpan _defaultTimeout;

        public Wait(TimeSpan? defaultTimeout = null)
        {
            _defaultTimeout = defaultTimeout ?? TimeSpan.FromSeconds(10);
        }

        public IElement UntilElementExists(IBrowser context, ILocator locator, TimeSpan? timeout = null)
        {
            var end = DateTime.UtcNow + (timeout ?? _defaultTimeout);
            Exception? lastError = null;

            while (DateTime.UtcNow < end)
            {
                try
                {
                    var element = context.GetElement(locator);
                    if (element != null)
                        return element;
                }
                catch (Exception ex)
                {
                    lastError = ex;
                }

                Thread.Sleep(500);
            }

            throw new TimeoutException($"Timed out waiting for element: {locator}", lastError);
        }

        public IElement UntilElementExists(IElement context, ILocator locator, TimeSpan? timeout = null)
        {
            var end = DateTime.UtcNow + (timeout ?? _defaultTimeout);
            Exception? lastError = null;

            while (DateTime.UtcNow < end)
            {
                try
                {
                    var element = context.GetElement(locator);
                    if (element != null)
                        return element;
                }
                catch (Exception ex)
                {
                    lastError = ex;
                }

                Thread.Sleep(500);
            }

            throw new TimeoutException($"Timed out waiting for nested element: {locator}", lastError);
        }

        public bool Until(IBrowser context, Func<IBrowser, bool> condition, TimeSpan? timeout = null)
        {
            var end = DateTime.UtcNow + (timeout ?? _defaultTimeout);
            while (DateTime.UtcNow < end)
            {
                if (condition(context))
                    return true;

                Thread.Sleep(500);
            }

            return false;
        }

        public bool Until(IElement context, Func<IElement, bool> condition, TimeSpan? timeout = null)
        {
            var end = DateTime.UtcNow + (timeout ?? _defaultTimeout);
            while (DateTime.UtcNow < end)
            {
                if (condition(context))
                    return true;

                Thread.Sleep(500);
            }

            return false;
        }
    }
}
