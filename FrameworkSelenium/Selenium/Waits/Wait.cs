using FrameworkSelenium.Exceptions;
using FrameworkSelenium.Selenium.Browsers;
using FrameworkSelenium.Selenium.Elements;
using OpenQA.Selenium;
using System;
using System.Linq;
using System.Threading;

namespace FrameworkSelenium.Selenium.Waits
{
    /// <summary>
    /// Forces the code to gracefully stop and repeat easily
    /// </summary>
    public class Wait : IWait
    {
        //todo think of other cool waits to add
        //todo add the ability to ignore other exceptions?

        private readonly TimeSpan _defaultTimeout;
        private readonly int _defaultPollingInterval;

        private readonly Type[] exceptionsToIgnore = [typeof(ForceWaitLoop), typeof(MoveTargetOutOfBoundsException),
            typeof(StaleElementReferenceException), typeof(ElementClickInterceptedException), typeof(ElementNotInteractableException)];

        public Wait(TimeSpan defaultTimeout = default, int defaultPollingInterval = default)
        {
            _defaultTimeout = defaultTimeout == TimeSpan.Zero ? TimeSpan.FromSeconds(1) : defaultTimeout;
            _defaultPollingInterval = defaultPollingInterval == 0 ? 500 : defaultPollingInterval;
        }

        /// <inheritdoc/>
        public IElement UntilElementExists(IBrowser context, ILocator locator)
        {
            DateTime endTime = DateTime.Now + _defaultTimeout;
            Exception lastError = null;

            while (DateTime.Now < endTime)
            {
                try
                {
                    IElement element = context.GetElement(locator);
                    if (element != null)
                        return element;
                }
                catch (Exception ex)
                {
                    if (!ShouldExceptionBeIgnored(ex))
                        throw;

                    lastError = ex;
                }

                Thread.Sleep(_defaultPollingInterval);
            }

            throw new WaitTimeoutException(_defaultTimeout, lastError);
        }

        /// <inheritdoc/>
        public IElement UntilElementExists(IElement context, ILocator locator)
        {
            DateTime endTime = DateTime.Now + _defaultTimeout;
            Exception lastError = null;

            while (DateTime.Now < endTime)
            {
                try
                {
                    IElement element = context.GetElement(locator);
                    if (element != null)
                        return element;
                }
                catch (Exception ex)
                {
                    if (!ShouldExceptionBeIgnored(ex))
                        throw;

                    lastError = ex;
                }

                Thread.Sleep(_defaultPollingInterval);
            }

            throw new WaitTimeoutException(_defaultTimeout, lastError);
        }

        /// <inheritdoc/>
        public bool Until(IBrowser context, Func<IBrowser, bool> condition)
        {
            DateTime endTime = DateTime.Now + _defaultTimeout;
            while (DateTime.Now < endTime)
            {
                try
                {
                    if (condition(context))
                        return true;
                }
                catch (Exception ex)
                {
                    if (!ShouldExceptionBeIgnored(ex))
                        throw;
                }

                Thread.Sleep(_defaultPollingInterval);
            }

            return false;
        }

        /// <inheritdoc/>
        public bool Until(IElement context, Func<IElement, bool> condition)
        {
            DateTime endTime = DateTime.Now + _defaultTimeout;
            while (DateTime.Now < endTime)
            {
                try
                {
                    if (condition(context))
                        return true;
                }
                catch (Exception ex)
                {
                    if (!ShouldExceptionBeIgnored(ex))
                        throw;
                }

                Thread.Sleep(_defaultPollingInterval);
            }

            return false;
        }

        /// <inheritdoc/>
        public void UntilSuccessful(IBrowser context, Func<IBrowser, bool> condition)
        {
            DateTime endTime = DateTime.Now + _defaultTimeout;
            Exception lastError = null;

            while (DateTime.Now < endTime)
            {
                try
                {
                    if (condition(context))
                        return;
                }
                catch (Exception ex)
                {
                    if (!ShouldExceptionBeIgnored(ex))
                        throw;

                    lastError = ex;
                }

                Thread.Sleep(_defaultPollingInterval);
            }

            throw new WaitTimeoutException(_defaultTimeout, lastError);
        }

        /// <inheritdoc/>
        public void UntilSuccessful(IElement context, Func<IElement, bool> condition)
        {
            DateTime endTime = DateTime.Now + _defaultTimeout;
            Exception lastError = null;

            while (DateTime.Now < endTime)
            {
                try
                {
                    if (condition(context))
                        return;
                }
                catch (Exception ex)
                {
                    if (!ShouldExceptionBeIgnored(ex))
                        throw;

                    lastError = ex;
                }

                Thread.Sleep(_defaultPollingInterval);
            }

            throw new WaitTimeoutException(_defaultTimeout, lastError);
        }

        /// <summary>
        /// Checks if the given <see cref="Exception"/> is in <see cref="exceptionsToIgnore"/>
        /// </summary>
        /// <param name="ex">The potential <see cref="Exception"/> to ignore</param>
        /// <returns><b>True</b>: The <see cref="Exception"/> should be ignored | <b>False</b>: The <see cref="Exception"/> should be thrown</returns>
        private bool ShouldExceptionBeIgnored(Exception ex) =>
            exceptionsToIgnore.Any(type => type.IsInstanceOfType(ex));
    }
}
