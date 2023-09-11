using Api.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ILogger<CustomerController> _logger;

    private readonly IMongoCollection<Customer> _db;
    public CustomerController(ILogger<CustomerController> logger)
    {
        _logger = logger;

        var client = new MongoClient("mongodb://localhost:27017");
        var database = client.GetDatabase("Enpal");
        _db = database.GetCollection<Customer>("Customer"); 
    }

    private void Initialze() {
        _db.InsertOne(new Customer{
            Email = "fabianxvogt@gmail.com",
            Name = "Fabian",
        });
        _db.InsertOne(new Customer{
            Email = "Alice@gmail.com",
            Name = "Alice",
        });
        _db.InsertOne(new Customer{
            Email = "Bob@gmail.com",
            Name = "Bob",
        });
    }

    [HttpGet(Name = "Customer")]
    public IEnumerable<Customer> Get()
    {
        return _db.AsQueryable();
    }

    [HttpPost(Name = "Customer")]
    public string Post(Customer customer)
    {
        var emailFilter = Builders<Customer>.Filter.Eq(x => x.Email, customer.Email);

        if (_db.Find(emailFilter).Any()) {
            return "Email already exists!";
        }
        
        _db.InsertOne(customer);
        return "Customer with email '" + customer.Email + "' was inserted!";
    }

    [HttpDelete(Name = "Customer")]
    public string Delete(string email)
    {
        var emailFilter = Builders<Customer>.Filter.Eq(x => x.Email, email); 
        _db.DeleteOne(emailFilter);
        return "Customer with email '" + email + "' has been deleted!";
    }
}
