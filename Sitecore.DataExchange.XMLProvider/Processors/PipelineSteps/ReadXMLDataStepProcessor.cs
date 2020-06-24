using Sitecore.DataExchange.Attributes;
using Sitecore.DataExchange.Contexts;
using Sitecore.DataExchange.Models;
using Sitecore.DataExchange.Plugins;
using Sitecore.DataExchange.Processors.PipelineSteps;
using Sitecore.DataExchange.Providers.XMLSystem.Extensions;
using Sitecore.DataExchange.Providers.XMLSystem.Plugins;
using Sitecore.Services.Core.Diagnostics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace Sitecore.DataExchange.Providers.XMLSystem.Processors.PipelineSteps
{
    [RequiredEndpointPlugins(typeof(XMLSystemSettings))]
    public class ReadXMLDataStepProcessor : BaseReadDataStepProcessor
    {
        public ReadXMLDataStepProcessor()
        {
        }

        protected override void ReadData(Endpoint endpoint, PipelineStep pipelineStep, PipelineContext pipelineContext, ILogger logger)
        {
            if (endpoint == null)
            {
                throw new ArgumentNullException(nameof(endpoint));
            }
            if (pipelineStep == null)
            {
                throw new ArgumentNullException(nameof(pipelineStep));
            }
            if (pipelineContext == null)
            {
                throw new ArgumentNullException(nameof(pipelineContext));
            }

            //get the file path from the plugin on the endpoint
            var settings = endpoint.GetXMLSystemSettings();
            if (settings == null)
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(settings.XMLPath))
            {
                logger.Error(
                    "No path is specified on the endpoint. " +
                    "(pipeline step: {0}, endpoint: {1})",
                    pipelineStep.Name, endpoint.Name);
                return;
            }
            
            //if the path is relative, the base directory is used to build an 
            //absolute path so that when this code runs on the Sitecore server, 
            //relative paths will be based on the webroot
            var path = settings.XMLPath;

            Uri uriResult;
            bool result = Uri.TryCreate(path, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            if (!result)
            {
                if (!Path.IsPathRooted(path))
                {
                    path = string.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, path);
                }

                if (!File.Exists(path))
                {
                    logger.Error(
                        "The path specified on the endpoint does not exist. " +
                        "(pipeline step: {0}, endpoint: {1}, path: {2})",
                        pipelineStep.Name, endpoint.Name, path);
                    return;
                }
            }
           
            var lines = new List<string[]>();
            XmlDocument document = new XmlDocument();
            document.Load(path);
            XmlNodeList xmlNodeList = document.GetElementsByTagName(settings.XMLNodeName);
            XmlNode[] nodeArray = xmlNodeList.Cast<XmlNode>().ToArray();

            for (int i = 0; i < xmlNodeList.Count; i++)
            {
                List<string> strs = new List<string>();
                for (int j = 0; j < xmlNodeList[i].ChildNodes.Count; j++)
                {
                    strs.Add(xmlNodeList[i].ChildNodes[j].InnerText);
                }
                lines.Add(strs.ToArray());
            }
            
            //add the data that was read from the xml file to a plugin
            var dataSettings = new IterableDataSettings(nodeArray);
            logger.Info(
                "{0} rows were read from the file. (pipeline step: {1}, endpoint: {2})",
                lines.Count, pipelineStep.Name, endpoint.Name);

            //add the plugin to the pipeline context
            pipelineContext.AddPlugin(dataSettings);
        }
    }
}
