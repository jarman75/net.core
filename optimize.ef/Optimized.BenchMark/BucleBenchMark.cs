using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace Optimized.BenchMark;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class BucleBenchMark
{
    [Benchmark]
    public int For()
    {
        int result = 0;
        for (int i = 0; i < 1000; i++)
        {
            result += i;
        }
        return result;
    }
    [Benchmark]
    public int While()
    {
        int result = 0;
        int i = 0;
        while (i < 1000)
        {
            result += i;
            i++;
        }
        return result;
    }
    [Benchmark]
    public int DoWhile()
    {
        int result = 0;
        int i = 0;
        do
        {
            result += i;
            i++;
        } while (i < 1000);
        return result;
    }
    [Benchmark]
    public int ForEach()
    {
        int result = 0;
        
       foreach (int item in  Enumerable.Range(0, 1000))
       {
            result += item;
       }
        return result;
    }
}
