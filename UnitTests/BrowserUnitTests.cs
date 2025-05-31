using Moq;
using OpenQA.Selenium;
using FrameworkSelenium.Selenium.Elements;
using FrameworkSelenium.Selenium.Browsers;
using FrameworkSelenium.Selenium.Locators;
using FluentAssertions;
using System.Collections.ObjectModel;
using FrameworkSelenium.Selenium.Alerts;

namespace UnitTests
{
    public class BrowserUnitTests
    {
        //todo add more tests with moq
        //todo add workflow on github to run unit tests after a PR is merged
        //todo add README
        //todo add docker compose of grid
        //todo add xunit test runner
        //todo make attributes that set the size
        //todo look at Relative Locators

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
            mockBrowser.Setup(b => b.SwitchToAlert()).Returns(mockAlert.Object);

            FrameworkSelenium.Selenium.Alerts.IAlert alert = mockBrowser.Object.SwitchToAlert();
            alert.Should().NotBeNull();
            alert.Should().Be(mockAlert.Object);

            mockBrowser.Verify(b => b.SwitchToAlert(), Times.Once);
        }

        [Fact]
        public void VerifySwitchToAlertNegative()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.SwitchToAlert())
                        .Throws(new NoAlertPresentException("Alert not found"));

            Action act = () => mockBrowser.Object.SwitchToAlert();
            act.Should().Throw<NoAlertPresentException>().WithMessage("Alert not found");

            mockBrowser.Verify(b => b.SwitchToAlert(), Times.Once);
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
            mockBrowser.Setup(b => b.IsAlertPresent()).Returns(true);

            bool isPresent = mockBrowser.Object.IsAlertPresent();

            isPresent.Should().BeTrue();
            mockBrowser.Verify(b => b.IsAlertPresent(), Times.Once);
        }

        [Fact]
        public void VerifyIsAlertPresentNegative()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.IsAlertPresent()).Returns(false);

            bool isPresent = mockBrowser.Object.IsAlertPresent();

            isPresent.Should().BeFalse();
            mockBrowser.Verify(b => b.IsAlertPresent(), Times.Once);
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
            mockBrowser.Setup(b => b.SwitchToAlert()).Returns(mockAlert.Object);

            FrameworkSelenium.Selenium.Alerts.IAlert alert = mockBrowser.Object.SwitchToAlert();
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
            List<Cookie> cookieList = [new ("testCookie", "testValue")];

            mockBrowser.Setup(b => b.GetAllCookies())
                .Returns(() => new ReadOnlyCollection<Cookie>(cookieList));
            mockBrowser.Setup(b => b.DeleteCookie(It.IsAny<string>()))
                .Callback<string>(name => cookieList.RemoveAll(c => c.Name == name));

            mockBrowser.Object.GetAllCookies().Should().HaveCount(1);

            mockBrowser.Object.DeleteCookie("testCookie");

            mockBrowser.Object.GetAllCookies().Should().BeEmpty();

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
            List<Cookie> cookieList = [new ("testCookie", "testValue")];

            mockBrowser.Setup(b => b.GetAllCookies())
                .Returns(() => new ReadOnlyCollection<Cookie>(cookieList));
            mockBrowser.Setup(b => b.DeleteAllCookies())
                .Callback(() => cookieList.Clear());

            mockBrowser.Object.GetAllCookies().Should().HaveCount(1);

            mockBrowser.Object.DeleteAllCookies();

            mockBrowser.Object.GetAllCookies().Should().BeEmpty();

            mockBrowser.Verify(b => b.DeleteAllCookies(), Times.Once);
        }

        [Fact]
        public void VerifyGetAllCookie()
        {
            Mock<IBrowser> mockBrowser = new();
            List<Cookie> cookieList = [new ("testCookie", "testValue")];

            mockBrowser.Setup(b => b.GetAllCookies())
                .Returns(() => new ReadOnlyCollection<Cookie>(cookieList));

            ReadOnlyCollection<Cookie> cookieJar = mockBrowser.Object.GetAllCookies();
            cookieJar.Should().HaveCount(1);
            cookieJar[0].Name.Should().Be("testCookie");
            cookieJar[0].Value.Should().Be("testValue");

            mockBrowser.Verify(b => b.GetAllCookies(), Times.Once);
        }

        [Fact]
        public void VerifyGetAllCookieEmpty()
        {
            Mock<IBrowser> mockBrowser = new();

            mockBrowser.Setup(b => b.GetAllCookies())
                .Returns(() => new ReadOnlyCollection<Cookie>(new List<Cookie>()));

            ReadOnlyCollection<Cookie> cookieJar = mockBrowser.Object.GetAllCookies();
            cookieJar.Should().HaveCount(0);

            mockBrowser.Verify(b => b.GetAllCookies(), Times.Once);
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

        // #region Window + Tab Interaction

        // /// <summary>
        // /// The <see cref="ScreenSize"/> of the browser
        // /// </summary>
        // ScreenSize ScreenSize { get; }

        // /// <summary>
        // /// The <see cref="Size"/> of the current window
        // /// </summary>
        // Size WindowSize { get; }

        // /// <summary>
        // /// Gets the current window handle
        // /// </summary>
        // /// <returns>A string that is the current window handle</returns>
        // string GetCurrentWindowHandle();

        // /// <summary>
        // /// Gets all of the window handles
        // /// </summary>
        // /// <returns>A ReadOnlyCollection of string that are the window handles</returns>
        // ReadOnlyCollection<string> GetAllWindowHandles();

        // /// <summary>
        // /// Closes the current tab, or window if there is only one tab
        // /// </summary>
        // void CloseCurrentWindow();

        // /// <summary>
        // /// Switches to a window with the given <paramref name="windowHandle"/>
        // /// </summary>
        // /// <param name="windowHandle">The name of the window you want to go to</param>
        // void SwitchToWindow(string windowHandle);

        // /// <summary>
        // /// Opens and navigates to a new window
        // /// </summary>
        // void SwitchToNewWindow();

        // /// <summary>
        // /// Opens and navigates to a new tab
        // /// </summary>
        // void SwitchToNewTab();

        // /// <summary>
        // /// Calls to <see cref="WebDrivers.DriverHelper"/> to quit the current browser running
        // /// </summary>
        // void Quit();

        // #endregion

        // #region Scrolling

        // /// <summary>
        // /// Scrolls to the bottom of the page
        // /// </summary>
        // void ScrollToBottom();

        // /// <summary>
        // /// Scrolls to the top of the page
        // /// </summary>
        // void ScrollToTop();

        // #endregion

        // #region JavaScript

        // /// <summary>
        // /// Runs the given JavaScript
        // /// </summary>
        // /// <param name="javaScriptToRun">JS to run</param>
        // void ExecuteJavaScript(string javaScriptToRun);

        // /// <summary>
        // /// Runs JavaScript that returns something
        // /// </summary>
        // /// <param name="javaScriptToRun">JS to run</param>
        // /// <returns>something</returns>
        // object ExecuteJavaScriptThatReturns(string javaScriptToRun);

        // #endregion

        // #region Other Methods

        // /// <summary>
        // /// Sends given keys to the <see cref="Browser"/>
        // /// </summary>
        // /// <param name="keys">specific text or <see cref="Keys"/></param>
        // void SendKeys(string keys);

        // #endregion

        // #region Element Interaction

        // [Fact]
        // public void VerifyCanGetElement()
        // {
        //     // Arrange
        //     Mock<IBrowser> mockBrowser = new();
        //     Mock<IElement> mockElement = new();
        //     mockBrowser.Setup(b => b.GetElement(It.IsAny<ILocator>())).Returns(mockElement.Object);

        //     // Act
        //     var element = mockBrowser.Object.GetElement(Locator.Id("test"));

        //     // Assert
        //     element.Should().NotBeNull();
        //     mockBrowser.Verify(b => b.GetElement(It.IsAny<ILocator>()), Times.Once);
        // }

        // [Fact]
        // public void VerifyCanGetElements()
        // {
        //     // Arrange
        //     Mock<IBrowser> mockBrowser = new();
        //     Mock<IElement> mockElement = new();
        //     List<IElement> mockElements = [mockElement.Object];
        //     mockBrowser.Setup(b => b.GetElements(It.IsAny<ILocator>())).Returns(mockElements);

        //     // Act
        //     var elements = mockBrowser.Object.GetElements(Locator.Class("test"));

        //     // Assert
        //     elements.Should().NotBeNull();
        //     elements.Count.Should().Be(1);
        //     mockBrowser.Verify(b => b.GetElements(It.IsAny<ILocator>()), Times.Once);
        // }

        // /// <inheritdoc />
        // public bool ElementExist(ILocator locator, bool checkIfInteractable = true, TimeSpan defaultTimeout = default)
        // {
        //     Wait<IBrowser> wait = new(this, defaultTimeout == TimeSpan.Zero ? TimeSpan.FromSeconds(1) : defaultTimeout);
        //     IElement result = wait.UntilElementExists(locator);
        //     if (checkIfInteractable)
        //         return result.IsInteractable;

        //     return true;
        // }

        // /// <inheritdoc />
        // public bool ElementsExist(ILocator locator, bool checkIfInteractable = true, TimeSpan defaultTimeout = default)
        // {
        //     Wait<IBrowser> wait = new(this, defaultTimeout == TimeSpan.Zero ? TimeSpan.FromSeconds(1) : defaultTimeout);
        //     bool result = false;
        //     wait.UntilSuccessful(x =>
        //     {
        //         List<IElement> elements = x.GetElements(locator);
        //         if (elements.Count == 0)
        //             return result;

        //         if (elements.Any(x => x.IsInteractable == false))
        //             return result;

        //         result = true;
        //         return result;
        //     });

        //     return result;
        // }

        // #endregion


        // [Fact]
        // public void ExampleMoq()
        // {
        //     // Arrange
        //     //Mock<IElement> mockInput = new Mock<IElement>();
        //     Mock<IElement> mockContainer = new Mock<IElement>();
        //     //mockContainer.Setup(m => m.GetElement(It.IsAny<ILocator>())).Returns(mockInput.Object);

        //     Mock<IBrowser> mockBrowser = new Mock<IBrowser>();
        //     mockBrowser.Setup(b => b.GetElement(It.IsAny<ILocator>())).Returns(mockContainer.Object);

        //     // Act
        //     var main = mockBrowser.Object.GetElement(Locator.Id("main"));
        //     var input = main.GetElement(Locator.Name("username"));
        //     //input.SendKeys("testuser");

        //     // Assert
        //     //mockInput.Verify(i => i.SendKeys("testuser"), Times.Once);
        // }
    }
}