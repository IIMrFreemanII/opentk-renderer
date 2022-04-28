using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using open_tk_renderer.Aspects;
using open_tk_renderer.ECS;
using open_tk_renderer.ECS.Components;
using OpenTK.Mathematics;

namespace open_tk_renderer.benchmarks;

public class ClassSize
{
  public Vector2 value;
  public Vector2 value1;
  public Vector2 value2;
}

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class EcsBenchmark
{
  public static List<ClassSize> classSizes = new(10000);
  public static List<Size> escSizes = new(10000);

  public EcsBenchmark()
  {
    classSizes.Clear();
    escSizes.Clear();
    
    for (int i = 0; i < 10000; i++)
    {
      classSizes.Add(new ClassSize());
      escSizes.Add(new Size());
    }
  }

  [Benchmark(Baseline = true)]
  public void ECSComponent()
  {
    var list = new Size[1000];
    for (int i = 0; i < list.Length; i++)
    {
      list[i] = new Size();
    }
  }

  [Benchmark]
  public void ClassComponent()
  {
    var list = new ClassSize[1000];
    for (int i = 0; i < list.Length; i++)
    {
      list[i] = new ClassSize();
    }
  }
}
