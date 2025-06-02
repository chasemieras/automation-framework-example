using Moq;
using FrameworkSelenium.Selenium.Alerts;
using FrameworkSelenium.Selenium.Browsers;
using FluentAssertions;

namespace UnitTests.Framework
{
    public class AlertUnitTests
    {
        public class TestAlertsViaIAlert()
        {
            [Fact]
            public void Accept()
            {
                Mock<IAlert> alertMock = new();
                alertMock.Setup(a => a.Accept());

                alertMock.Object.Accept();

                alertMock.Verify(a => a.Accept(), Times.Once);
            }

            [Fact]
            public void Dismiss()
            {
                Mock<IAlert> alertMock = new();
                alertMock.Setup(a => a.Dismiss());

                alertMock.Object.Dismiss();

                alertMock.Verify(a => a.Dismiss(), Times.Once);
            }

            [Fact]
            public void GetText()
            {
                Mock<IAlert> alertMock = new();
                string expectedText = "Alert text";
                alertMock.Setup(a => a.GetText()).Returns(expectedText);

                string result = alertMock.Object.GetText();
                result.Should().Be(expectedText);

                alertMock.Verify(a => a.GetText(), Times.Once);
            }

            [Fact]
            public void SendKeys()
            {
                Mock<IAlert> alertMock = new();
                string keys = "test123";
                alertMock.Setup(a => a.SendKeys(keys));

                alertMock.Object.SendKeys(keys);

                alertMock.Verify(a => a.SendKeys(keys), Times.Once);
            }
        }

        public class TestAlertsViaIBrowser()
        {

            [Fact]
            public void Accept()
            {
                Mock<IAlert> alertMock = new Mock<IAlert>();
                alertMock.Setup(a => a.Accept());
                Mock<IBrowser> browserMock = new Mock<IBrowser>();
                browserMock.Setup(b => b.SwitchToAlert).Returns(alertMock.Object);

                IAlert alert = browserMock.Object.SwitchToAlert;
                alert.Accept();

                alertMock.Verify(a => a.Accept(), Times.Once);
            }

            [Fact]
            public void Dismiss()
            {
                Mock<IAlert> alertMock = new Mock<IAlert>();
                alertMock.Setup(a => a.Dismiss());
                Mock<IBrowser> browserMock = new Mock<IBrowser>();
                browserMock.Setup(b => b.SwitchToAlert).Returns(alertMock.Object);

                IAlert alert = browserMock.Object.SwitchToAlert;
                alert.Dismiss();

                alertMock.Verify(a => a.Dismiss(), Times.Once);
            }

            [Fact]
            public void GetText()
            {
                Mock<IAlert> alertMock = new Mock<IAlert>();
                alertMock.Setup(a => a.GetText()).Returns("Alert text");
                Mock<IBrowser> browserMock = new Mock<IBrowser>();
                browserMock.Setup(b => b.SwitchToAlert).Returns(alertMock.Object);

                IAlert alert = browserMock.Object.SwitchToAlert;
                string alertText = alert.GetText();

                alertMock.Verify(a => a.GetText(), Times.Once);
            }

            [Fact]
            public void SendKeys()
            {
                Mock<IAlert> alertMock = new Mock<IAlert>();
                alertMock.Setup(a => a.SendKeys(It.IsAny<string>()));
                Mock<IBrowser> browserMock = new Mock<IBrowser>();
                browserMock.Setup(b => b.SwitchToAlert).Returns(alertMock.Object);

                IAlert alert = browserMock.Object.SwitchToAlert;

                alert.SendKeys(It.IsAny<string>());

                alertMock.Verify(a => a.SendKeys(It.IsAny<string>()), Times.Once);
            }
        }
    }
}