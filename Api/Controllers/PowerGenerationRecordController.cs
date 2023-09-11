using Api.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PowerGenerationRecordController : ControllerBase
{
    private readonly ILogger<PowerGenerationRecordController> _logger;

    private readonly IMongoCollection<PowerGenerationRecord> _db;
    private readonly IMongoCollection<Customer> _customerDb;
    public PowerGenerationRecordController(ILogger<PowerGenerationRecordController> logger)
    {
        _logger = logger;

        var client = new MongoClient("mongodb://localhost:27017"); 
        var database = client.GetDatabase("Enpal");
        _db = database.GetCollection<PowerGenerationRecord>("PowerGenerationRecord");
        _customerDb = database.GetCollection<Customer>("Customer");
    }

    [HttpGet(Name = "PowerGenerationRecord")]
    public IEnumerable<PowerGenerationRecord> Get()
    {
        return _db.AsQueryable();
    }

    [HttpPost(Name = "PowerGenerationRecord")]
    public string Post(PowerGenerationRecord powerRecord)
    {
        // Check if Customer exists
        var customerExistFilter = Builders<Customer>.Filter.Eq(x => x.Id, powerRecord.CustomerId);

        if (_customerDb.Find(customerExistFilter).Any() == false) {
            return "Customer ID '"+powerRecord.CustomerId.ToString()+"' not found!";
        }

        // Filter on current time: Only one record should be inserted per Day, Customer, Solar panel.
        DateTime currentTime = DateTime.Now;

        var dayFilter = Builders<PowerGenerationRecord>.Filter.And(
            Builders<PowerGenerationRecord>.Filter.Eq(x => x.CustomerId, powerRecord.CustomerId),
            Builders<PowerGenerationRecord>.Filter.Eq(x => x.SolarPanelId, powerRecord.SolarPanelId),
            Builders<PowerGenerationRecord>.Filter.Eq(x => x.Date, powerRecord.Date)
        );

        if (_db.Find(dayFilter).Any()) {
            return "A Power Record already exists for this Day, Customer and Panel type!";
        }
        
        _db.InsertOne(powerRecord);
        return "PowerGenerationRecord for Customer '" + powerRecord.CustomerId + "' has been inserted!";
    }

    [HttpDelete(Name = "PowerGenerationRecord")]
    public string Delete(string id)
    {
        var emailFilter = Builders<PowerGenerationRecord>.Filter.Eq(x => x.Id.ToString(), id); 
        _db.DeleteOne(emailFilter);
        return "PowerGenerationRecord with email '" + id + "' has been deleted!";
    }
}
