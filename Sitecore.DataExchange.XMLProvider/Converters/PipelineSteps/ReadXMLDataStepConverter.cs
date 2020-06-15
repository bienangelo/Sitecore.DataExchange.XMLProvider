using Sitecore.DataExchange.Attributes;
using Sitecore.DataExchange.Converters.PipelineSteps;
using Sitecore.DataExchange.Models;
using Sitecore.DataExchange.Plugins;
using Sitecore.DataExchange.Providers.XMLSystem.Models.PipelineSteps;
using Sitecore.DataExchange.Repositories;
using Sitecore.Services.Core.Model;
using System;

namespace Sitecore.DataExchange.Providers.XMLSystem.Converters.PipelineSteps
{
    [SupportedIds("{00191A02-F1B4-406C-B3CB-60568CCDD6D3}")]
    public class ReadXMLDataStepConverter : BasePipelineStepConverter
    {
        // the id from the pipeline step template you created named Read XML Data Pipeline Step.
        //private static readonly Guid TemplateId = Guid.Parse("{00191A02-F1B4-406C-B3CB-60568CCDD6D3}");
        public ReadXMLDataStepConverter(IItemModelRepository repository) : base(repository)
        {
            //this.SupportedTemplateIds.Add(TemplateId);
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
