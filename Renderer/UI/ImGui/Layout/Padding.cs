using open_tk_renderer.Renderer.UI.Widgets.Utils;
using open_tk_renderer.Utils;

namespace open_tk_renderer.Renderer.UI.ImGui.Layout;

public class PaddingPool : Pool<Padding>
{
}

public class Padding : Node
{
  public EdgeInsets padding;

  public static Padding Create(EdgeInsets? padding = null)
  {
    var obj = PaddingPool.Create();
    obj.padding = padding ?? EdgeInsets.All(value: 0);
    return obj;
  }

  public override void Delete()
  {
    PaddingPool.Delete(this);
  }
  
  public override void CalcSize(BoxConstraints constraints)
  {
    size = constraints.Biggest;
    var newConstraints = BoxConstraints.Loose(size);

    foreach (var child in children)
    {
      child.CalcSize(newConstraints.Deflate(padding));
      size = newConstraints.Constrain(padding.InflateSize(child.size));
    }
  }
  
  public override void CalcPosition()
  {
    foreach (var child in children)
    {
      child.position = position + padding.TopLeft;
      child.CalcPosition();
    }
  }
}
