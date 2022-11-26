using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Neptune.Web.Common;

public class IntConverter : JsonConverter<int>
{

    public IntConverter()
    { }

    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(int);
    }

    public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.TokenType == JsonTokenType.String ? int.Parse(reader.GetString()) : reader.GetInt32();
    }

    public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options) => writer.WriteNumberValue(value);
}