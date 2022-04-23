using System.ComponentModel;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using open_tk_renderer.temp;
using PostSharp;
using PostSharp.Patterns.Model;

namespace open_tk_renderer.benchmarks;

public class BindObj
{
  public Bind<float> width = new();
  public Bind<float> height = new();

  public BindObj(float width, float height)
  {
    this.width.Value = width;
    this.height.Value = height;
  }
}

public class PlainObj
{
  public float width;
  public float height;

  public PlainObj(float width, float height)
  {
    this.width = width;
    this.height = height;
  }
}

[NotifyPropertyChanged]
public class NotifyObj
{
  public float Width { get; set; }
  public float Height { get; set; }
  
  public NotifyObj(float width, float height)
  {
    Width = width;
    Height = height;
  }
}

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class BindBenchmark
{
  private static NotifyObj notifyObject = new(100, 100);
  private static BindObj bindObject = new(100, 100);
  private static PlainObj plainObject = new(100, 100);

  public BindBenchmark()
  {
    for (int i = 0; i < 3; i++)
    {
      Post.Cast<NotifyObj, INotifyPropertyChanged>(notifyObject).PropertyChanged += (obj, e) => { };
      bindObject.width.ValueChanged += (value) => { };
      bindObject.height.ValueChanged += (value) => { };
    }
  }

  [Benchmark(Baseline = true)]
  public float SetValueNotifyObject()
  {
    notifyObject.Width = 200;
    notifyObject.Height = 200;
    return notifyObject.Width + notifyObject.Height;
  }
  
  [Benchmark]
  public float SetValueBindObject()
  {
    bindObject.width.Value = 200;
    bindObject.height.Value = 200;
    return bindObject.width.Value + bindObject.height.Value;
  }

  [Benchmark]
  public float SetValuePlainObject()
  {
    plainObject.width = 200;
    plainObject.height = 200;
    return plainObject.width + plainObject.height;
  }
}
