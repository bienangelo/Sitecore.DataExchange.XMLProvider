using Sitecore.DataExchange.DataAccess;
using System;
using System.Xml;

namespace Sitecore.DataExchange.Providers.XMLSystem.Converters.DataAccess.Reader
{
    public class XMLElementValueReader : IValueReader
    {
        public readonly string ElementName;

        public XMLElementValueReader(string elementName)
        {
            this.ElementName = elementName;
        }
        public ReadResult CanRead(object source, DataAccessContext context)
        {
            bool flag = source != null && source is XmlNode;
            return new ReadResult(DateTime.Now)
            {
                ReadValue = source,
                WasValueRead = flag,
            };
        }

        public ReadResult Read(object source, DataAccessContext context)
        {
            var flag = false;
            object readValue = (object)null;
            var xmlNode = source as XmlNode;

            if (xmlNode != null)
            {
                for (int i = 0; i < xmlNode.ChildNodes.Count; i++)
                {
                    if (ElementName.Equals(xmlNode.ChildNodes[i].Name, StringComparison.OrdinalIgnoreCase))
                    {
                        readValue = xmlNode.ChildNodes[i].InnerText;
                        flag = true;
                        break;
                    }
                }
            }

            return new ReadResult(DateTime.Now)
            {
                WasValueRead = flag,
                ReadValue = readValue
            };
        }
    }
}
