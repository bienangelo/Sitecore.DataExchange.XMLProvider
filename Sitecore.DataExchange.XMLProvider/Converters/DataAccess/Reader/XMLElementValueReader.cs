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

        //public CanReadResult CanRead(object source, DataAccessContext context)
        //{
        //    bool flag = source != null && source is XmlNode;
        //    return new CanReadResult()
        //    {
        //        CanReadValue = flag
        //    };
        //}

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

        //public ReadResult Read(object source, DataAccessContext context)
        //{
        //    var flag = false;
        //    object readValue = (object)null;
        //    var xmlNode = source as XmlNode;
        //    var elements = ElementName.Split('|');


        //    if (xmlNode != null && elements.Length == 2)
        //    {
        //        var baseElement = elements[0];
        //        var fallbackElement = elements[1];
        //        var fallbackReadValue = string.Empty;

        //        for (int i = 0; i < xmlNode.ChildNodes.Count; i++)
        //        {
        //            if (baseElement.Equals(xmlNode.ChildNodes[i].Name, StringComparison.OrdinalIgnoreCase))
        //            {
        //                readValue = xmlNode.ChildNodes[i].InnerText;
        //                flag = true;
        //            }
        //            else if (fallbackElement.Equals(xmlNode.ChildNodes[i].Name, StringComparison.OrdinalIgnoreCase))
        //            {
        //                fallbackReadValue = xmlNode.ChildNodes[i].InnerText;
        //                flag = true;
        //            }
        //        }

        //        if (String.IsNullOrEmpty(readValue.ToString()))
        //        {
        //            readValue = fallbackReadValue;
        //        }
        //    }

        //    return new ReadResult(DateTime.Now)
        //    {
        //        WasValueRead = flag,
        //        ReadValue = readValue
        //    };
        //}
    }
}
