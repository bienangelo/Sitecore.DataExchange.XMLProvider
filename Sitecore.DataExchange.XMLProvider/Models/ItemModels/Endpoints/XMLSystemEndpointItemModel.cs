using Sitecore.DataExchange.XMLProvider;
using Sitecore.Services.Core.Model;

namespace Sitecore.DataExchange.Providers.XMLSystem.Models.ItemModels.Endpoints
{
    //This class defines constants for the names of the fields from the endpoint template.
    //These field names are referenced by the converter.
    //In order for the converter to be able to run inside or outside of the Sitecore server, 
    //the type Sitecore.Services.Core.Model.ItemModel is used to represent 
    //Sitecore items.Fields on this type are accessed by name.
    public class XMLSystemEndpointItemModel : ItemModel
    {        
        public const string XMLNodeName = Constants.XmlProvider.XmlEndpointXmlNodeName;
        public const string XMLPath = Constants.XmlProvider.XmlEndpointXmlPath;
    }
}
