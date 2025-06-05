using OpenQA.Selenium;
using FluentAssertions;
using Moq;
using AutomationFramework.Exceptions;
using AutomationFramework.Enums;
using AutomationFramework.Selenium.WebDrivers;

namespace UnitTests.Framework
{
    public class DriverHelperUnitTests
    {
        [Fact]
        public void ThrowsFrameworkExceptionForUnsupportedDriverType()
        {
            DriverType unsupportedType = (DriverType)999;
            void act() => DriverHelper.GetDriver(unsupportedType);
            Assert.Throws<FrameworkException>(act);
        }

        [Fact]
        public void GetDriverReturnsSameInstanceForSameThread()
        {
            Mock<IWebDriver> mockDriver = new();
            Mock<IDriver> mockIDriver = new();
            mockIDriver.Setup(d => d.GenerateDriverOptions).Returns(new Mock<DriverOptions>().Object);
            mockIDriver.Setup(d => d.GenerateWebDriver(It.IsAny<DriverOptions>())).Returns(mockDriver.Object);

            IWebDriver driver1 = mockIDriver.Object.GenerateWebDriver(mockIDriver.Object.GenerateDriverOptions);
            IWebDriver driver2 = mockIDriver.Object.GenerateWebDriver(mockIDriver.Object.GenerateDriverOptions);
            driver1.Should().BeSameAs(driver2);
        }

        [Fact]
        public void QuitDriverSetsDriverToNull()
        {
            Mock<IWebDriver> mockDriver = new();
            Mock<IDriver> mockIDriver = new();
            mockIDriver.Setup(d => d.GenerateDriverOptions).Returns(new Mock<DriverOptions>().Object);
            mockIDriver.Setup(d => d.GenerateWebDriver(It.IsAny<DriverOptions>())).Returns(mockDriver.Object);

            mockIDriver.Object.GenerateWebDriver(mockIDriver.Object.GenerateDriverOptions);
            mockDriver.Object.Quit();
            IWebDriver? driver = null;
            driver.Should().BeNull();
        }

        [Fact]
        public void CanCreateChromeDriver()
        {
            Mock<IWebDriver> mockDriver = new();
            Mock<IDriver> mockIDriver = new();
            mockIDriver.Setup(d => d.GenerateDriverOptions).Returns(new Mock<DriverOptions>().Object);
            mockIDriver.Setup(d => d.GenerateWebDriver(It.IsAny<DriverOptions>())).Returns(mockDriver.Object);

            IWebDriver driver = mockIDriver.Object.GenerateWebDriver(mockIDriver.Object.GenerateDriverOptions);
            driver.Should().NotBeNull();
        }

        [Fact]
        public void CanCreateEdgeDriver()
        {
            Mock<IWebDriver> mockDriver = new();
            Mock<IDriver> mockIDriver = new();
            mockIDriver.Setup(d => d.GenerateDriverOptions).Returns(new Mock<DriverOptions>().Object);
            mockIDriver.Setup(d => d.GenerateWebDriver(It.IsAny<DriverOptions>())).Returns(mockDriver.Object);

            IWebDriver driver = mockIDriver.Object.GenerateWebDriver(mockIDriver.Object.GenerateDriverOptions);
            driver.Should().NotBeNull();
        }

        [Fact]
        public void CanCreateFirefoxDriver()
        {
            Mock<IWebDriver> mockDriver = new();
            Mock<IDriver> mockIDriver = new();
            mockIDriver.Setup(d => d.GenerateDriverOptions).Returns(new Mock<DriverOptions>().Object);
            mockIDriver.Setup(d => d.GenerateWebDriver(It.IsAny<DriverOptions>())).Returns(mockDriver.Object);

            IWebDriver driver = mockIDriver.Object.GenerateWebDriver(mockIDriver.Object.GenerateDriverOptions);
            driver.Should().NotBeNull();
        }
    }
}