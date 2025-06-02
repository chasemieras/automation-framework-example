using FrameworkSelenium.Exceptions;
using FrameworkSelenium.Selenium.Elements;
using FrameworkSelenium.Selenium.Locators;
using OpenQA.Selenium;
using System;
using System.Linq;
using System.Threading;

namespace FrameworkSelenium.Selenium.Waits
{
    /// <remarks>
    /// Creates a new <see cref="Wait{TContext}"/> instance with the given default timeout and polling interval.
    /// </remarks>
    /// <param name="defaultTimeout">the time that the wait will have before it errors</param>
    /// <param name="defaultPollingInterval">how often the wait will wait before repeating</param>
    /// <param name="context">the context that the wait will use to find elements</param>
    public class Wait<TContext>(TContext context, TimeSpan defaultTimeout = default, int defaultPollingInterval = default) : IWait<TContext> where TContext : IElementFinder
    {
        //todo think of other cool waits to add
        //todo add the ability to ignore other exceptions?

        private readonly TimeSpan _defaultTimeout = defaultTimeout == TimeSpan.Zero ? TimeSpan.FromSeconds(1) : defaultTimeout;
        private readonly int _defaultPollingInterval = defaultPollingInterval == 0 ? 500 : defaultPollingInterval;
        private readonly TContext _context = context;
        private readonly Type[] exceptionsToIgnore = [typeof(ForceWaitLoop), typeof(MoveTargetOutOfBoundsException),
            typeof(StaleElementReferenceException), typeof(ElementClickInterceptedException), typeof(ElementNotInteractableException)];

        /// <inheritdoc/>
        public IElement UntilElementExists(ILocator locator)
        {
            DateTime endTime = DateTime.Now + _defaultTimeout;
            Exception lastError = null;

            while (DateTime.Now < endTime)
            {
                try
                {
                    IElement element = _context.GetElement(locator);
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
        public bool Until(Func<TContext, bool> condition)
        {
            DateTime endTime = DateTime.Now + _defaultTimeout;
            while (DateTime.Now < endTime)
            {
                try
                {
                    if (condition(_context))
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
        public void UntilSuccessful(Func<TContext, bool> condition)
        {
            DateTime endTime = DateTime.Now + _defaultTimeout;
            Exception lastError = null;

            while (DateTime.Now < endTime)
            {
                try
                {
                    if (condition(_context))
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
