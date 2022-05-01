using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using open_tk_renderer.Aspects;
using open_tk_renderer.ECS;
using open_tk_renderer.ECS.Components;
using OpenTK.Mathematics;

namespace open_tk_renderer.benchmarks;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class EcsBenchmark
{
  // public static Registry registry = new ();
  // public static Entity entity = registry.Create();
  public EcsBenchmark()
  {
  }

  [Benchmark(Baseline = true)]
  public Type GetCustomType()
  {
    return typeof(Position);
  }

  [Benchmark]
  public string GetName()
  {
    return nameof(Position);
  }
}
