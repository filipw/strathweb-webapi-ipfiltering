using System.Configuration;

namespace Strathweb.WebApi.IpFiltering.Configuration
{
    [ConfigurationCollection(typeof(IpAddressElement))]
    public class IpAddressElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new IpAddressElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((IpAddressElement)element).Address;
        }
    }
}