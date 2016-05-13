using System.Configuration;

namespace Strathweb.WebApi.IpFiltering.Configuration
{
    public class IpFilteringSection : ConfigurationSection
    {
        [ConfigurationProperty("ipAddresses", IsDefaultCollection = true)]
        public IpAddressElementCollection IpAddresses
        {
            get { return (IpAddressElementCollection)this["ipAddresses"]; }
            set { this["ipAddresses"] = value; }
        }
    }
}