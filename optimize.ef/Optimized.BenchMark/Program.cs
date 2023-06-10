using BenchmarkDotNet.Running;

namespace Optimized.BenchMark;
public class Program
{
    public static void Main(string[] args)
    {
        BenchmarkRunner.Run<QuerysBenchMark>();

        //BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
        
    }
}
