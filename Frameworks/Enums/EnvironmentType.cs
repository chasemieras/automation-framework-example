namespace FrameworkSelenium.Enums
{
    /// <summary>
    /// An enum that represents the kind of environment that a test will run in
    /// </summary>
    public enum EnvironmentType
    {
        /// <summary>
        /// On your machine
        /// </summary>
        Local,

        /// <summary>
        /// On a CI/CD pipeline on GitLabs or Azure DevOps
        /// </summary>
        Remote
    }
}
