using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using open_tk_renderer.Renderer.UI.ImGui.Layout;

namespace open_tk_renderer.benchmarks;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class PoolBenchmark
{
  public PoolBenchmark()
  {
    SizedBoxPool.Prefill(1000);
  }
  [Benchmark(Baseline = true)]
  public float PooledClass()
  {
    float sum = 0;
    for (int i = 0; i < 10000; i++)
    {
      var elem = SizedBox.Create();
      sum += elem.width + elem.height;
      elem.Delete();
    }

    return sum;
  }
  
  [Benchmark]
  public float NonPooledClass()
  {
    float sum = 0;
    for (int i = 0; i < 10000; i++)
    {
      var elem = new SizedBox();
      sum += elem.width + elem.height;
    }
    return sum;
  }
}
