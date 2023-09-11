using MongoDB.Bson;
using Api.Converters;
using System.Text.Json.Serialization;

namespace Api.Models;

public class SolarPanelType
{
    public required string Id { get; set; }
    public required string Name { get; set; }
}