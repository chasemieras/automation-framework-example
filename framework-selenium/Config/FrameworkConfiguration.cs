namespace framework_selenium.Config
{
    public class FrameworkConfiguration
    {
        private static readonly ThreadLocal<FrameworkConfiguration> _config = new();
        private static readonly object _lock = new();
        
        public static FrameworkConfiguration Confg
        {
            get
            {
                lock (_lock)
                {
                    _config.Value ??= new FrameworkConfiguration();

                    return _config.Value;
                }
            }
        }

        private FrameworkConfiguration() 
        {
            //driver type
            //environment type

            //tablet sizing
            //mobile sizing
            //headless mode
            //timeout for elements
            //timeout for pages
        }

    }
}
