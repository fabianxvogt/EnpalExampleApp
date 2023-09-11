using MongoDB.Bson;
using Api.Converters;
using System.Text.Json.Serialization;

namespace Api.Models;

public class PowerGenerationRecord
{
    [JsonConverter(typeof(StringToObjectId))]
    public ObjectId Id { get; set; }
    public required DateOnly Date { get; set; }
    [JsonConverter(typeof(StringToObjectId))]
    public required ObjectId CustomerId { get; set; }
    [JsonConverter(typeof(StringToObjectId))]
    public required ObjectId SolarPanelId { get; set; }

    public required float OutputWatt { get; set; }
}
