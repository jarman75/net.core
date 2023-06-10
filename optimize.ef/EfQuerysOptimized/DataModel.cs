namespace EfQuerysOptimized;
public class Transaction
{
    public int Id { get; set; }
    public string Description { get; set; } = null!;
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public int AccountId { get; set; }
    public Account Account { get; set; } = null!;
    
}

public class Account
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Balance { get; set; }
    public ICollection<Transaction> Transactions { get; set; } = null!;
}
