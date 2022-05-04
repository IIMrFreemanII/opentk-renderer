using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace open_tk_renderer.benchmarks;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class EcsBenchmark
{
  // public static Registry registry = new ();
  //
  // [Benchmark(Baseline = true)]
  // public void AddComponent()
  // {
  //   var entity = registry.Create();
  //   registry.AddComponent(entity, new Size());
  // }
}
