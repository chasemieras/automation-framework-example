using FrameworkSelenium.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkSelenium
{
    public class ScreenSize
    {
        public enum SizeType
        {
            Desktop,
            Mobile,
            Tablet
        }

        public int Height { get; }
        public int Width { get; }
        public SizeType Type { get; }

        private ScreenSize(int height, int width, SizeType type) 
        { 
            Height = height;
            Width = width;
            Type = type;
        }

        public static ScreenSize Desktop => new(FrameworkConfiguration.Config.DesktopHeight, FrameworkConfiguration.Config.DesktopWidth, SizeType.Desktop);

        public static ScreenSize Mobile => new(FrameworkConfiguration.Config.MobileHeight, FrameworkConfiguration.Config.MobileWidth, SizeType.Mobile);

        public static ScreenSize Tablet => new(FrameworkConfiguration.Config.TabletHeight, FrameworkConfiguration.Config.TabletWidth, SizeType.Tablet);
    }
}
