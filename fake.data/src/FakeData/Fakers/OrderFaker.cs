using Bogus;
using FakeData.Models;
using Newtonsoft.Json;

namespace FakeData.Fakers;

public class OrderFaker : Faker<Order>
{
    public OrderFaker()
    {
        RuleFor(o => o.Id, f => f.Random.Number(1, 100));
        RuleFor(o => o.Date, f => f.Date.Past());
        RuleFor(o => o.CustomerId, f => f.Random.Number(1, 100));
        RuleFor(o => o.Total, f => f.Random.Decimal(1, 1000));
        RuleFor(o => o.Status, f => f.PickRandom<Status>());
        RuleFor(o => o.Details, f => new OrderDetailFaker().GenerateBetween(1, 5));
    }
}

public class OrderDetailFaker : Faker<OrderDetail>
{
    public OrderDetailFaker()
    {
        RuleFor(od => od.ProductId, f => f.Random.Number(1, 100));
        RuleFor(od => od.ProductName, f => f.Commerce.ProductName());
        RuleFor(od => od.Quantity, f => f.Random.Number(1, 10));
        RuleFor(od => od.UnitPrice, f => f.Random.Decimal(1, 100));
        RuleFor(od => od.Discount, f => f.Random.Decimal(1, 10));
    }
    
}

 public static class ExtensionsForTesting
{
    public static void Dump(this object obj)
    {
        Console.WriteLine(obj.DumpString());
    }

    public static string DumpString(this object obj)
    {
        return JsonConvert.SerializeObject(obj, Formatting.Indented);
    }
}