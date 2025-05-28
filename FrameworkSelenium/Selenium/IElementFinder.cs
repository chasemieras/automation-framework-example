using FrameworkSelenium.Selenium.Locator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkSelenium.Selenium.Elements
{
    public interface IElementFinder
    {

        IElement GetElement(ILocator locator);
        
        List<IElement> GetElements(ILocator locator);

        bool ElementExist(ILocator locator);

        bool ElementsExist(ILocator locator);

    }
}
