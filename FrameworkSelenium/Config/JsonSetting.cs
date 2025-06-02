using System.Text.Json;
using System.Text.Json.Serialization;

namespace FrameworkSelenium.Config
{
    /// <summary>
    /// Json settings for reading json files
    /// </summary>
    public static class JsonSettings
    {
        /// <summary>
        /// Specific settings for <see cref="FrameworkConfiguration"/>
        /// </summary>
        public static readonly JsonSerializerOptions FrameworkConfigSettings = new()
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter() }
        };
    }

}
