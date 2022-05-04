using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using open_tk_renderer.ECS;

namespace benchmarks;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class EcsBenchmark
{
  public static Registry registry = new ();
  //
  // [Benchmark(Baseline = true)]
  // public void AddComponent()
  // {
  //   var entity = registry.Create();
  //   registry.AddComponent(entity, new Size());
  // }
}
