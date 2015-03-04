using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using EPiServer.Find.Api;

namespace Random2Find.Api
{
    public class ScriptSortingConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var sorting = (ScriptSorting) value;

            writer.WriteStartObject();
            writer.WritePropertyName("_script");

            writer.WriteStartObject();
            writer.WritePropertyName("script");
            writer.WriteValue(sorting.Script);

            writer.WritePropertyName("type");
            writer.WriteValue(sorting.Type.ToString().ToLower());

            writer.WritePropertyName("lang");
            writer.WriteValue(sorting.Language.ToString().ToLower());

            if (sorting.Parameters.Any())
            {
                writer.WritePropertyName("params");
                writer.WriteStartObject();
                foreach (var parameter in sorting.Parameters)
                {
                    writer.WritePropertyName(parameter.Key);
                    serializer.Serialize(writer, parameter.Value);
                }
                writer.WriteEndObject();
            }

            if (sorting.Order.HasValue)
            {
                writer.WritePropertyName("order");
                if (sorting.Order.Value == SortOrder.Ascending)
                {
                    writer.WriteValue("asc");
                }
                else
                {
                    writer.WriteValue("desc");
                }
            }
            writer.WriteEndObject();
            writer.WriteEndObject();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return null;
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(ScriptSorting).IsAssignableFrom(objectType);
        }
    }
}
