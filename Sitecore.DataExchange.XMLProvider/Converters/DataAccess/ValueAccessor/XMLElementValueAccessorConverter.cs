using Sitecore.DataExchange.Attributes;
using Sitecore.DataExchange.Converters.DataAccess.ValueAccessors;
using Sitecore.DataExchange.DataAccess;
using Sitecore.DataExchange.DataAccess.Readers;
using Sitecore.DataExchange.DataAccess.Writers;
using Sitecore.DataExchange.Providers.XMLSystem.Converters.DataAccess.Reader;
using Sitecore.DataExchange.Providers.XMLSystem.Models.ItemModels.DataAccess;
using Sitecore.DataExchange.Repositories;
using Sitecore.Services.Core.Model;
using System;

namespace Sitecore.DataExchange.Providers.XMLSystem.Converters.DataAccess.ValueAccessors
{
    [SupportedIds(new string[] { "{9B88A6C5-C38E-4A41-9798-17AC92F3BD20}", "{209047C8-7900-450B-A6F3-AA30EAA00DEB}" })]
    public class XMLElementValueAccessorConverter : ValueAccessorConverter
    {
        //public IValueWriter ValueWriter { get; set; }
        //public IValueReader ValueReader { get; set; }

        //the id from the value accessor template you created named XML Element Value Accessor.
        //private static readonly Guid TemplateId = Guid.Parse("{9B88A6C5-C38E-4A41-9798-17AC92F3BD20}");

        public XMLElementValueAccessorConverter(IItemModelRepository repository) : base(repository)
        {
            //this.SupportedTemplateIds.Add(TemplateId);
        }

        //public override IValueAccessor Convert(ItemModel source)
        //{
        //    var accessor = base.Convert(source);
        //    if (accessor == null)
        //    {
        //        return null;
        //    }
        //    var elementName = base.GetStringValue(source, XMLElementValueAccessorItemModel.ElementName);
        //    if (String.IsNullOrEmpty(elementName))
        //    {
        //        return null;
        //    }

        //    if (accessor.ValueReader == null)
        //    {
        //        accessor.ValueReader = new XMLElementValueReader(elementName);
        //    }
        //    if (accessor.ValueWriter == null)
        //    {
        //        accessor.ValueWriter = new PropertyValueWriter(elementName);
        //    }
        //    return accessor;
        //}

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

        //protected override ConvertResult<IValueAccessor> ConvertSupportedItem(ItemModel source)
        //{
        //    //var accessor = base.Convert(source);
        //    //if (accessor == null)
        //    //{
        //    //    return null;
        //    //}

        //    var fieldName = base.GetStringValue(source, XMLElementValueAccessorItemModel.ElementName);
        //    if (string.IsNullOrEmpty(fieldName))
        //    {
        //        return null;
        //    }

        //    //accessor.ValueReader = this.GetValueReader(source) ?? new XMLElementValueReader(fieldName);

        //    //ValueWriter = this.GetValueWriter(source);
        //    //if (ValueWriter == null)
        //    //{
        //    //    ValueWriter = new PropertyValueWriter(fieldName);
        //    //}

        //    return this.PositiveResult((IValueAccessor)new ValueAccessor() {
        //        ValueReader = this.GetValueReader(source) ?? new XMLElementValueReader(fieldName)
        //    });

        //    //return accessor;
        //}

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
