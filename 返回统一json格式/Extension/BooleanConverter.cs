using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

/// <summary>
/// 这个拓展的目的是让返回的bool类型的值为0，1
/// </summary>
public class BooleanConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(bool) || objectType == typeof(Nullable<bool>);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        if (reader.Value == null)
            return null;

        return Convert.ToBoolean(reader.Value);
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        if (value == null)
            writer.WriteNull();
        else
        {
            UInt32 val = Convert.ToUInt32(Convert.ToBoolean(value));
            writer.WriteValue(val);
        }
    }
}

