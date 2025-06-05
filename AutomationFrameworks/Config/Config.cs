using System.Text.Json;
using AutomationFramework.Enums;

namespace AutomationFramework.Config
{

    /// <summary>
    /// The class used by <see cref="JsonSerializer.Deserialize{TValue}(string, JsonSerializerOptions)"/>
    /// to deserialize the <see cref="FrameworkConfiguration.Config" /> file.
    /// </summary>
    public class Config
    {

        /// <summary>
        /// The type of driver that is used during testing
        /// </summary>
        public DriverType DriverType { get; set; }

        /// <summary>
        /// The type of environment that the tests are run in
        /// </summary>
        public EnvironmentType EnvironmentType { get; set; }

        /// <summary>
        /// The height of the desktop screen size in pixels
        /// </summary>
        public int DesktopHeight { get; set; }

        /// <summary>
        /// The width of the desktop screen size in pixels
        /// </summary>
        public int DesktopWidth { get; set; }

        /// <summary>
        /// The height of the mobile screen size in pixels
        /// </summary>
        public int MobileHeight { get; set; }

        /// <summary>
        /// The width of the mobile screen size in pixels
        /// </summary>
        public int MobileWidth { get; set; }

        /// <summary>
        /// The height of the tablet screen size in pixels
        /// </summary>
        public int TabletHeight { get; set; }
        /// <summary>
        /// The width of the tablet screen size in pixels
        /// </summary>
        public int TabletWidth { get; set; }

        /// <summary>
        /// The default timeout for elements in seconds
        /// </summary>
        public int DefaultElementTimeout { get; set; }
        /// <summary>
        /// The default timeout for pages in seconds
        /// </summary>
        public int DefaultPageTimeout { get; set; }
    }
}
