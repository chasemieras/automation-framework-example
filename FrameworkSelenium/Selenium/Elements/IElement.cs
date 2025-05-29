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
        Point Location { get; }

        #endregion

        #region Click Interactions

        void Click();

        void RightClick();

        #endregion

        #region Attributes

        string GetAttribute(string attribute);
        bool AttributeExists(string attribute, bool checkIfEmpty = true);
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
        string AltText { get; }
        string TagName { get; }
        string Class { get; }
        string Rel { get; }
        bool Checked { get; }
        string Styling { get; }


        #endregion

        #region Text Interaction

        string Text { get; }
        string ExtractString { get; }
        void SendKeys(string text);
        void SendEnter();
        void Clear();
        void Submit();

        #endregion

        #region Other Methods

        void ScrollToElement();

        void ExtractText();

        public string GetPseudoelement();

        #endregion

        //todo add HTTP checker for link | Shadowroot | Select Element

    }
}
