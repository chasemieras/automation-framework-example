using System.Drawing;

namespace FrameworkSelenium.Selenium.Elements
{
    public interface IElement : IElementFinder
    {

        #region Variables

        bool IsDisplayed { get; }
        bool IsEnabled { get; }
        bool IsInteractable { get; }
        bool IsSelected { get; }
        string TagName { get; }
        Point Location { get; }
        string CssValue { get; }
        string Text { get; }

        #endregion

        #region Attributes

        string GetAttribute(string attribute);
        string Src {get;}
        string Href { get; }
        string Value { get; }
        string ClassName { get; }
        string Id { get; }
        string Name { get; }
        string Title { get; }
        string InnerHtml { get; }
        string OuterHtml { get; }
        string Target { get; }
        string Style { get; }
        string Alt { get; }

        #endregion
        
        void Click();
        void SendKeys(string text);
        void Clear();
        void Submit();
        void ScrollToElement();
        
        void ExtractText();

        public string GetPseudoelement();

        //todo add HTTP checker for link
        //attrubte exists & atrribute exists and not empty

    }
}
