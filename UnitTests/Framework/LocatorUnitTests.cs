using Xunit;
using FluentAssertions;
using FrameworkSelenium.Selenium.Locators;
using FrameworkSelenium.Enums;
using OpenQA.Selenium;

namespace UnitTests.Framework
{
    public class LocatorUnitTests
    {
        [Fact]
        public void Id()
        {
            Locator locator = Locator.Id("myId");
            locator.Type.Should().Be(LocatorType.Id);
            locator.Value.Should().Be("myId");
            locator.ToBy.Should().BeEquivalentTo(By.Id("myId"));
        }

        [Fact]
        public void Name()
        {
            Locator locator = Locator.Name("myName");
            locator.Type.Should().Be(LocatorType.Name);
            locator.Value.Should().Be("myName");
            locator.ToBy.Should().BeEquivalentTo(By.Name("myName"));
        }

        [Fact]
        public void CssSelector()
        {
            Locator locator = Locator.CssSelector(".my-class");
            locator.Type.Should().Be(LocatorType.CssSelector);
            locator.Value.Should().Be(".my-class");
            locator.ToBy.Should().BeEquivalentTo(By.CssSelector(".my-class"));
        }

        [Fact]
        public void XPath()
        {
            Locator locator = Locator.XPath("//div[@id='test']");
            locator.Type.Should().Be(LocatorType.XPath);
            locator.Value.Should().Be("//div[@id='test']");
            locator.ToBy.Should().BeEquivalentTo(By.XPath("//div[@id='test']"));
        }

        [Fact]
        public void Class()
        {
            Locator locator = Locator.Class("my-class");
            locator.Type.Should().Be(LocatorType.Class);
            locator.Value.Should().Be("my-class");
            locator.ToBy.Should().BeEquivalentTo(By.ClassName("my-class"));
        }

        [Fact]
        public void TagName()
        {
            Locator locator = Locator.TagName("input");
            locator.Type.Should().Be(LocatorType.TagName);
            locator.Value.Should().Be("input");
            locator.ToBy.Should().BeEquivalentTo(By.TagName("input"));
        }

        [Fact]
        public void LinkText()
        {
            Locator locator = Locator.LinkText("Click here");
            locator.Type.Should().Be(LocatorType.LinkText);
            locator.Value.Should().Be("Click here");
            locator.ToBy.Should().BeEquivalentTo(By.LinkText("Click here"));
        }

        [Fact]
        public void PartialLinkText()
        {
            Locator locator = Locator.PartialLinkText("Click");
            locator.Type.Should().Be(LocatorType.PartialLinkText);
            locator.Value.Should().Be("Click");
            locator.ToBy.Should().BeEquivalentTo(By.PartialLinkText("Click"));
        }
    }
}