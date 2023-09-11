using System.Text.Json.Serialization;

namespace App.Models;

public class Customer
{
    public string Id { get; set; }
    public required string Email { get; set; }
    public required string Name { get; set; }
}
