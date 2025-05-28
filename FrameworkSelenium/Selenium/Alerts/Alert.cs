namespace FrameworkSelenium.Selenium.Alerts
{
    public class Alert(OpenQA.Selenium.IAlert alert) : IAlert
    {
        private readonly OpenQA.Selenium.IAlert _alert = alert;

        public void Accept() => _alert.Accept();

        public void Dismiss() => _alert.Dismiss();

        public string GetText() => _alert.Text;

        public void SendKeys(string keys) => _alert.SendKeys(keys);

    }
}
