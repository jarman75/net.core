namespace FakeData.Models;

public class Order
{
    
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public int CustomerId { get; set; }
    public decimal Total { get; set; }
    public Status Status { get; set; }
    public List<OrderDetail> Details { get; set; } = new List<OrderDetail>();
    
}

public enum Status 
{
    Pending,
    Shipped,
    Delivered
}
public class OrderDetail
{
    public int ProductId { get; set; }
    public required string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
}
