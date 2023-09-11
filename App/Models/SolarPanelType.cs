using System.Text.Json.Serialization;

namespace App.Models;

public class SolarPanelType
{
    public required string Id { get; set; }
    public required string Name { get; set; }
}