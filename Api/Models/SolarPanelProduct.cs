using MongoDB.Bson;
using Api.Converters;
using System.Text.Json.Serialization;

namespace Api.Models;

public class SolarPanelProduct
{
    [JsonConverter(typeof(StringToObjectId))]
    public ObjectId Id { get; set; }

    public required string Name { get; set; }

    public required float MaxOutputWatt { get; set; }

    public required string PanelTypeId { get; set; }
}
