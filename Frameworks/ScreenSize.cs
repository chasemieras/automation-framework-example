using FrameworkSelenium.Config;

namespace FrameworkSelenium
{
    /// <summary>
    /// Represents the screen size of the browser window
    /// </summary>
    public class ScreenSize
    {

        /// <summary>
        /// An enum to represent the sizes that the framework can switch to
        /// </summary>
        public enum SizeType
        {
            /// <summary>
            /// Represents a desktop screen size, usually around the size of a laptop
            /// </summary>
            Desktop,

            /// <summary>
            /// Represents a mobile screen size, usually around the size of a smartphone
            /// </summary>
            Mobile,

            /// <summary>
            /// Represents a tablet screen size, usually around the size of a tablet device
            /// </summary>
            Tablet
        }

        /// <summary>
        /// The height of the screen in pixels
        /// </summary>
        public int Height { get; }

        /// <summary>
        /// The width of the screen in pixels
        /// </summary>
        public int Width { get; }

        /// <summary>
        /// The type of screen size
        /// </summary>
        public SizeType Type { get; }

        private ScreenSize(int height, int width, SizeType type)
        {
            Height = height;
            Width = width;
            Type = type;
        }

        /// <summary>
        /// Represents the desktop screen size based on the <see cref="FrameworkConfiguration.Config"/>
        /// </summary>
        public static ScreenSize Desktop => new(FrameworkConfiguration.Config.DesktopHeight, FrameworkConfiguration.Config.DesktopWidth, SizeType.Desktop);

        /// <summary>
        /// Represents the mobile screen size based on the <see cref="FrameworkConfiguration.Config"/>
        /// </summary>
        public static ScreenSize Mobile => new(FrameworkConfiguration.Config.MobileHeight, FrameworkConfiguration.Config.MobileWidth, SizeType.Mobile);

        /// <summary>
        /// Represents the tablet screen size based on the <see cref="FrameworkConfiguration.Config"/>
        /// </summary>
        public static ScreenSize Tablet => new(FrameworkConfiguration.Config.TabletHeight, FrameworkConfiguration.Config.TabletWidth, SizeType.Tablet);
    }
}
