using System.Text.Json.Serialization;

namespace App.Models;

public class SolarPanelProduct
{
    public string Id { get; set; }

    public required string Name { get; set; }

    public required float MaxOutputWatt { get; set; }

    public required string PanelTypeId { get; set; }
}
