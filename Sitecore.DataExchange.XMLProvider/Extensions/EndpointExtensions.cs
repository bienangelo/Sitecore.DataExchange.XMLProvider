using Sitecore.DataExchange.Models;
using Sitecore.DataExchange.Providers.XMLSystem.Plugins;

namespace Sitecore.DataExchange.Providers.XMLSystem.Extensions
{
    public static class EndpointExtensions
    {
        //This extension method makes it easier to access the plugin when it is needed.

        //Providing extension methods to access a plugin is a best practice to consider when implementing a provider for Data Exchange Framework.

        public static XMLSystemSettings GetXMLSystemSettings(this Endpoint endpoint)
        {
            return endpoint.GetPlugin<XMLSystemSettings>();
        }        
    }
}
