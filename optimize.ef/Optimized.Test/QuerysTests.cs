namespace Optimized.Test;

public class QuerysTests
{
    readonly DataContext _context;
    readonly DataQuerys _querys;
    
    public QuerysTests()
    {
        _context = TestData.GetContext();
        _querys = new DataQuerys(_context);
    }

    [Fact]
    //get all transactions for account 1, where amount > 700
    public async Task GetTransactionsByAccountId()
    {
        var transactions = await _querys.GetTransactionsByAccountId(1, x => x.Amount > 700);
        Assert.Equal(2, transactions.Count);
    }
    //get transactions when account not exist
    [Fact]
    public async Task GetTransactionsByAccountIdWhenAccountNotExist()
    {
        var transactions = await _querys.GetTransactionsByAccountId(100);
        Assert.Empty(transactions);
    }
}