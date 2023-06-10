using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace Optimized.BenchMark;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class RestultExceptionBenchMark
{
    
    private void GetException()
    {
        throw new Exception("Error");
    }
    
    private string GetResult()
    {
        return "Error";
    }

    [Benchmark]
    public string GetExceptionBenchmark()
    {
        try { GetException(); return "Ok"; }
        catch (Exception e) { return e.Message; }
    }
    [Benchmark]
    public string GetResultBenchmark()
    {
        return GetResult();
    }
}
