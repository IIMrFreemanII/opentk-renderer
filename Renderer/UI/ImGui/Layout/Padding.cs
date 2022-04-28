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

    for (int i = 0; i < children.Count; i++)
    {
      var child = children[i];
      child.CalcSize(newConstraints.Deflate(padding));
      size = newConstraints.Constrain(padding.InflateSize(child.size));
    }
  }
  
  public override void CalcPosition()
  {
    for (int i = 0; i < children.Count; i++)
    {
      var child = children[i];
      child.position = position + padding.TopLeft;
      child.CalcPosition();
    }
  }
}
