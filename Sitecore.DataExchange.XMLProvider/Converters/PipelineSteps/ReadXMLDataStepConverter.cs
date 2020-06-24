using Sitecore.DataExchange.Attributes;
using Sitecore.DataExchange.Converters.PipelineSteps;
using Sitecore.DataExchange.Models;
using Sitecore.DataExchange.Plugins;
using Sitecore.DataExchange.Providers.XMLSystem.Models.PipelineSteps;
using Sitecore.DataExchange.Repositories;
using Sitecore.DataExchange.XMLProvider;
using Sitecore.Services.Core.Model;

namespace Sitecore.DataExchange.Providers.XMLSystem.Converters.PipelineSteps
{
    [SupportedIds(Constants.XmlProvider.ReadXmlDataPipelineStepTemplateId)]
    public class ReadXMLDataStepConverter : BasePipelineStepConverter
    {
        // the id from the pipeline step template you created named Read XML Data Pipeline Step.
        public ReadXMLDataStepConverter(IItemModelRepository repository) : base(repository)
        {
        }

        protected override void AddPlugins(ItemModel source, PipelineStep pipelineStep)
        {
            AddEndpointSettings(source, pipelineStep);
        }

        private void AddEndpointSettings(ItemModel source, PipelineStep pipelineStep)
        {
            //create the endpoint settings
            var settings = new EndpointSettings();

            //populate the endpoint settings using values from the item
            var endpointFrom = base.ConvertReferenceToModel<Endpoint>(source, ReadXMLDataStepItemModel.EndpointFrom);
            if (endpointFrom != null)
            {
                settings.EndpointFrom = endpointFrom;
            }

            //add the endpoint settings to the pipeline step    
            pipelineStep.AddPlugin(settings);
        }
    }
}
