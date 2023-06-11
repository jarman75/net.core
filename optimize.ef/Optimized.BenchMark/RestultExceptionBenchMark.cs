using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using EfQuerysOptimized;

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
    
    private Result<string, ValidationFailed> GetResult()
    {
        var fail = new ValidationFailed("General", "Error");
        var response = new Result<string, ValidationFailed>(fail);
        return response;             
        
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
        return GetResult()
        .Match(
            success => success,
            failure => failure.ValidationResult["General"]);;
    }
}
