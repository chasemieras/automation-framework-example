using FrameworkSelenium.Selenium.Browsers;
using FrameworkSelenium.Selenium.Elements;
using System;
using System.Threading;

namespace FrameworkSelenium.Selenium.Waits
{
    public class Wait : IWait
    {
        private readonly Func<Action, TimeSpan, Exception?> _retry;
        private readonly TimeSpan _defaultTimeout;
        private readonly int _defaultPollingInterval;

        public Wait(TimeSpan defaultTimeout = default, int defaultPollingInterval = default)
        {
            _defaultTimeout = defaultTimeout ?? TimeSpan.FromSeconds(1);
            _defaultPollingInterval = defaultPollingInterval ?? 500;
        }

        //todo think of other cool waits to add

        public IElement UntilElementExists(IBrowser context, ILocator locator, 
        TimeSpan timeout = default, int pollingInterval = 500)
        {
            var end = DateTime.Now + (timeout ?? _defaultTimeout);
            Exception? lastError = null;

            while (DateTime.Now < end)
            {
                try
                {
                    var element = context.FindElement(locator);
                    if (element != null)
                        return element;
                }
                catch (Exception ex)
                {
                    lastError = ex;
                }

                Thread.Sleep(pollingInterval);
            }

            throw new WaitTimeoutException($"Timed out waiting for element: {locator}", lastError);
        }

        public IElement UntilElementExists(IElement context, ILocator locator, 
        TimeSpan timeout = default, int pollingInterval = 500)
        {
            var end = DateTime.Now + (timeout ?? _defaultTimeout);
            Exception? lastError = null;

            while (DateTime.Now < end)
            {
                try
                {
                    var element = context.FindElement(locator);
                    if (element != null)
                        return element;
                }
                catch (Exception ex)
                {
                    lastError = ex;
                }

                Thread.Sleep(pollingInterval);
            }

            throw new WaitTimeoutException($"Timed out waiting for nested element: {locator}", lastError);
        }

        public bool Until(IBrowser context, Func<IBrowser, bool> condition, 
        TimeSpan timeout = default, int pollingInterval = 500)
        {
            var end = DateTime.Now + (timeout ?? _defaultTimeout);
            while (DateTime.Now < end)
            {
                if (condition(context))
                    return true;

                Thread.Sleep(pollingInterval);
            }

            return false;
        }

        public bool Until(IElement context, Func<IElement, bool> condition, 
        TimeSpan timeout = default, int pollingInterval = 500)
        {
            var end = DateTime.Now + (timeout ?? _defaultTimeout);
            while (DateTime.Now < end)
            {
                if (condition(context))
                    return true;

                Thread.Sleep(pollingInterval);
            }

            return false;
        }

        public void UntilSuccessful(IBrowser context, Func<IElement, bool> condition, 
            TimeSpan timeout = default, int pollingInterval = 500)
        {
            var end = DateTime.Now + (timeout == default ? _defaultTimeout : timeout);
            Exception? lastError = null;

            while (DateTime.Now < end)
            {
                try
                {
                    if (condition(context))
                        return;
                }
                catch (Exception ex)
                {
                    lastError = ex;
                }

                Thread.Sleep(pollingInterval);
            }

            throw new WaitTimeoutException("Timed out waiting for condition to succeed without error.", lastError);
        }

        public void UntilSuccessful(IElement context, Func<IElement, bool> condition, 
            TimeSpan timeout = default, int pollingInterval = 500)
        {
            var end = DateTime.Now + (timeout == default ? _defaultTimeout : timeout);
            Exception? lastError = null;

            while (DateTime.Now < end)
            {
                try
                {
                    if (condition(context))
                        return;
                }
                catch (Exception ex)
                {
                    lastError = ex;
                }

                Thread.Sleep(pollingInterval);
            }

            throw new WaitTimeoutException("Timed out waiting for condition to succeed without error.", lastError);
        }
    }
}
