using Moq;
using FluentAssertions;
using System.Drawing;
using FrameworkSelenium.Selenium.Elements;
using FrameworkSelenium.Selenium.Locators;

namespace UnitTests.Framework
{
    public class ElementUnitTests
    {
        #region Variables

        [Fact]
        public void Verify_IsDisplayed()
        {
            Mock<IElement> mockElement = new();
            mockElement.Setup(e => e.IsDisplayed).Returns(true);

            bool displayed = mockElement.Object.IsDisplayed;

            displayed.Should().BeTrue();
            mockElement.Verify(e => e.IsDisplayed, Times.Once);
        }

        [Fact]
        public void Verify_IsEnabled()
        {
            Mock<IElement> mockElement = new();
            mockElement.Setup(e => e.IsEnabled).Returns(true);

            bool enabled = mockElement.Object.IsEnabled;

            enabled.Should().BeTrue();
            mockElement.Verify(e => e.IsEnabled, Times.Once);
        }

        [Fact]
        public void Verify_IsInteractable()
        {
            Mock<IElement> mockElement = new();
            mockElement.Setup(e => e.IsInteractable).Returns(true);

            bool interactable = mockElement.Object.IsInteractable;

            interactable.Should().BeTrue();
            mockElement.Verify(e => e.IsInteractable, Times.Once);
        }

        [Fact]
        public void Verify_IsSelected()
        {
            Mock<IElement> mockElement = new();
            mockElement.Setup(e => e.IsSelected).Returns(true);

            bool selected = mockElement.Object.IsSelected;

            selected.Should().BeTrue();
            mockElement.Verify(e => e.IsSelected, Times.Once);
        }

        [Fact]
        public void Verify_Location()
        {
            Mock<IElement> mockElement = new();
            Point point = new(10, 20);
            mockElement.Setup(e => e.Location).Returns(point);

            Point result = mockElement.Object.Location;

            result.Should().Be(point);
            mockElement.Verify(e => e.Location, Times.Once);
        }

        #endregion

        #region Click Interactions

        [Fact]
        public void Verify_Click()
        {
            Mock<IElement> mockElement = new();
            mockElement.Setup(e => e.Click());

            mockElement.Object.Click();

            mockElement.Verify(e => e.Click(), Times.Once);
        }

        [Fact]
        public void Verify_RightClick()
        {
            Mock<IElement> mockElement = new();
            mockElement.Setup(e => e.RightClick());

            mockElement.Object.RightClick();

            mockElement.Verify(e => e.RightClick(), Times.Once);
        }

        #endregion

        #region Attributes

        [Fact]
        public void Verify_GetAttribute()
        {
            Mock<IElement> mockElement = new();
            mockElement.Setup(e => e.GetAttribute("data-test")).Returns("value");

            string attr = mockElement.Object.GetAttribute("data-test");

            attr.Should().Be("value");
            mockElement.Verify(e => e.GetAttribute("data-test"), Times.Once);
        }

        [Fact]
        public void Verify_AttributeExists()
        {
            Mock<IElement> mockElement = new();
            mockElement.Setup(e => e.AttributeExists("data-test")).Returns(true);

            bool exists = mockElement.Object.AttributeExists("data-test");

            exists.Should().BeTrue();
            mockElement.Verify(e => e.AttributeExists("data-test"), Times.Once);
        }

        [Fact]
        public void Verify_Src()
        {
            Mock<IElement> mockElement = new();
            mockElement.SetupGet(e => e.Src).Returns("http://img");
            mockElement.Object.Src.Should().Be("http://img");
        }

        [Fact]
        public void Verify_Href()
        {
            Mock<IElement> mockElement = new();
            mockElement.SetupGet(e => e.Href).Returns("http://link");
            mockElement.Object.Href.Should().Be("http://link");
        }

        [Fact]
        public void Verify_Value()
        {
            Mock<IElement> mockElement = new();
            mockElement.SetupGet(e => e.Value).Returns("inputVal");
            mockElement.Object.Value.Should().Be("inputVal");
        }

        [Fact]
        public void Verify_ClassName()
        {
            Mock<IElement> mockElement = new();
            mockElement.SetupGet(e => e.ClassName).Returns("my-class");
            mockElement.Object.ClassName.Should().Be("my-class");
        }

        [Fact]
        public void Verify_Id()
        {
            Mock<IElement> mockElement = new();
            mockElement.SetupGet(e => e.Id).Returns("my-id");
            mockElement.Object.Id.Should().Be("my-id");
        }

        [Fact]
        public void Verify_Name()
        {
            Mock<IElement> mockElement = new();
            mockElement.SetupGet(e => e.Name).Returns("my-name");
            mockElement.Object.Name.Should().Be("my-name");
        }

        [Fact]
        public void Verify_Title()
        {
            Mock<IElement> mockElement = new();
            mockElement.SetupGet(e => e.Title).Returns("my-title");
            mockElement.Object.Title.Should().Be("my-title");
        }

        [Fact]
        public void Verify_InnerHtml()
        {
            Mock<IElement> mockElement = new();
            mockElement.SetupGet(e => e.InnerHtml).Returns("<b>hi</b>");
            mockElement.Object.InnerHtml.Should().Be("<b>hi</b>");
        }

        [Fact]
        public void Verify_OuterHtml()
        {
            Mock<IElement> mockElement = new();
            mockElement.SetupGet(e => e.OuterHtml).Returns("<div></div>");
            mockElement.Object.OuterHtml.Should().Be("<div></div>");
        }

        [Fact]
        public void Verify_Target()
        {
            Mock<IElement> mockElement = new();
            mockElement.SetupGet(e => e.Target).Returns("_blank");
            mockElement.Object.Target.Should().Be("_blank");
        }

        [Fact]
        public void Verify_AltText()
        {
            Mock<IElement> mockElement = new();
            mockElement.SetupGet(e => e.AltText).Returns("alt");
            mockElement.Object.AltText.Should().Be("alt");
        }

        [Fact]
        public void Verify_TagName()
        {
            Mock<IElement> mockElement = new();
            mockElement.SetupGet(e => e.TagName).Returns("div");
            mockElement.Object.TagName.Should().Be("div");
        }

        [Fact]
        public void Verify_Class()
        {
            Mock<IElement> mockElement = new();
            mockElement.SetupGet(e => e.Class).Returns("class");
            mockElement.Object.Class.Should().Be("class");
        }

        [Fact]
        public void Verify_Rel()
        {
            Mock<IElement> mockElement = new();
            mockElement.SetupGet(e => e.Rel).Returns("nofollow");
            mockElement.Object.Rel.Should().Be("nofollow");
        }

        [Fact]
        public void Verify_Checked()
        {
            Mock<IElement> mockElement = new();
            mockElement.Setup(e => e.Checked).Returns(true);

            bool checkedVal = mockElement.Object.Checked;

            checkedVal.Should().BeTrue();
            mockElement.Verify(e => e.Checked, Times.Once);
        }

        #endregion

        #region Styling

        [Fact]
        public void Verify_Styling()
        {
            Mock<IElement> mockElement = new();
            mockElement.Setup(e => e.Styling).Returns("color: red;");

            string styling = mockElement.Object.Styling;

            styling.Should().Be("color: red;");
            mockElement.Verify(e => e.Styling, Times.Once);
        }

        [Fact]
        public void Verify_GetCssValue()
        {
            Mock<IElement> mockElement = new();
            mockElement.Setup(e => e.GetCssValue("color")).Returns("red");

            string css = mockElement.Object.GetCssValue("color");

            css.Should().Be("red");
            mockElement.Verify(e => e.GetCssValue("color"), Times.Once);
        }

        #endregion

        #region Text Interaction

        [Fact]
        public void Verify_Text()
        {
            Mock<IElement> mockElement = new();
            mockElement.Setup(e => e.Text).Returns("Hello");

            string text = mockElement.Object.Text;

            text.Should().Be("Hello");
            mockElement.Verify(e => e.Text, Times.Once);
        }

        [Fact]
        public void Verify_ExtractString()
        {
            Mock<IElement> mockElement = new();
            mockElement.Setup(e => e.ExtractString).Returns("Extracted");

            string extract = mockElement.Object.ExtractString;

            extract.Should().Be("Extracted");
            mockElement.Verify(e => e.ExtractString, Times.Once);
        }

        [Fact]
        public void Verify_SendKeys()
        {
            Mock<IElement> mockElement = new();
            mockElement.Setup(e => e.SendKeys("test"));

            mockElement.Object.SendKeys("test");

            mockElement.Verify(e => e.SendKeys("test"), Times.Once);
        }

        [Fact]
        public void Verify_SendEnter()
        {
            Mock<IElement> mockElement = new();
            mockElement.Setup(e => e.SendEnter());

            mockElement.Object.SendEnter();

            mockElement.Verify(e => e.SendEnter(), Times.Once);
        }

        [Fact]
        public void Verify_Clear()
        {
            Mock<IElement> mockElement = new();
            mockElement.Setup(e => e.Clear());

            mockElement.Object.Clear();

            mockElement.Verify(e => e.Clear(), Times.Once);
        }

        [Fact]
        public void Verify_Submit()
        {
            Mock<IElement> mockElement = new();
            mockElement.Setup(e => e.Submit());

            mockElement.Object.Submit();

            mockElement.Verify(e => e.Submit(), Times.Once);
        }

        #endregion

        #region Other Methods

        [Fact]
        public void Verify_ScrollToElement()
        {
            Mock<IElement> mockElement = new();
            mockElement.Setup(e => e.ScrollToElement());

            mockElement.Object.ScrollToElement();

            mockElement.Verify(e => e.ScrollToElement(), Times.Once);
        }

        [Fact]
        public void Verify_GetPseudoelement()
        {
            Mock<IElement> mockElement = new();
            mockElement.Setup(e => e.GetPseudoelement("before", "content")).Returns("pseudo-content");

            string pseudo = mockElement.Object.GetPseudoelement("before", "content");

            pseudo.Should().Be("pseudo-content");
            mockElement.Verify(e => e.GetPseudoelement("before", "content"), Times.Once);
        }

        #endregion

        #region Element Interaction

        [Fact]
        public void Verify_CanGetChildElement()
        {
            Mock<IElement> mockElement = new();
            Mock<IElement> mockChild = new();
            mockElement.Setup(e => e.GetElement(It.IsAny<ILocator>())).Returns(mockChild.Object);

            IElement child = mockElement.Object.GetElement(Locator.Id("test"));

            child.Should().NotBeNull();
            mockElement.Verify(e => e.GetElement(It.IsAny<ILocator>()), Times.Once);
        }

        [Fact]
        public void Verify_CanGetChildElements()
        {
            Mock<IElement> mockElement = new();
            Mock<IElement> mockChild = new();
            List<IElement> mockChildren = new() { mockChild.Object };
            mockElement.Setup(e => e.GetElements(It.IsAny<ILocator>())).Returns(mockChildren);

            List<IElement> children = mockElement.Object.GetElements(Locator.Class("test"));

            children.Should().NotBeNull();
            children.Count.Should().Be(1);
            mockElement.Verify(e => e.GetElements(It.IsAny<ILocator>()), Times.Once);
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
        public void Verify_ChildElementExist(bool expected, bool checkInteractable, int time)
        {
            TimeSpan span = time == 0 ? default : TimeSpan.FromSeconds(time);
            Mock<IElement> mockElement = new();
            mockElement.Setup(e => e.ElementExist(It.IsAny<ILocator>(), checkInteractable, span)).Returns(expected);

            bool result = mockElement.Object.ElementExist(Locator.Class("test"), checkInteractable, span);

            result.Should().Be(expected);
            mockElement.Verify(e => e.ElementExist(It.IsAny<ILocator>(), checkInteractable, span), Times.Once);
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
        public void Verify_ChildElementsExist(bool expected, bool checkInteractable, int time)
        {
            TimeSpan span = time == 0 ? default : TimeSpan.FromSeconds(time);
            Mock<IElement> mockElement = new();
            mockElement.Setup(e => e.ElementsExist(It.IsAny<ILocator>(), checkInteractable, span)).Returns(expected);

            bool result = mockElement.Object.ElementsExist(Locator.Class("test"), checkInteractable, span);

            result.Should().Be(expected);
            mockElement.Verify(e => e.ElementsExist(It.IsAny<ILocator>(), checkInteractable, span), Times.Once);
        }

        #endregion

    }
}