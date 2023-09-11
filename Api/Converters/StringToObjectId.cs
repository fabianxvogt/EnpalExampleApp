
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using MongoDB.Bson;

namespace Api.Converters;

public class StringToObjectId : JsonConverter<ObjectId>
{
    public override bool CanConvert(Type objectType) => objectType == typeof(ObjectId);

    public override ObjectId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            return new ObjectId(reader.GetString());
        }

        throw new JsonException($"Unexpected token type: {reader.TokenType}");
    }

    public override void Write(Utf8JsonWriter writer, ObjectId value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}