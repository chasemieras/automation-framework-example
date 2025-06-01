#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.

using Moq;
using OpenQA.Selenium;
using FrameworkSelenium.Selenium.Browsers;
using FrameworkSelenium.Selenium.Locators;
using FluentAssertions;
using System.Collections.ObjectModel;
using FrameworkSelenium;
using System.Drawing;
using FrameworkSelenium.Selenium.Elements;

namespace UnitTests
{
    public class BrowserUnitTests
    {

        [Fact]
        public void VerifyBrowserIsNotNull()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Should().NotBeNull();
        }

        #region Variables

        [Fact]
        public void VerifyPageTitleIsNotNull()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Should().NotBeNull();
            mockBrowser.Setup(b => b.Title).Returns("Test Page Title");
            string title = mockBrowser.Object.Title;

            title.Should().NotBeNullOrEmpty();
            title.Should().Be("Test Page Title");

            mockBrowser.Verify(b => b.Title, Times.Once);
        }

        [Fact]
        public void VerifyCurrentUrlIsNotNull()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Should().NotBeNull();
            mockBrowser.Setup(b => b.CurrentUrl).Returns("http://example.com");
            string url = mockBrowser.Object.CurrentUrl;

            url.Should().NotBeNullOrEmpty();
            url.Should().Be("http://example.com");

            mockBrowser.Verify(b => b.CurrentUrl, Times.Once);
        }

        [Fact]
        public void VerifyPageSourceIsNotNull()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Should().NotBeNull();
            mockBrowser.Setup(b => b.PageSource).Returns("<html><body>Test Page Source</body></html>");
            string pageSource = mockBrowser.Object.PageSource;

            pageSource.Should().NotBeNullOrEmpty();
            pageSource.Should().Be("<html><body>Test Page Source</body></html>");

            mockBrowser.Verify(b => b.PageSource, Times.Once);
        }

        [Fact]
        public void VerifyDriverTypeIsNotNull()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Should().NotBeNull();
            mockBrowser.Setup(b => b.DriverType).Returns("MockDriver");
            string driverType = mockBrowser.Object.DriverType;

            driverType.Should().NotBeNullOrEmpty();
            driverType.Should().Be("MockDriver");

            mockBrowser.Verify(b => b.DriverType, Times.Once);
        }

        #endregion

        #region Navigation

        [Fact]
        public void VerifyCanNavigate()
        {
            Mock<IBrowser> mockBrowser = new();
            string testUrl = "http://example.com";
            mockBrowser.Setup(b => b.Navigate(testUrl));

            mockBrowser.Object.Navigate(testUrl);

            mockBrowser.Verify(b => b.Navigate(testUrl), Times.Once);
        }

        [Fact]
        public void VerifyCanGoBack()
        {
            Mock<IBrowser> mockBrowser = new();
            string testUrl = "http://example.com";
            string currUrl = "http://start.com";
            string urlMemory = currUrl;

            mockBrowser.SetupGet(b => b.CurrentUrl).Returns(() => urlMemory);
            mockBrowser.Setup(b => b.Navigate(It.IsAny<string>()))
                .Callback<string>(url => urlMemory = url);
            mockBrowser.Setup(b => b.Back())
                .Callback(() => urlMemory = currUrl);

            mockBrowser.Object.Navigate(testUrl);
            mockBrowser.Object.CurrentUrl.Should().Be(testUrl);
            mockBrowser.Object.Back();
            mockBrowser.Object.CurrentUrl.Should().Be(currUrl);

            mockBrowser.Verify(b => b.Back(), Times.Once);
        }

        [Fact]
        public void VerifyCanGoForward()
        {
            Mock<IBrowser> mockBrowser = new();
            string testUrl = "http://example.com";
            string currUrl = "http://start.com";
            string urlMemory = currUrl;

            mockBrowser.SetupGet(b => b.CurrentUrl).Returns(() => urlMemory);
            mockBrowser.Setup(b => b.Navigate(It.IsAny<string>()))
                .Callback<string>(url => urlMemory = url);
            mockBrowser.Setup(b => b.Back())
                .Callback(() => urlMemory = currUrl);
            mockBrowser.Setup(b => b.Forward())
                .Callback(() => urlMemory = testUrl);

            mockBrowser.Object.Navigate(testUrl);
            mockBrowser.Object.CurrentUrl.Should().Be(testUrl);
            mockBrowser.Object.Back();
            mockBrowser.Object.CurrentUrl.Should().Be(currUrl);
            mockBrowser.Object.Forward();
            mockBrowser.Object.CurrentUrl.Should().Be(testUrl);

            mockBrowser.Verify(b => b.Forward(), Times.Once);
        }

        [Fact]
        public void VerifyCanRefresh()
        {
            Mock<IBrowser> mockBrowser = new();
            string testUrl = "http://example.com";
            string urlMemory = testUrl;

            mockBrowser.SetupGet(b => b.CurrentUrl).Returns(() => urlMemory);
            mockBrowser.Setup(b => b.Navigate(It.IsAny<string>()))
                .Callback<string>(url => urlMemory = url);
            mockBrowser.Setup(b => b.Refresh())
                .Callback(() => urlMemory = testUrl);

            mockBrowser.Object.Navigate(testUrl);
            mockBrowser.Object.CurrentUrl.Should().Be(testUrl);
            mockBrowser.Object.Refresh();
            mockBrowser.Object.CurrentUrl.Should().Be(testUrl);

            mockBrowser.Verify(b => b.Refresh(), Times.Once);
        }

        #endregion

        #region Alert Interaction

        [Fact]
        public void VerifySwitchToAlertPositive()
        {
            Mock<IBrowser> mockBrowser = new();
            Mock<FrameworkSelenium.Selenium.Alerts.IAlert> mockAlert = new();
            mockBrowser.Setup(b => b.SwitchToAlert).Returns(mockAlert.Object);

            FrameworkSelenium.Selenium.Alerts.IAlert alert = mockBrowser.Object.SwitchToAlert;
            alert.Should().NotBeNull();
            alert.Should().Be(mockAlert.Object);

            mockBrowser.Verify(b => b.SwitchToAlert, Times.Once);
        }

        [Fact]
        public void VerifySwitchToAlertNegative()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.SwitchToAlert)
                        .Throws(new NoAlertPresentException("Alert not found"));

            Action act = () => { FrameworkSelenium.Selenium.Alerts.IAlert _ = mockBrowser.Object.SwitchToAlert; };
            act.Should().Throw<NoAlertPresentException>().WithMessage("Alert not found");

            mockBrowser.Verify(b => b.SwitchToAlert, Times.Once);
        }

        [Fact]
        public void VerifyAcceptAlertPositive()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Object.AcceptAlert();

            mockBrowser.Verify(b => b.AcceptAlert(), Times.Once);
        }

        [Fact]
        public void VerifyAcceptAlertNegative()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.AcceptAlert())
                        .Throws(new NoAlertPresentException("Alert not found"));

            Action act = () => mockBrowser.Object.AcceptAlert();
            act.Should().Throw<NoAlertPresentException>().WithMessage("Alert not found");

            mockBrowser.Verify(b => b.AcceptAlert(), Times.Once);
        }

        [Fact]
        public void VerifyDismissAlertPositive()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Object.DismissAlert();

            mockBrowser.Verify(b => b.DismissAlert(), Times.Once);
        }

        [Fact]
        public void VerifyDismissAlertNegative()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.DismissAlert())
                        .Throws(new NoAlertPresentException("Alert not found"));

            Action act = () => mockBrowser.Object.DismissAlert();
            act.Should().Throw<NoAlertPresentException>().WithMessage("Alert not found");

            mockBrowser.Verify(b => b.DismissAlert(), Times.Once);
        }

        [Fact]
        public void VerifyIsAlertPresentPositive()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.IsAlertPresent).Returns(true);

            bool isPresent = mockBrowser.Object.IsAlertPresent;

            isPresent.Should().BeTrue();
            mockBrowser.Verify(b => b.IsAlertPresent, Times.Once);
        }

        [Fact]
        public void VerifyIsAlertPresentNegative()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.IsAlertPresent).Returns(false);

            bool isPresent = mockBrowser.Object.IsAlertPresent;

            isPresent.Should().BeFalse();
            mockBrowser.Verify(b => b.IsAlertPresent, Times.Once);
        }

        #endregion

        #region iFrame Handling

        [Fact]
        public void VerifySwitchToFrameLocatorPositive()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.SwitchToFrame(It.IsAny<ILocator>()));

            mockBrowser.Object.SwitchToFrame(Locator.Id("testFrame"));

            mockBrowser.Verify(b => b.SwitchToFrame(It.IsAny<ILocator>()), Times.Once);
        }

        [Fact]
        public void VerifySwitchToFrameLocatorNegative()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.SwitchToFrame(It.IsAny<ILocator>()))
                        .Throws(new NoSuchFrameException("Frame not found"));

            Action act = () => mockBrowser.Object.SwitchToFrame(Locator.Id("nonExistentFrame"));
            act.Should().Throw<NoSuchFrameException>().WithMessage("Frame not found");

            mockBrowser.Verify(b => b.SwitchToFrame(It.IsAny<ILocator>()), Times.Once);
        }

        [Fact]
        public void VerifySwitchToFrameIntPositive()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.SwitchToFrame(It.IsAny<int>()));

            mockBrowser.Object.SwitchToFrame(0);

            mockBrowser.Verify(b => b.SwitchToFrame(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void VerifySwitchToFrameIntNegative()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.SwitchToFrame(It.IsAny<int>()))
                        .Throws(new NoSuchFrameException("Frame not found"));

            Action act = () => mockBrowser.Object.SwitchToFrame(0);
            act.Should().Throw<NoSuchFrameException>().WithMessage("Frame not found");

            mockBrowser.Verify(b => b.SwitchToFrame(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void VerifySwitchToDefaultContent()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.SwitchToDefaultContent());
            mockBrowser.Object.SwitchToDefaultContent();
            mockBrowser.Verify(b => b.SwitchToDefaultContent(), Times.Once);
        }

        [Fact]
        public void VerifySwitchToDefaultContentFromFrameLocator()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.SwitchToDefaultContent());
            mockBrowser.Setup(b => b.SwitchToFrame(It.IsAny<ILocator>()));

            mockBrowser.Object.SwitchToFrame(Locator.Id("testFrame"));
            mockBrowser.Object.SwitchToDefaultContent();
            mockBrowser.Verify(b => b.SwitchToDefaultContent(), Times.Once);
        }

        [Fact]
        public void VerifySwitchToDefaultContentFromFrameInt()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.SwitchToDefaultContent());
            mockBrowser.Setup(b => b.SwitchToFrame(It.IsAny<int>()));

            mockBrowser.Object.SwitchToFrame(0);
            mockBrowser.Object.SwitchToDefaultContent();
            mockBrowser.Verify(b => b.SwitchToDefaultContent(), Times.Once);
        }

        [Fact]
        public void VerifySwitchToDefaultContentFromAlert()
        {
            Mock<IBrowser> mockBrowser = new();
            Mock<FrameworkSelenium.Selenium.Alerts.IAlert> mockAlert = new();

            mockBrowser.Setup(b => b.SwitchToDefaultContent());
            mockBrowser.Setup(b => b.SwitchToAlert).Returns(mockAlert.Object);

            FrameworkSelenium.Selenium.Alerts.IAlert alert = mockBrowser.Object.SwitchToAlert;
            alert.Should().NotBeNull();
            alert.Should().Be(mockAlert.Object);

            mockBrowser.Object.SwitchToDefaultContent();

            mockBrowser.Verify(b => b.SwitchToDefaultContent(), Times.Once);
        }

        #endregion

        #region Cookie Handling

        [Fact]
        public void VerifyAddCookie()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.AddCookie(It.IsAny<string>(), It.IsAny<string>()));
            mockBrowser.Setup(b => b.GetCookie(It.IsAny<string>())).Returns(new Cookie("testCookie", "testValue"));

            string name = "testCookie";
            string value = "testValue";
            mockBrowser.Object.AddCookie(name, value);

            Cookie cookie = mockBrowser.Object.GetCookie(name);

            cookie.Should().NotBeNull();
            cookie.Name.Should().Be(name);
            cookie.Value.Should().Be(value);

            mockBrowser.Verify(b => b.AddCookie(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void VerifyGetCookiePositive()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.GetCookie(It.IsAny<string>())).Returns(new Cookie("testCookie", "testValue"));

            string name = "testCookie";
            string value = "testValue";

            Cookie cookie = mockBrowser.Object.GetCookie(name);

            cookie.Should().NotBeNull();
            cookie.Name.Should().Be(name);
            cookie.Value.Should().Be(value);

            mockBrowser.Verify(b => b.GetCookie(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void VerifyGetCookieNull()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.GetCookie(null))
                        .Throws(new ArgumentNullException("name", "Cookie name cannot be null"));

            Action act = () => mockBrowser.Object.GetCookie(null);
            act.Should().Throw<ArgumentNullException>().WithMessage("Cookie name cannot be null (Parameter 'name')");
        }

        [Fact]
        public void VerifyGetCookieNegative()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.GetCookie(It.IsAny<string>())).Returns((Cookie)null);

            string name = "nonexistentCookie";

            Cookie cookie = mockBrowser.Object.GetCookie(name);

            cookie.Should().BeNull();

            mockBrowser.Verify(b => b.GetCookie(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void VerifyDeleteCookie()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.DeleteCookie(It.IsAny<string>()));

            string name = "testCookie";

            mockBrowser.Object.DeleteCookie(name);

            mockBrowser.Verify(b => b.DeleteCookie(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void VerifyDeleteCookieVerifyAfter()
        {
            // Arrange
            Mock<IBrowser> mockBrowser = new();
            List<Cookie> cookieList = new List<Cookie> { new("testCookie", "testValue") };

            mockBrowser.Setup(b => b.GetAllCookies)
                .Returns(() => new ReadOnlyCollection<Cookie>(cookieList));
            mockBrowser.Setup(b => b.DeleteCookie(It.IsAny<string>()))
                .Callback<string>(name => cookieList.RemoveAll(c => c.Name == name));

            mockBrowser.Object.GetAllCookies.Should().HaveCount(1);

            mockBrowser.Object.DeleteCookie("testCookie");

            mockBrowser.Object.GetAllCookies.Should().BeEmpty();

            mockBrowser.Verify(b => b.DeleteCookie("testCookie"), Times.Once);
        }

        [Fact]
        public void VerifyDeleteCookieNull()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.DeleteCookie(null))
                        .Throws(new ArgumentNullException("name", "Cookie name cannot be null"));

            Action act = () => mockBrowser.Object.DeleteCookie(null);
            act.Should().Throw<ArgumentNullException>().WithMessage("Cookie name cannot be null (Parameter 'name')");
        }

        [Fact]
        public void VerifyDeleteAllCookie()
        {
            // Arrange
            Mock<IBrowser> mockBrowser = new();
            List<Cookie> cookieList = new List<Cookie> { new("testCookie", "testValue") };

            mockBrowser.Setup(b => b.GetAllCookies)
                .Returns(() => new ReadOnlyCollection<Cookie>(cookieList));
            mockBrowser.Setup(b => b.DeleteAllCookies())
                .Callback(() => cookieList.Clear());

            mockBrowser.Object.GetAllCookies.Should().HaveCount(1);

            mockBrowser.Object.DeleteAllCookies();

            mockBrowser.Object.GetAllCookies.Should().BeEmpty();

            mockBrowser.Verify(b => b.DeleteAllCookies(), Times.Once);
        }

        [Fact]
        public void VerifyGetAllCookie()
        {
            Mock<IBrowser> mockBrowser = new();
            List<Cookie> cookieList = new List<Cookie> { new("testCookie", "testValue") };

            mockBrowser.Setup(b => b.GetAllCookies)
                .Returns(() => new ReadOnlyCollection<Cookie>(cookieList));

            ReadOnlyCollection<Cookie> cookieJar = mockBrowser.Object.GetAllCookies;
            cookieJar.Should().HaveCount(1);
            cookieJar[0].Name.Should().Be("testCookie");
            cookieJar[0].Value.Should().Be("testValue");

            mockBrowser.Verify(b => b.GetAllCookies, Times.Once);
        }

        [Fact]
        public void VerifyGetAllCookieEmpty()
        {
            Mock<IBrowser> mockBrowser = new();

            mockBrowser.Setup(b => b.GetAllCookies)
                .Returns(() => new ReadOnlyCollection<Cookie>(new List<Cookie>()));

            ReadOnlyCollection<Cookie> cookieJar = mockBrowser.Object.GetAllCookies;
            cookieJar.Should().HaveCount(0);

            mockBrowser.Verify(b => b.GetAllCookies, Times.Once);
        }


        [Fact]
        public void VerifyDoesCookieExistPositive()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.DoesCookieExist(It.IsAny<string>())).Returns(true);

            bool doesIt = mockBrowser.Object.DoesCookieExist("testCookie");

            doesIt.Should().BeTrue();

            mockBrowser.Verify(b => b.DoesCookieExist(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void VerifyDoesCookieExistNegative()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.DoesCookieExist(It.IsAny<string>())).Returns(false);

            bool doesIt = mockBrowser.Object.DoesCookieExist("testCookie");

            doesIt.Should().BeFalse();

            mockBrowser.Verify(b => b.DoesCookieExist(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void VerifyDoesCookieExistNull()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.DoesCookieExist(null))
                        .Throws(new ArgumentNullException("name", "Cookie name cannot be null"));

            Action act = () => mockBrowser.Object.DoesCookieExist(null);
            act.Should().Throw<ArgumentNullException>().WithMessage("Cookie name cannot be null (Parameter 'name')");
        }

        #endregion

        #region Window + Tab Interaction

        [Fact]
        public void VerifyScreenSizeDesktop()
        {
            Helper.SetFrameworkConfiguration("config.json");
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.ScreenSize).Returns(ScreenSize.Desktop);
            ScreenSize size = mockBrowser.Object.ScreenSize;

            size.Should().NotBeNull();
            size.Type.Should().Be(ScreenSize.Desktop.Type);
            size.Width.Should().Be(ScreenSize.Desktop.Width);
            size.Height.Should().Be(ScreenSize.Desktop.Height);

            mockBrowser.Verify(b => b.ScreenSize, Times.Once);
        }

        [Fact]
        public void VerifyScreenSizeMobile()
        {
            Helper.SetFrameworkConfiguration("config.json");
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.ScreenSize).Returns(ScreenSize.Mobile);
            ScreenSize size = mockBrowser.Object.ScreenSize;

            size.Should().NotBeNull();
            size.Type.Should().Be(ScreenSize.Mobile.Type);
            size.Width.Should().Be(ScreenSize.Mobile.Width);
            size.Height.Should().Be(ScreenSize.Mobile.Height);

            mockBrowser.Verify(b => b.ScreenSize, Times.Once);
        }

        [Fact]
        public void VerifyScreenSizeTablet()
        {
            Helper.SetFrameworkConfiguration("config.json");
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.ScreenSize).Returns(ScreenSize.Tablet);
            ScreenSize size = mockBrowser.Object.ScreenSize;

            size.Should().NotBeNull();
            size.Type.Should().Be(ScreenSize.Tablet.Type);
            size.Width.Should().Be(ScreenSize.Tablet.Width);
            size.Height.Should().Be(ScreenSize.Tablet.Height);

            mockBrowser.Verify(b => b.ScreenSize, Times.Once);
        }

        [Fact]
        public void VerifyScreenSizeNull()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.ScreenSize).Returns((ScreenSize)null);
            ScreenSize size = mockBrowser.Object.ScreenSize;

            size.Should().BeNull();

            mockBrowser.Verify(b => b.ScreenSize, Times.Once);
        }

        [Fact]
        public void VerifyWindowSizeDesktop()
        {
            Helper.SetFrameworkConfiguration("config.json");
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.WindowSize).Returns(new Size(ScreenSize.Desktop.Width, ScreenSize.Desktop.Height));
            Size size = mockBrowser.Object.WindowSize;

            size.Should().NotBeNull();
            size.Width.Should().Be(ScreenSize.Desktop.Width);
            size.Height.Should().Be(ScreenSize.Desktop.Height);
            size.IsEmpty.Should().BeFalse();

            mockBrowser.Verify(b => b.WindowSize, Times.Once);
        }

        [Fact]
        public void VerifyWindowSizeMobile()
        {
            Helper.SetFrameworkConfiguration("config.json");
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.WindowSize).Returns(new Size(ScreenSize.Mobile.Width, ScreenSize.Mobile.Height));
            Size size = mockBrowser.Object.WindowSize;

            size.Should().NotBeNull();
            size.Width.Should().Be(ScreenSize.Mobile.Width);
            size.Height.Should().Be(ScreenSize.Mobile.Height);
            size.IsEmpty.Should().BeFalse();

            mockBrowser.Verify(b => b.WindowSize, Times.Once);
        }

        [Fact]
        public void VerifyWindowSizeTablet()
        {
            Helper.SetFrameworkConfiguration("config.json");
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.WindowSize).Returns(new Size(ScreenSize.Tablet.Width, ScreenSize.Tablet.Height));
            Size size = mockBrowser.Object.WindowSize;

            size.Should().NotBeNull();
            size.Width.Should().Be(ScreenSize.Tablet.Width);
            size.Height.Should().Be(ScreenSize.Tablet.Height);
            size.IsEmpty.Should().BeFalse();

            mockBrowser.Verify(b => b.WindowSize, Times.Once);
        }

        [Fact]
        public void VerifyWindowSizeNull()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.WindowSize).Returns(null);
            Size size = mockBrowser.Object.WindowSize;

            size.Width.Should().Be(0);
            size.Height.Should().Be(0);
            size.IsEmpty.Should().BeTrue();

            mockBrowser.Verify(b => b.WindowSize, Times.Once);
        }

        [Fact]
        public void VerifyGetCurrentWindowHandle()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.GetCurrentWindowHandle).Returns("currentWindowHandle123");
            string handle = mockBrowser.Object.GetCurrentWindowHandle;

            handle.Should().NotBeNullOrEmpty();
            handle.Should().Be("currentWindowHandle123");

            mockBrowser.Verify(b => b.GetCurrentWindowHandle, Times.Once);
        }

        [Fact]
        public void VerifyGetAllWindowHandlesSingle()
        {
            Mock<IBrowser> mockBrowser = new();
            List<string> handles = new List<string> { "windowHandle1" };

            mockBrowser.Setup(b => b.GetAllWindowHandles)
                .Returns(() => new ReadOnlyCollection<string>(handles));

            ReadOnlyCollection<string> windowHandles = mockBrowser.Object.GetAllWindowHandles;
            windowHandles.Should().NotBeNull();
            windowHandles.Should().HaveCount(1);
            windowHandles[0].Should().Be("windowHandle1");

            mockBrowser.Verify(b => b.GetAllWindowHandles, Times.Once);
        }

        [Fact]
        public void VerifyGetAllWindowHandlesMultiple()
        {
            Mock<IBrowser> mockBrowser = new();
            List<string> handles = new List<string> { "windowHandle1", "windowHandle2", "windowHandle3" };

            mockBrowser.Setup(b => b.GetAllWindowHandles)
                .Returns(() => new ReadOnlyCollection<string>(handles));

            ReadOnlyCollection<string> windowHandles = mockBrowser.Object.GetAllWindowHandles;
            windowHandles.Should().NotBeNull();
            windowHandles.Should().HaveCount(3);

            windowHandles[0].Should().Be("windowHandle1");
            windowHandles[1].Should().Be("windowHandle2");
            windowHandles[2].Should().Be("windowHandle3");

            mockBrowser.Verify(b => b.GetAllWindowHandles, Times.Once);
        }

        [Fact]
        public void VerifyGetAllWindowHandlesEmpty()
        {
            Mock<IBrowser> mockBrowser = new();
            List<string> handles = new List<string>();

            mockBrowser.Setup(b => b.GetAllWindowHandles)
                .Returns(() => new ReadOnlyCollection<string>(handles));

            ReadOnlyCollection<string> windowHandles = mockBrowser.Object.GetAllWindowHandles;
            windowHandles.Should().NotBeNull();
            windowHandles.Should().HaveCount(0);

            mockBrowser.Verify(b => b.GetAllWindowHandles, Times.Once);
        }

        [Fact]
        public void VerifyCloseCurrentWindowSingle()
        {
            // Arrange
            Mock<IBrowser> mockBrowser = new();
            List<string> handles = new List<string> { "window1" };
            mockBrowser.Setup(b => b.GetAllWindowHandles)
                .Returns(() => new ReadOnlyCollection<string>(handles));
            mockBrowser.Setup(b => b.CloseCurrentWindow())
                .Callback(() =>
                {
                    if (handles.Count > 0)
                        handles.RemoveAt(handles.Count - 1);
                });

            mockBrowser.Object.GetAllWindowHandles.Should().HaveCount(1);

            mockBrowser.Object.CloseCurrentWindow();

            mockBrowser.Object.GetAllWindowHandles.Should().HaveCount(0);

            mockBrowser.Verify(b => b.CloseCurrentWindow(), Times.Once);
        }

        [Fact]
        public void VerifyCloseCurrentWindowMultiple()
        {
            Mock<IBrowser> mockBrowser = new();
            List<string> handles = new List<string> { "window1", "window2" };
            mockBrowser.Setup(b => b.GetAllWindowHandles)
                .Returns(() => new ReadOnlyCollection<string>(handles));
            mockBrowser.Setup(b => b.CloseCurrentWindow())
                .Callback(() =>
                {
                    if (handles.Count > 0)
                        handles.RemoveAt(handles.Count - 1);
                });

            mockBrowser.Object.GetAllWindowHandles.Should().HaveCount(2);

            mockBrowser.Object.CloseCurrentWindow();

            mockBrowser.Object.GetAllWindowHandles.Should().HaveCount(1);

            mockBrowser.Verify(b => b.CloseCurrentWindow(), Times.Once);
        }

        [Fact]
        public void VerifySwitchToWindow()
        {
            Mock<IBrowser> mockBrowser = new();
            List<string> handles = new List<string> { "window1", "window2" };
            string currentHandle = "window1";

            mockBrowser.Setup(b => b.GetAllWindowHandles)
                .Returns(() => new ReadOnlyCollection<string>(handles));
            mockBrowser.Setup(b => b.GetCurrentWindowHandle)
                .Returns(() => currentHandle);
            mockBrowser.Setup(b => b.SwitchToWindow(It.IsAny<string>()))
                .Callback<string>(handle =>
                {
                    if (handles.Contains(handle))
                        currentHandle = handle;
                });

            mockBrowser.Object.GetCurrentWindowHandle.Should().Be("window1");

            mockBrowser.Object.SwitchToWindow("window2");

            mockBrowser.Object.GetCurrentWindowHandle.Should().Be("window2");

            mockBrowser.Verify(b => b.SwitchToWindow("window2"), Times.Once);
        }

        [Fact]
        public void VerifySwitchToNewWindow()
        {
            Mock<IBrowser> mockBrowser = new();
            List<string> handles = new List<string> { "window1" };
            string currentHandle = "window1";

            mockBrowser.Setup(b => b.GetAllWindowHandles)
                .Returns(() => new ReadOnlyCollection<string>(handles));
            mockBrowser.Setup(b => b.GetCurrentWindowHandle)
                .Returns(() => currentHandle);
            mockBrowser.Setup(b => b.SwitchToNewWindow())
                .Callback(() =>
                {
                    string newHandle = $"window{handles.Count + 1}";
                    handles.Add(newHandle);
                    currentHandle = newHandle;
                });

            mockBrowser.Object.GetAllWindowHandles.Should().HaveCount(1);
            mockBrowser.Object.GetCurrentWindowHandle.Should().Be("window1");

            mockBrowser.Object.SwitchToNewWindow();

            mockBrowser.Object.GetAllWindowHandles.Should().HaveCount(2);
            mockBrowser.Object.GetCurrentWindowHandle.Should().Be("window2");

            mockBrowser.Verify(b => b.SwitchToNewWindow(), Times.Once);
        }

        [Fact]
        public void VerifySwitchToNewTab()
        {
            Mock<IBrowser> mockBrowser = new();
            List<string> handles = new List<string> { "tab1" };
            string currentHandle = "tab1";

            mockBrowser.Setup(b => b.GetAllWindowHandles)
                .Returns(() => new ReadOnlyCollection<string>(handles));
            mockBrowser.Setup(b => b.GetCurrentWindowHandle)
                .Returns(() => currentHandle);
            mockBrowser.Setup(b => b.SwitchToNewTab())
                .Callback(() =>
                {
                    string newHandle = $"tab{handles.Count + 1}";
                    handles.Add(newHandle);
                    currentHandle = newHandle;
                });

            mockBrowser.Object.GetAllWindowHandles.Should().HaveCount(1);
            mockBrowser.Object.GetCurrentWindowHandle.Should().Be("tab1");

            mockBrowser.Object.SwitchToNewTab();

            mockBrowser.Object.GetAllWindowHandles.Should().HaveCount(2);
            mockBrowser.Object.GetCurrentWindowHandle.Should().Be("tab2");

            mockBrowser.Verify(b => b.SwitchToNewTab(), Times.Once);
        }
        [Fact]
        public void VerifyQuit()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.Quit());

            mockBrowser.Object.Quit();

            mockBrowser.Verify(b => b.Quit(), Times.Once);
        }

        #endregion

        #region Scrolling

        [Fact]
        public void VerifyScrollToBottom()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.ScrollToBottom());

            mockBrowser.Object.ScrollToBottom();

            mockBrowser.Verify(b => b.ScrollToBottom(), Times.Once);
        }

        [Fact]
        public void VerifyScrollToTop()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.ScrollToTop());

            mockBrowser.Object.ScrollToTop();

            mockBrowser.Verify(b => b.ScrollToTop(), Times.Once);
        }

        #endregion

        #region JavaScript

        [Fact]
        public void VerifyExecuteJavaScript()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.ExecuteJavaScript(It.IsAny<string>()));

            mockBrowser.Object.ExecuteJavaScript(It.IsAny<string>());

            mockBrowser.Verify(b => b.ExecuteJavaScript(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void VerifyExecuteJavaScriptThatReturnsString()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.ExecuteJavaScriptThatReturns(It.IsAny<string>())).Returns("result");

            string result = mockBrowser.Object.ExecuteJavaScriptThatReturns(It.IsAny<string>()).ToString();
            result.Should().NotBeNullOrEmpty();
            result.Should().Be("result");

            mockBrowser.Verify(b => b.ExecuteJavaScriptThatReturns(It.IsAny<string>()), Times.Once);
        }


        [Fact]
        public void VerifyExecuteJavaScriptThatReturnsBoolTrue()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.ExecuteJavaScriptThatReturns(It.IsAny<string>())).Returns("true");

            bool result = bool.Parse(mockBrowser.Object.ExecuteJavaScriptThatReturns(It.IsAny<string>()).ToString());
            result.Should().BeTrue();

            mockBrowser.Verify(b => b.ExecuteJavaScriptThatReturns(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void VerifyExecuteJavaScriptThatReturnsBoolFalse()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.ExecuteJavaScriptThatReturns(It.IsAny<string>())).Returns("false");

            bool result = bool.Parse(mockBrowser.Object.ExecuteJavaScriptThatReturns(It.IsAny<string>()).ToString());
            result.Should().BeFalse();

            mockBrowser.Verify(b => b.ExecuteJavaScriptThatReturns(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void VerifyExecuteJavaScriptThatReturnsNull()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.ExecuteJavaScriptThatReturns(It.IsAny<string>())).Returns(null);

            object result = mockBrowser.Object.ExecuteJavaScriptThatReturns(It.IsAny<string>());
            result.Should().BeNull();

            mockBrowser.Verify(b => b.ExecuteJavaScriptThatReturns(It.IsAny<string>()), Times.Once);
        }

        #endregion

        #region Other Methods

        [Fact]
        public void VerifySendKeys()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.SendKeys(It.IsAny<string>()));

            mockBrowser.Object.SendKeys(It.IsAny<string>());

            mockBrowser.Verify(b => b.SendKeys(It.IsAny<string>()), Times.Once);
        }

        #endregion

        #region Element Interaction

        [Fact]
        public void VerifyCanGetElement()
        {
            Mock<IBrowser> mockBrowser = new();
            Mock<IElement> mockElement = new();
            mockBrowser.Setup(b => b.GetElement(It.IsAny<ILocator>())).Returns(mockElement.Object);

            IElement element = mockBrowser.Object.GetElement(Locator.Id("test"));

            element.Should().NotBeNull();
            mockBrowser.Verify(b => b.GetElement(It.IsAny<ILocator>()), Times.Once);
        }

        [Fact]
        public void VerifyCanGetElements()
        {
            Mock<IBrowser> mockBrowser = new();
            Mock<IElement> mockElement = new();
            List<IElement> mockElements = new List<IElement> { mockElement.Object };
            mockBrowser.Setup(b => b.GetElements(It.IsAny<ILocator>())).Returns(mockElements);

            List<IElement> elements = mockBrowser.Object.GetElements(Locator.Class("test"));

            elements.Should().NotBeNull();
            elements.Count.Should().Be(1);
            mockBrowser.Verify(b => b.GetElements(It.IsAny<ILocator>()), Times.Once);
        }

        [Theory]
        [InlineData(true, true, 0)]
        [InlineData(false, true, 0)]
        [InlineData(true, false, 0)]
        [InlineData(false, false, 0)]
        [InlineData(true, true, 5)]
        [InlineData(true, true, 500)]
        [InlineData(true, false, 5)]
        [InlineData(true, false, 500)]
        [InlineData(false, true, 5)]
        [InlineData(false, true, 500)]
        [InlineData(false, false, 5)]
        [InlineData(false, false, 500)]
        public void VerifyElementExist(bool expected, bool checkInteractable, int time)
        {
            TimeSpan span = time == 0 ? default : TimeSpan.FromSeconds(time);
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.ElementExist(It.IsAny<ILocator>(), checkInteractable, span)).Returns(expected);

            bool result = mockBrowser.Object.ElementExist(Locator.Class("test"), checkInteractable, span);

            result.Should().Be(expected);
            mockBrowser.Verify(b => b.ElementExist(It.IsAny<ILocator>(), checkInteractable, span), Times.Once);
        }

        [Theory]
        [InlineData(true, true, 0)]
        [InlineData(false, true, 0)]
        [InlineData(true, false, 0)]
        [InlineData(false, false, 0)]
        [InlineData(true, true, 5)]
        [InlineData(true, true, 500)]
        [InlineData(true, false, 5)]
        [InlineData(true, false, 500)]
        [InlineData(false, true, 5)]
        [InlineData(false, true, 500)]
        [InlineData(false, false, 5)]
        [InlineData(false, false, 500)]
        public void VerifyElementsExist(bool expected, bool checkInteractable, int time)
        {
            TimeSpan span = time == 0 ? default : TimeSpan.FromSeconds(time);
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.ElementsExist(It.IsAny<ILocator>(), checkInteractable, span)).Returns(expected);

            bool result = mockBrowser.Object.ElementsExist(Locator.Class("test"), checkInteractable, span);

            result.Should().Be(expected);
            mockBrowser.Verify(b => b.ElementsExist(It.IsAny<ILocator>(), checkInteractable, span), Times.Once);
        }

        #endregion

    }
}

#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
