using FrameworkSelenium.Enums;
using OpenQA.Selenium;

public interface ILocator
    {
        By ToBy { get; }
        LocatorType Type { get; }
        string Value { get; }
}
