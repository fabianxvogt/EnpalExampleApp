using MongoDB.Bson;
using Api.Converters;
using System.Text.Json.Serialization;

namespace Api.Models;

public class Customer
{
    [JsonConverter(typeof(StringToObjectId))]
    public ObjectId Id { get; set; }
    public required string Email { get; set; }
    public required string Name { get; set; }
}
