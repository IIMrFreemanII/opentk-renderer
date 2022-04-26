using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using open_tk_renderer.Renderer.UI;
using open_tk_renderer.Renderer.UI.ImGui;
using open_tk_renderer.Renderer.UI.Widgets.Utils;

namespace open_tk_renderer.benchmarks;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class ImGuiBenchmark
{
  [Benchmark(Baseline = true)]
  public void ImGui()
  {
    Ui.Page(new(500));
    {
      Ui.S_SizedBox(100, 100);
      Ui.S_DecoratedBox(new BoxDecoration(Colors.Red));
      Ui.E_DecoratedBox();
      Ui.E_SizedBox();
    }
    Ui.PageEnd(false);
  }
}
