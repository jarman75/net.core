using Bogus;
using FakeData.Models;

namespace FakeData.Fakers;

public class CustomerFaker : Faker<Customer>
{
    public CustomerFaker()
    {
        RuleFor(c => c.Id, f => f.Random.Number(1, 100));
        RuleFor(c => c.FirstName, f => f.Person.FirstName);
        RuleFor(c => c.LastName, f => f.Person.LastName);
        RuleFor(c => c.Address, f => f.Address.FullAddress());
        RuleFor(c => c.Email, f => f.Person.Email); 
        RuleFor(c => c.Orders, f => new OrderFaker().GenerateBetween(1, 5));       
    }
}