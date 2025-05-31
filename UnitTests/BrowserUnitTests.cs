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
        public void EnsureBrowserIsNotNull()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Should().NotBeNull();
        }

        #region Variables

        [Fact]
        public void EnsurePageTitleIsNotNull()
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
        public void EnsureCurrentUrlIsNotNull()
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
        public void EnsurePageSourceIsNotNull()
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
        public void EnsureDriverTypeIsNotNull()
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
        public void EnsureCanNavigate()
        {
            Mock<IBrowser> mockBrowser = new();
            string testUrl = "http://example.com";
            mockBrowser.Setup(b => b.Navigate(testUrl));

            mockBrowser.Object.Navigate(testUrl);

            mockBrowser.Verify(b => b.Navigate(testUrl), Times.Once);
        }

        [Fact]
        public void EnsureCanGoBack()
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
        public void EnsureCanGoForward()
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
        public void EnsureCanRefresh()
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
        public void EnsureSwitchToAlertPositive()
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
        public void EnsureSwitchToAlertNegative()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.SwitchToAlert())
                        .Throws(new NoAlertPresentException("Alert not found"));

            Action act = () => mockBrowser.Object.SwitchToAlert();
            act.Should().Throw<NoAlertPresentException>().WithMessage("Alert not found");

            mockBrowser.Verify(b => b.SwitchToAlert(), Times.Once);
        }

        [Fact]
        public void EnsureAcceptAlertPositive()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Object.AcceptAlert();

            mockBrowser.Verify(b => b.AcceptAlert(), Times.Once);
        }

        [Fact]
        public void EnsureAcceptAlertNegative()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.AcceptAlert())
                        .Throws(new NoAlertPresentException("Alert not found"));

            Action act = () => mockBrowser.Object.AcceptAlert();
            act.Should().Throw<NoAlertPresentException>().WithMessage("Alert not found");

            mockBrowser.Verify(b => b.AcceptAlert(), Times.Once);
        }

        [Fact]
        public void EnsureDismissAlertPositive()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Object.DismissAlert();

            mockBrowser.Verify(b => b.DismissAlert(), Times.Once);
        }

        [Fact]
        public void EnsureDismissAlertNegative()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.DismissAlert())
                        .Throws(new NoAlertPresentException("Alert not found"));

            Action act = () => mockBrowser.Object.DismissAlert();
            act.Should().Throw<NoAlertPresentException>().WithMessage("Alert not found");

            mockBrowser.Verify(b => b.DismissAlert(), Times.Once);
        }

        [Fact]
        public void EnsureIsAlertPresentPositive()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.IsAlertPresent()).Returns(true);

            bool isPresent = mockBrowser.Object.IsAlertPresent();

            isPresent.Should().BeTrue();
            mockBrowser.Verify(b => b.IsAlertPresent(), Times.Once);
        }

        [Fact]
        public void EnsureIsAlertPresentNegative()
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
        public void EnsureSwitchToFrameLocatorPositive()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.SwitchToFrame(It.IsAny<ILocator>()));

            mockBrowser.Object.SwitchToFrame(Locator.Id("testFrame"));

            mockBrowser.Verify(b => b.SwitchToFrame(It.IsAny<ILocator>()), Times.Once);
        }

        [Fact]
        public void EnsureSwitchToFrameLocatorNegative()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.SwitchToFrame(It.IsAny<ILocator>()))
                        .Throws(new NoSuchFrameException("Frame not found"));

            Action act = () => mockBrowser.Object.SwitchToFrame(Locator.Id("nonExistentFrame"));
            act.Should().Throw<NoSuchFrameException>().WithMessage("Frame not found");

            mockBrowser.Verify(b => b.SwitchToFrame(It.IsAny<ILocator>()), Times.Once);
        }

        [Fact]
        public void EnsureSwitchToFrameIntPositive()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.SwitchToFrame(It.IsAny<int>()));

            mockBrowser.Object.SwitchToFrame(0);

            mockBrowser.Verify(b => b.SwitchToFrame(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void EnsureSwitchToFrameIntNegative()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.SwitchToFrame(It.IsAny<int>()))
                        .Throws(new NoSuchFrameException("Frame not found"));

            Action act = () => mockBrowser.Object.SwitchToFrame(0);
            act.Should().Throw<NoSuchFrameException>().WithMessage("Frame not found");

            mockBrowser.Verify(b => b.SwitchToFrame(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void EnsureSwitchToDefaultContent()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.SwitchToDefaultContent());
            mockBrowser.Object.SwitchToDefaultContent();
            mockBrowser.Verify(b => b.SwitchToDefaultContent(), Times.Once);
        }

        [Fact]
        public void EnsureSwitchToDefaultContentFromFrameLocator()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.SwitchToDefaultContent());
            mockBrowser.Setup(b => b.SwitchToFrame(It.IsAny<ILocator>()));

            mockBrowser.Object.SwitchToFrame(Locator.Id("testFrame"));
            mockBrowser.Object.SwitchToDefaultContent();
            mockBrowser.Verify(b => b.SwitchToDefaultContent(), Times.Once);
        }

        [Fact]
        public void EnsureSwitchToDefaultContentFromFrameInt()
        {
            Mock<IBrowser> mockBrowser = new();
            mockBrowser.Setup(b => b.SwitchToDefaultContent());
            mockBrowser.Setup(b => b.SwitchToFrame(It.IsAny<int>()));

            mockBrowser.Object.SwitchToFrame(0);
            mockBrowser.Object.SwitchToDefaultContent();
            mockBrowser.Verify(b => b.SwitchToDefaultContent(), Times.Once);
        }

        [Fact]
        public void EnsureSwitchToDefaultContentFromAlert()
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

        // #region Cookie Handling

        // /// <summary>
        // /// Adds a new <see cref="Cookie"/> with the given <paramref name="name"/> and <paramref name="value"/>
        // /// </summary>
        // /// <param name="name">the name of the cookie you want to add</param>
        // /// <param name="value">the value of the cookie you want to add</param>
        // void AddCookie(string name, string value);

        // /// <summary>
        // /// Gets a <see cref="Cookie"/> based on the <paramref name="name"/> given
        // /// </summary>
        // /// <param name="name">the name of the cookie you want to get</param>
        // /// <returns>A <see cref="Cookie"/></returns>
        // Cookie GetCookie(string name);

        // /// <summary>
        // /// Deletes a <see cref="Cookie"/> based on the <paramref name="name"/> given
        // /// </summary>
        // /// <param name="name">the name of the cookie you want to get</param>
        // void DeleteCookie(string name);

        // /// <summary>
        // /// Deletes all <see cref="Cookie"/>s
        // /// </summary>
        // void DeleteAllCookies();

        // /// <summary>
        // /// Gets all <see cref="Cookie"/>s
        // /// </summary>
        // /// <returns>A ReadOnlyCollection of <see cref="Cookie"/>s</returns>
        // ReadOnlyCollection<Cookie> GetAllCookies();

        // /// <summary>
        // /// Checks that the given <see cref="Cookie"/> <paramref name="name"/> is present
        // /// </summary>
        // /// <returns><b>True</b>: The <see cref="Cookie"/> is present | <b>False</b>: The <see cref="Cookie"/> is not present</returns>
        // bool DoesCookieExist(string name);

        // #endregion

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
        // public void EnsureCanGetElement()
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
        // public void EnsureCanGetElements()
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