using Sitecore.DataExchange.Attributes;
using Sitecore.DataExchange.Converters.Endpoints;
using Sitecore.DataExchange.Models;
using Sitecore.DataExchange.Providers.XMLSystem.Models.ItemModels.Endpoints;
using Sitecore.DataExchange.Providers.XMLSystem.Plugins;
using Sitecore.DataExchange.Repositories;
using Sitecore.DataExchange.XMLProvider;
using Sitecore.Services.Core.Model;

namespace Sitecore.DataExchange.Providers.XMLSystem.Converters.Endpoints
{
    //By inheriting from BaseEndpointConverter<ItemModel> you get access to a number of methods 
    //that facilitate reading values from fields on a Sitecore item.
    [SupportedIds(Constants.XmlProvider.XmlEndpointTemplateId)]
    public class XMLSystemEndpointConverter : BaseEndpointConverter
    {
        //the id from the endpoint template you created named XML Endpoint.
        public XMLSystemEndpointConverter(IItemModelRepository repository) : base(repository)
        {
            //identify the template an item must be based
            //on in order for the converter to be able to
            //convert the item
        }
        protected override void AddPlugins(ItemModel source, Endpoint endpoint)
        {
            ////create the plugin
            var settings = new XMLSystemSettings
            {
                XMLPath = base.GetStringValue(source, XMLSystemEndpointItemModel.XMLPath),
                XMLNodeName = base.GetStringValue(source, XMLSystemEndpointItemModel.XMLNodeName)
            };

            //add the plugin to the endpoint     
            endpoint.AddPlugin(settings);
        }
    }
}
