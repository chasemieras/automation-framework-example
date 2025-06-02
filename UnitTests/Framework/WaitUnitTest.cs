using Moq;
using FrameworkSelenium.Selenium.Elements;
using FrameworkSelenium.Selenium.Locators;
using FrameworkSelenium.Selenium.Browsers;
using FrameworkSelenium.Selenium.Waits;

namespace UnitTests.Framework
{
    public class WaitUnitTests
    {
        public class IBrowserUnitTests()
        {
            [Fact]
            public void UntilElementExists_ReturnsElement()
            {
                Mock<ILocator> locatorMock = new();
                Mock<IElement> elementMock = new();
                Mock<IWait<IBrowser>> waitMock = new();

                waitMock.Setup(w => w.UntilElementExists(locatorMock.Object))
                    .Returns(elementMock.Object);

                IElement result = waitMock.Object.UntilElementExists(locatorMock.Object);

                Assert.Equal(elementMock.Object, result);
            }

            [Fact]
            public void Until_ReturnsTrueWhenConditionMet()
            {
                Mock<IWait<IBrowser>> waitMock = new();
                Func<IBrowser, bool> condition = b => true;

                waitMock.Setup(w => w.Until(condition)).Returns(true);

                bool result = waitMock.Object.Until(condition);

                Assert.True(result);
            }

            [Fact]
            public void Until_ReturnsFalseWhenConditionNotMet()
            {
                Mock<IWait<IBrowser>> waitMock = new();
                Func<IBrowser, bool> condition = b => false;

                waitMock.Setup(w => w.Until(condition)).Returns(false);

                bool result = waitMock.Object.Until(condition);

                Assert.False(result);
            }

            [Fact]
            public void UntilSuccessful_CallsMethod()
            {
                Mock<IWait<IBrowser>> waitMock = new();
                Func<IBrowser, bool> condition = b => true;

                waitMock.Setup(w => w.UntilSuccessful(condition));

                waitMock.Object.UntilSuccessful(condition);

                waitMock.Verify(w => w.UntilSuccessful(condition), Times.Once);
            }

        }

        public class IElementUnitTests()
        {
            [Fact]
            public void UntilElementExists_ReturnsElement()
            {
                Mock<ILocator> locatorMock = new();
                Mock<IElement> elementMock = new();
                Mock<IWait<IElement>> waitMock = new();

                waitMock.Setup(w => w.UntilElementExists(locatorMock.Object))
                    .Returns(elementMock.Object);

                IElement result = waitMock.Object.UntilElementExists(locatorMock.Object);

                Assert.Equal(elementMock.Object, result);
            }

            [Fact]
            public void Until_ReturnsTrueWhenConditionMet()
            {
                Mock<IWait<IElement>> waitMock = new();
                Func<IElement, bool> condition = e => true;

                waitMock.Setup(w => w.Until(condition)).Returns(true);

                bool result = waitMock.Object.Until(condition);

                Assert.True(result);
            }

            [Fact]
            public void Until_ReturnsFalseWhenConditionNotMet()
            {
                Mock<IWait<IElement>> waitMock = new();
                Func<IElement, bool> condition = e => false;

                waitMock.Setup(w => w.Until(condition)).Returns(false);

                bool result = waitMock.Object.Until(condition);

                Assert.False(result);
            }

            [Fact]
            public void UntilSuccessful_CallsMethod()
            {
                Mock<IWait<IElement>> waitMock = new();
                Func<IElement, bool> condition = e => true;

                waitMock.Setup(w => w.UntilSuccessful(condition));

                waitMock.Object.UntilSuccessful(condition);

                waitMock.Verify(w => w.UntilSuccessful(condition), Times.Once);
            }
        }
    }
}