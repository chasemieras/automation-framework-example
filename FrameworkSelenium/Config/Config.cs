using FrameworkSelenium.Enums;

namespace FrameworkSelenium.Config
{
    public class Config
    {
        public BrowserType BrowserType { get; set; }
        public EnvironmentType EnvironmentType { get; set; }

        public int DesktopHeight { get; set; }
        public int DesktopWidth { get; set; }

        public int MobileHeight { get; set; }
        public int MobileWidth { get; set; }

        public int TabletHeight { get; set; }
        public int TabletWidth { get; set; }

        public int DefaultElementTimeout { get; set; }
        public int DefaultPageTimeout { get; set; }
    }
}
