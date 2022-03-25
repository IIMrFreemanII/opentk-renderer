using open_tk_renderer.Renderer.UI;
using open_tk_renderer.Renderer.UI.Widgets;
using open_tk_renderer.Renderer.UI.Widgets.Layout;
using OpenTK.Mathematics;
using Timeout = open_tk_renderer.Utils.Timeout;

namespace open_tk_renderer.Components;

public class App : HookWidget
{
  public override Widget Build()
  {
    var (count, setCount) = UseState(1);
    // // var (count1, setCount1) = UseState(10);
    Timeout.Set(() => setCount(count + 10), 1000);
    //
    // Console.WriteLine($"state1 {count}");
    // // Console.WriteLine($"state2 {count1}");
    return this;

    // return new Column(new ()
    // {
    //   new Container(Color4.Red, new(100, 100)),
    //   new Container(Color4.Green, new(100, 100)),
    //   new Container(Color4.Blue, new(100, 100)),
    // });
  }
}