using System.Text.Json;
using System.Text.Json.Serialization;

namespace Neptune.Common.JsonConverters;

public class DateTimeConverter : JsonConverter<DateTime>
{
    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert == typeof(DateTime);
    }

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.GetDateTime().ToUniversalTime();
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToUniversalTime());
    }
}

public class NullableDateTimeConverter : JsonConverter<DateTime?>
{
    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert == typeof(DateTime?);
    }

    public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
        {
            return null;
        }

        //Check for empty/whitespace string
        if (reader.TokenType == JsonTokenType.String)
        {
            var valueAsString = reader.GetString();
            if (string.IsNullOrWhiteSpace(valueAsString))
            {
                return null;
            }
        }

        return reader.GetDateTime().ToUniversalTime();
    }

    public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
    {
        if (value.HasValue)
        {
            writer.WriteStringValue(value.Value.ToUniversalTime());
        }
        else
        {
            writer.WriteNullValue();
        }
    }
}