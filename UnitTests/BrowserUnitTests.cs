using Moq;
using Xunit;
using OpenQA.Selenium;

public class LoginTests
{
    //todo add more tests with moq
    //todo add workflow on github to run unit tests after a PR is merged
    //todo add README
    //todo add docker compose of grid
    //todo add xunit test runner

    [Fact]
    public void ExampleMoq()
    {
        // Arrange
        var mockInput = new Mock<IElement>();
        var mockContainer = new Mock<IElement>();
        mockContainer.Setup(m => m.Find(By.Name("username"))).Returns(mockInput.Object);

        var mockBrowser = new Mock<IBrowser>();
        mockBrowser.Setup(b => b.Find(By.Id("main"))).Returns(mockContainer.Object);

        // Act
        var main = mockBrowser.Object.Find(By.Id("main"));
        var input = main.Find(By.Name("username"));
        input.SendKeys("testuser");

        // Assert
        mockInput.Verify(i => i.SendKeys("testuser"), Times.Once);
    }
}
