using Sitecore.DataExchange.Attributes;
using Sitecore.DataExchange.Converters.DataAccess.ValueAccessors;
using Sitecore.DataExchange.DataAccess;
using Sitecore.DataExchange.DataAccess.Writers;
using Sitecore.DataExchange.Providers.XMLSystem.Converters.DataAccess.Reader;
using Sitecore.DataExchange.Providers.XMLSystem.Models.ItemModels.DataAccess;
using Sitecore.DataExchange.Repositories;
using Sitecore.DataExchange.XMLProvider;
using Sitecore.Services.Core.Model;
using System;

namespace Sitecore.DataExchange.Providers.XMLSystem.Converters.DataAccess.ValueAccessors
{
    [SupportedIds(new string[] { Constants.XmlProvider.XmlElementValueAccessorTemplateId, Constants.XmlProvider.XmlElementFallbackValueAccessorTemplateId })]
    public class XMLElementValueAccessorConverter : ValueAccessorConverter
    {
        public XMLElementValueAccessorConverter(IItemModelRepository repository) : base(repository)
        {
        }

        protected override IValueReader GetValueReader(ItemModel source)
        {
            var reader = base.GetValueReader(source);
            if (reader == null)
            {
                var fieldName = this.GetStringValue(source, XMLElementValueAccessorItemModel.ElementName);

                if (String.IsNullOrEmpty(fieldName)) return null;
                reader = new XMLElementValueReader(fieldName);
            }
            return reader;
        }

        protected override IValueWriter GetValueWriter(ItemModel source)
        {
            var writer = base.GetValueWriter(source);
            if (writer == null)
            {
                var fieldname = this.GetStringValue(source, XMLElementValueAccessorItemModel.ElementName);
                if (String.IsNullOrEmpty(fieldname)) return null;
                writer = new PropertyValueWriter(fieldname);
            }
            return writer;
        }
        
        protected override ConvertResult<IValueAccessor> ConvertSupportedItem(ItemModel source)
        {
            ConvertResult<IValueAccessor> convertResult = base.ConvertSupportedItem(source);

            if (!convertResult.WasConverted) return convertResult;

            string fieldValue = this.GetStringValue(source, XMLElementValueAccessorItemModel.ElementName);

            if (String.IsNullOrEmpty(fieldValue))
            {
                return this.NegativeResult(source, "The property name field must have a value specified.",
                    string.Format($"field: {(object)XMLElementValueAccessorItemModel.ElementName}"));
            }

            IValueAccessor convertedValue = convertResult.ConvertedValue;

            if (convertedValue == null)
            {
                return this.NegativeResult(source, "A null accessor was returned by the converter.", Array.Empty<string>());
            }

            if (convertedValue.ValueReader == null)
            {
                XMLElementValueReader xmlValueReader = new XMLElementValueReader(fieldValue);
                convertedValue.ValueReader = (IValueReader)xmlValueReader;
            }

            if (convertedValue.ValueWriter == null)
            {
                PropertyValueWriter propertyValueWriter = new PropertyValueWriter(fieldValue);
                convertedValue.ValueWriter = (IValueWriter)propertyValueWriter;
            }

            return this.PositiveResult(convertedValue);
        }
    }
}
