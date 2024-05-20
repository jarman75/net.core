using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using EfQuerysOptimized;

namespace Optimized.BenchMark;
[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class QuerysBenchMark
{
    readonly DataContext _context;
    readonly DataQuerys _querys;
    
    public QuerysBenchMark()
    {
         _context = TestData.GetContext(false);
        _querys = new DataQuerys(_context);
    }

    [Benchmark]
    public async Task GetTransactions()
    {
       await _querys.GetTransactionsByAccountId(1, x => x.Amount > 200);
    }
    [Benchmark]
    public async Task GetAccount_Find()
    {
       await _querys.FindAccountById(1);
    }
    [Benchmark]
    public async Task GetAccount_FirstOrDefault()
    {
       await _querys.GetAccountById(1);
    }
    [Benchmark]
    public async Task GetAccount_SingleOrDefault()
    {
       await _querys.GetAccountByIdSingle(1);
    }
   
}
