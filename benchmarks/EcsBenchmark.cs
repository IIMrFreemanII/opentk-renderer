using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using open_tk_renderer.ECS.Components;
using OpenTK.Mathematics;

namespace open_tk_renderer.benchmarks;

public interface IData { }

public struct StructSize : IData
{
  public Vector2 size;
}

public class ClassSize : IData
{
  public Vector2 size;
}

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class EcsBenchmark
{
  // public static Registry registry = new ();
  // public static Entity entity = registry.Create();
  public EcsBenchmark() { }

  // [Benchmark(Baseline = true)]
  // public Type GetCustomType()
  // {
  //   return typeof(Position);
  // }
  //
  // [Benchmark]
  // public string GetName()
  // {
  //   return nameof(Position);
  // }

  //---------

  // [Benchmark(Baseline = true)]
  // public StructSize CreateStructSize()
  // {
  //   return new StructSize();
  // }
  //
  // [Benchmark]
  // public ClassSize CreateClassSize()
  // {
  //   return new ClassSize();
  // }
}
