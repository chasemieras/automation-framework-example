using System;
using System.Collections.Generic;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace AutomationFramework.Attributes
{
    /// <summary>
    /// Discovers the [Headless] trait and adds "Type: Headless" to test traits
    /// </summary>
    public class HeadlessTraitDiscoverer : ITraitDiscoverer
    {
        /// <inheritdoc/>
        public IEnumerable<KeyValuePair<string, string>> GetTraits(IAttributeInfo traitAttribute)
        {
            yield return new KeyValuePair<string, string>("Type", "Headless");
        }
    }

    /// <summary>
    /// Attribute to indicate that a test will run in a tablet format
    /// </summary>
    [TraitDiscoverer("AutomationFramework.Attributes.HeadlessTraitDiscoverer", "AutomationFramework")]
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public class Headless : Attribute, ITraitAttribute;

}