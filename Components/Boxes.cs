using open_tk_renderer.Renderer.UI;
using OpenTK.Mathematics;

namespace open_tk_renderer.Components;

public class Boxes : HookWidget
{
  public override Widget Build()
  {
    var (count, setCount) = UseState(0);
    // var (count1, setCount1) = UseState(10);
    // Task.Delay(1000)
    //     .ContinueWith(
    //         task =>
    //         {
    //             // Console.WriteLine("Delay");
    //             setCount(count + 10);
    //             // setCount1(count1 + 1);
    //         }
    //     );

    Console.WriteLine($"state1 {count}");
    // Console.WriteLine($"state2 {count1}");
    return new Row(
      new()
      {
        new Container(Color4.Red, new(100 + count, 100)),
        new Container(Color4.Green, new(100 + count, 100)),
        new Container(Color4.Blue, new(100 + count, 100)),
      }
    );
  }
}