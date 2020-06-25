using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.DataExchange.XMLProvider
{
    public static class Constants
    {
        public static class XmlProvider
        {
            public const string ReadXmlDataPipelineStepTemplateId = "{00191A02-F1B4-406C-B3CB-60568CCDD6D3}";
            public const string XmlEndpointTemplateId = "{CC05EF8C-F4C8-4EB9-A39A-8E71F978C1DD}";
            public const string XmlElementValueAccessorTemplateId = "{9B88A6C5-C38E-4A41-9798-17AC92F3BD20}";

            public const string ReadXmlDataPipelineStepEndpointFromField = "EndpointFrom";
            public const string XmlEndpointXmlNodeName = "XMLNodeName";
            public const string XmlEndpointXmlPath = "XMLPath";
            public const string XmlElementValueAccessorElementName = "ElementName";
        }
    }
}