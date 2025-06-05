using System;
using System.Collections.Generic;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace AutomationFramework.Attributes
{
    /// <summary>
    /// Discovers the [Mobile] trait and adds "Type: Mobile" to test traits.
    /// </summary>
    public class MobileTraitDiscoverer : ITraitDiscoverer
    {
        /// <inheritdoc/>
        public IEnumerable<KeyValuePair<string, string>> GetTraits(IAttributeInfo traitAttribute)
        {
            yield return new KeyValuePair<string, string>("Type", "Mobile");
        }
    }

    /// <summary>
    /// Attribute to indicate that a test will run on a mobile setting.
    /// </summary>
    [TraitDiscoverer("AutomationFramework.Attributes.MobileTraitDiscoverer", "AutomationFramework")]
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public class Mobile : Attribute, ITraitAttribute;
}