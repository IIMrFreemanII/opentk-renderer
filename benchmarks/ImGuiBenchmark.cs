using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using open_tk_renderer.Renderer.UI.ImGui.Layout;
using open_tk_renderer.Renderer.UI.Widgets.Utils;

namespace open_tk_renderer.benchmarks;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class ImGuiBenchmark
{
  // [Benchmark(Baseline = true)]
  // public void NodeInterface()
  // {
  //   var node = new StructSizedBox();
  //   node.Layout();
  //   node.CalcSize(BoxConstraints.Loose(new(500)));
  //   node.CalcPosition();
  // }
  //
  // [Benchmark]
  // public void NodeClass()
  // {
  //   var node = new SizedBox();
  //   node.Layout();
  //   node.CalcSize(BoxConstraints.Loose(new(500)));
  //   node.CalcPosition();
  // }
}
