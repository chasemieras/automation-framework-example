using System;
using System.Collections.Generic;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace AutomationFramework.Attributes
{
    /// <summary>
    /// Discovers the [Tablet] trait and adds "Type: Tablet" to test traits
    /// </summary>
    public class TabletTraitDiscoverer : ITraitDiscoverer
    {
        /// <inheritdoc/>
        public IEnumerable<KeyValuePair<string, string>> GetTraits(IAttributeInfo traitAttribute)
        {
            yield return new KeyValuePair<string, string>("Type", "Tablet");
        }
    }

    /// <summary>
    /// Attribute to indicate that a test will run in a tablet format
    /// </summary>
    [TraitDiscoverer("AutomationFramework.Attributes.TabletTraitDiscoverer", "AutomationFramework")]
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public class Tablet : Attribute, ITraitAttribute;
}