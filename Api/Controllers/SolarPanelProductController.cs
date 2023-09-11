using Api.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SolarPanelProductController : ControllerBase
{
    private readonly ILogger<SolarPanelProductController> _logger;

    private readonly IMongoCollection<SolarPanelProduct> _db;
    public SolarPanelProductController(ILogger<SolarPanelProductController> logger)
    {
        _logger = logger;

        var client = new MongoClient("mongodb://localhost:27017");
        var database = client.GetDatabase("Enpal"); 
        database.DropCollection("PowerGenerationRecord");
        _db = database.GetCollection<SolarPanelProduct>("SolarPanelProduct");
    }

    [HttpGet(Name = "SolarPanel")]
    public IEnumerable<SolarPanelProduct> Get()
    {
        return _db.AsQueryable();
    }

    [HttpPost(Name = "SolarPanel")]
    public string Post(SolarPanelProduct solarPanel)
    {
        _db.InsertOne(solarPanel);
        return "New solar panel with ID '" + solarPanel.Id.ToString() + "' was inserted!";
    }

    [HttpDelete(Name = "SolarPanel")]
    public string Delete(string id)
    {
        var idFilter = Builders<SolarPanelProduct>.Filter.Eq(x => x.Id.ToString(), id); 
        _db.DeleteOne(idFilter);
        return "Customer with email '" + id + "' has been deleted!";
    }
}
