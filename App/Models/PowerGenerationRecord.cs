using System.Text.Json.Serialization;

namespace App.Models;

public class PowerGenerationRecord
{
    public string Id { get; set; }
    public required DateOnly Date { get; set; }
    public required string CustomerId { get; set; }
    public required string SolarPanelId { get; set; }

    public required float OutputWatt { get; set; }
}
