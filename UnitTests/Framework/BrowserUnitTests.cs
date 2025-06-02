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

namespace UnitTests.Framework
{
    public class BrowserUnitTests
    {

        [Fact]
        public void Verify_BrowserIsNotNull()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Should().NotBeNull();
        }

        #region Variables

        [Fact]
        public void Verify_PageTitleIsNotNull()
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
        public void Verify_CurrentUrlIsNotNull()
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
        public void Verify_PageSourceIsNotNull()
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
        public void Verify_DriverTypeIsNotNull()
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
        public void Verify_CanNavigate()
        {
            Mock<IBrowser> mockBrowser = new();
            string testUrl = "http://example.com";
            mockBrowser.Setup(b => b.Navigate(testUrl));

            mockBrowser.Object.Navigate(testUrl);

            mockBrowser.Verify(b => b.Navigate(testUrl), Times.Once);
        }

        [Fact]
        public void Verify_CanGoBack()
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
        public void Verify_CanGoForward()
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
        public void Verify_CanRefresh()
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
        public void Verify_SwitchToAlertPositive()
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
        public void Verify_SwitchToAlertNegative()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.SwitchToAlert)
                        .Throws(new NoAlertPresentException("Alert not found"));

            Action act = () => { FrameworkSelenium.Selenium.Alerts.IAlert _ = mockBrowser.Object.SwitchToAlert; };
            act.Should().Throw<NoAlertPresentException>().WithMessage("Alert not found");

            mockBrowser.Verify(b => b.SwitchToAlert, Times.Once);
        }

        [Fact]
        public void Verify_AcceptAlertPositive()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Object.AcceptAlert();

            mockBrowser.Verify(b => b.AcceptAlert(), Times.Once);
        }

        [Fact]
        public void Verify_AcceptAlertNegative()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.AcceptAlert())
                        .Throws(new NoAlertPresentException("Alert not found"));

            Action act = () => mockBrowser.Object.AcceptAlert();
            act.Should().Throw<NoAlertPresentException>().WithMessage("Alert not found");

            mockBrowser.Verify(b => b.AcceptAlert(), Times.Once);
        }

        [Fact]
        public void Verify_DismissAlertPositive()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Object.DismissAlert();

            mockBrowser.Verify(b => b.DismissAlert(), Times.Once);
        }

        [Fact]
        public void Verify_DismissAlertNegative()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.DismissAlert())
                        .Throws(new NoAlertPresentException("Alert not found"));

            Action act = () => mockBrowser.Object.DismissAlert();
            act.Should().Throw<NoAlertPresentException>().WithMessage("Alert not found");

            mockBrowser.Verify(b => b.DismissAlert(), Times.Once);
        }

        [Fact]
        public void Verify_IsAlertPresentPositive()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.IsAlertPresent).Returns(true);

            bool isPresent = mockBrowser.Object.IsAlertPresent;

            isPresent.Should().BeTrue();
            mockBrowser.Verify(b => b.IsAlertPresent, Times.Once);
        }

        [Fact]
        public void Verify_IsAlertPresentNegative()
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
        public void Verify_SwitchToFrameLocatorPositive()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.SwitchToFrame(It.IsAny<ILocator>()));

            mockBrowser.Object.SwitchToFrame(Locator.Id("testFrame"));

            mockBrowser.Verify(b => b.SwitchToFrame(It.IsAny<ILocator>()), Times.Once);
        }

        [Fact]
        public void Verify_SwitchToFrameLocatorNegative()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.SwitchToFrame(It.IsAny<ILocator>()))
                        .Throws(new NoSuchFrameException("Frame not found"));

            Action act = () => mockBrowser.Object.SwitchToFrame(Locator.Id("nonExistentFrame"));
            act.Should().Throw<NoSuchFrameException>().WithMessage("Frame not found");

            mockBrowser.Verify(b => b.SwitchToFrame(It.IsAny<ILocator>()), Times.Once);
        }

        [Fact]
        public void Verify_SwitchToFrameIntPositive()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.SwitchToFrame(It.IsAny<int>()));

            mockBrowser.Object.SwitchToFrame(0);

            mockBrowser.Verify(b => b.SwitchToFrame(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void Verify_SwitchToFrameIntNegative()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.SwitchToFrame(It.IsAny<int>()))
                        .Throws(new NoSuchFrameException("Frame not found"));

            Action act = () => mockBrowser.Object.SwitchToFrame(0);
            act.Should().Throw<NoSuchFrameException>().WithMessage("Frame not found");

            mockBrowser.Verify(b => b.SwitchToFrame(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void Verify_SwitchToDefaultContent()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.SwitchToDefaultContent());
            mockBrowser.Object.SwitchToDefaultContent();
            mockBrowser.Verify(b => b.SwitchToDefaultContent(), Times.Once);
        }

        [Fact]
        public void Verify_SwitchToDefaultContentFromFrameLocator()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.SwitchToDefaultContent());
            mockBrowser.Setup(b => b.SwitchToFrame(It.IsAny<ILocator>()));

            mockBrowser.Object.SwitchToFrame(Locator.Id("testFrame"));
            mockBrowser.Object.SwitchToDefaultContent();
            mockBrowser.Verify(b => b.SwitchToDefaultContent(), Times.Once);
        }

        [Fact]
        public void Verify_SwitchToDefaultContentFromFrameInt()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.SwitchToDefaultContent());
            mockBrowser.Setup(b => b.SwitchToFrame(It.IsAny<int>()));

            mockBrowser.Object.SwitchToFrame(0);
            mockBrowser.Object.SwitchToDefaultContent();
            mockBrowser.Verify(b => b.SwitchToDefaultContent(), Times.Once);
        }

        [Fact]
        public void Verify_SwitchToDefaultContentFromAlert()
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
        public void Verify_AddCookie()
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
        public void Verify_GetCookiePositive()
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
        public void Verify_GetCookieNull()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.GetCookie(null))
                        .Throws(new ArgumentNullException("name", "Cookie name cannot be null"));

            Action act = () => mockBrowser.Object.GetCookie(null);
            act.Should().Throw<ArgumentNullException>().WithMessage("Cookie name cannot be null (Parameter 'name')");
        }

        [Fact]
        public void Verify_GetCookieNegative()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.GetCookie(It.IsAny<string>())).Returns((Cookie)null);

            string name = "nonexistentCookie";

            Cookie cookie = mockBrowser.Object.GetCookie(name);

            cookie.Should().BeNull();

            mockBrowser.Verify(b => b.GetCookie(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void Verify_DeleteCookie()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.DeleteCookie(It.IsAny<string>()));

            string name = "testCookie";

            mockBrowser.Object.DeleteCookie(name);

            mockBrowser.Verify(b => b.DeleteCookie(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void Verify_DeleteCookieVerifyAfter()
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
        public void Verify_DeleteCookieNull()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.DeleteCookie(null))
                        .Throws(new ArgumentNullException("name", "Cookie name cannot be null"));

            Action act = () => mockBrowser.Object.DeleteCookie(null);
            act.Should().Throw<ArgumentNullException>().WithMessage("Cookie name cannot be null (Parameter 'name')");
        }

        [Fact]
        public void Verify_DeleteAllCookie()
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
        public void Verify_GetAllCookie()
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
        public void Verify_GetAllCookieEmpty()
        {
            Mock<IBrowser> mockBrowser = new();

            mockBrowser.Setup(b => b.GetAllCookies)
                .Returns(() => new ReadOnlyCollection<Cookie>(new List<Cookie>()));

            ReadOnlyCollection<Cookie> cookieJar = mockBrowser.Object.GetAllCookies;
            cookieJar.Should().HaveCount(0);

            mockBrowser.Verify(b => b.GetAllCookies, Times.Once);
        }


        [Fact]
        public void Verify_DoesCookieExistPositive()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.DoesCookieExist(It.IsAny<string>())).Returns(true);

            bool doesIt = mockBrowser.Object.DoesCookieExist("testCookie");

            doesIt.Should().BeTrue();

            mockBrowser.Verify(b => b.DoesCookieExist(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void Verify_DoesCookieExistNegative()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.DoesCookieExist(It.IsAny<string>())).Returns(false);

            bool doesIt = mockBrowser.Object.DoesCookieExist("testCookie");

            doesIt.Should().BeFalse();

            mockBrowser.Verify(b => b.DoesCookieExist(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void Verify_DoesCookieExistNull()
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
        public void Verify_ScreenSizeDesktop()
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
        public void Verify_ScreenSizeMobile()
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
        public void Verify_ScreenSizeTablet()
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
        public void Verify_ScreenSizeNull()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.ScreenSize).Returns((ScreenSize)null);
            ScreenSize size = mockBrowser.Object.ScreenSize;

            size.Should().BeNull();

            mockBrowser.Verify(b => b.ScreenSize, Times.Once);
        }

        [Fact]
        public void Verify_WindowSizeDesktop()
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
        public void Verify_WindowSizeMobile()
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
        public void Verify_WindowSizeTablet()
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
        public void Verify_WindowSizeNull()
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
        public void Verify_GetCurrentWindowHandle()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.GetCurrentWindowHandle).Returns("currentWindowHandle123");
            string handle = mockBrowser.Object.GetCurrentWindowHandle;

            handle.Should().NotBeNullOrEmpty();
            handle.Should().Be("currentWindowHandle123");

            mockBrowser.Verify(b => b.GetCurrentWindowHandle, Times.Once);
        }

        [Fact]
        public void Verify_GetAllWindowHandlesSingle()
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
        public void Verify_GetAllWindowHandlesMultiple()
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
        public void Verify_GetAllWindowHandlesEmpty()
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
        public void Verify_CloseCurrentWindowSingle()
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
        public void Verify_CloseCurrentWindowMultiple()
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
        public void Verify_SwitchToWindow()
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
        public void Verify_SwitchToNewWindow()
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
        public void Verify_SwitchToNewTab()
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
        public void Verify_Quit()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.Quit());

            mockBrowser.Object.Quit();

            mockBrowser.Verify(b => b.Quit(), Times.Once);
        }

        #endregion

        #region Scrolling

        [Fact]
        public void Verify_ScrollToBottom()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.ScrollToBottom());

            mockBrowser.Object.ScrollToBottom();

            mockBrowser.Verify(b => b.ScrollToBottom(), Times.Once);
        }

        [Fact]
        public void Verify_ScrollToTop()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.ScrollToTop());

            mockBrowser.Object.ScrollToTop();

            mockBrowser.Verify(b => b.ScrollToTop(), Times.Once);
        }

        #endregion

        #region JavaScript

        [Fact]
        public void Verify_ExecuteJavaScript()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.ExecuteJavaScript(It.IsAny<string>()));

            mockBrowser.Object.ExecuteJavaScript(It.IsAny<string>());

            mockBrowser.Verify(b => b.ExecuteJavaScript(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void Verify_ExecuteJavaScriptThatReturnsString()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.ExecuteJavaScriptThatReturns(It.IsAny<string>())).Returns("result");

            string result = mockBrowser.Object.ExecuteJavaScriptThatReturns(It.IsAny<string>()).ToString();
            result.Should().NotBeNullOrEmpty();
            result.Should().Be("result");

            mockBrowser.Verify(b => b.ExecuteJavaScriptThatReturns(It.IsAny<string>()), Times.Once);
        }


        [Fact]
        public void Verify_ExecuteJavaScriptThatReturnsBoolTrue()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.ExecuteJavaScriptThatReturns(It.IsAny<string>())).Returns("true");

            bool result = bool.Parse(mockBrowser.Object.ExecuteJavaScriptThatReturns(It.IsAny<string>()).ToString());
            result.Should().BeTrue();

            mockBrowser.Verify(b => b.ExecuteJavaScriptThatReturns(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void Verify_ExecuteJavaScriptThatReturnsBoolFalse()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.ExecuteJavaScriptThatReturns(It.IsAny<string>())).Returns("false");

            bool result = bool.Parse(mockBrowser.Object.ExecuteJavaScriptThatReturns(It.IsAny<string>()).ToString());
            result.Should().BeFalse();

            mockBrowser.Verify(b => b.ExecuteJavaScriptThatReturns(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void Verify_ExecuteJavaScriptThatReturnsNull()
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
        public void Verify_SendKeys()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.SendKeys(It.IsAny<string>()));

            mockBrowser.Object.SendKeys(It.IsAny<string>());

            mockBrowser.Verify(b => b.SendKeys(It.IsAny<string>()), Times.Once);
        }

        #endregion

        #region Element Interaction

        [Fact]
        public void Verify_CanGetElement()
        {
            Mock<IBrowser> mockBrowser = new();
            Mock<IElement> mockElement = new();
            mockBrowser.Setup(b => b.GetElement(It.IsAny<ILocator>())).Returns(mockElement.Object);

            IElement element = mockBrowser.Object.GetElement(Locator.Id("test"));

            element.Should().NotBeNull();
            mockBrowser.Verify(b => b.GetElement(It.IsAny<ILocator>()), Times.Once);
        }

        [Fact]
        public void Verify_CanGetElements()
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
        public void Verify_ElementExist(bool expected, bool checkInteractable, int time)
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
        public void Verify_ElementsExist(bool expected, bool checkInteractable, int time)
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
