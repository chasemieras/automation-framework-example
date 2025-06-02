using AutomationFramework.Framework;

namespace AutomationFramework.Selenium
{
    /// <summary>
    /// Object to handle wrapping around <see cref="OpenQA.Selenium.IAlert"/>.
    /// </summary>
    public class Alert(OpenQA.Selenium.IAlert alert) : IAlert
    {
        private readonly OpenQA.Selenium.IAlert _alert = alert;

        /// <inheritdoc />
        public void Accept() => _alert.Accept();

        /// <inheritdoc />
        public void Dismiss() => _alert.Dismiss();

        /// <inheritdoc />
        public string GetText() => _alert.Text;

        /// <inheritdoc />
        public void SendKeys(string keys) => _alert.SendKeys(keys);

    }
}
