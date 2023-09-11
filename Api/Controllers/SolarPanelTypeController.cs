using Api.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SolarPanelTypeController : ControllerBase
{
    private readonly ILogger<SolarPanelTypeController> _logger;

    private readonly IMongoCollection<SolarPanelType> _db;
    public SolarPanelTypeController(ILogger<SolarPanelTypeController> logger)
    {
        _logger = logger;

        var client = new MongoClient("mongodb://localhost:27017");
        var database = client.GetDatabase("Enpal");
        database.DropCollection("SolarPanelType");
        _db = database.GetCollection<SolarPanelType>("SolarPanelType"); 
        Initialize();
    }

    private void Initialize() {
        _db.InsertOne(new SolarPanelType{
            Id = "0",
            Name = "Monocrystalline Silicon"
        });
        _db.InsertOne(new SolarPanelType{
            Id = "1",
            Name = "Polycrystalline Silicon"
        });
        _db.InsertOne(new SolarPanelType{
            Id = "2",
            Name = "Thin-Filmn"
        });
        _db.InsertOne(new SolarPanelType{
            Id = "3",
            Name = "Bifacial"
        });
        _db.InsertOne(new SolarPanelType{
            Id = "4",
            Name = "Organic"
        });
    }

    [HttpGet(Name = "SolarPanelType")]
    public IEnumerable<SolarPanelType> Get()
    {
        return _db.AsQueryable();
    }
}
