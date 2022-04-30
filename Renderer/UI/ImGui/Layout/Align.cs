using open_tk_renderer.Renderer.UI.Widgets.Utils;
using open_tk_renderer.Utils;
using OpenTK.Mathematics;

namespace open_tk_renderer.Renderer.UI.ImGui.Layout;

public class AlignPool : Pool<Align> { }

public class Align : Node
{
  public Vector2? sizeFactor;
  public Alignment alignment;
  
  public static Align Create(Alignment? alignment = null, Vector2? sizeFactor = null)
  {
    var obj = AlignPool.Create();
    obj.alignment = alignment ?? Alignment.TopLeft;
    obj.sizeFactor = sizeFactor;

    return obj;
  }
  
  public override void Delete()
  {
    base.Delete();
    AlignPool.Delete(this);
  }

  public override void CalcSize(BoxConstraints constraints)
  {
    size = constraints.Biggest;
    var newConstraints = BoxConstraints.Loose(constraints.Biggest);

    foreach (var child in children)
    {
      child.CalcSize(newConstraints);
      if (sizeFactor.HasValue) size = newConstraints.Constrain(child.size * sizeFactor.Value);
    }
  }

  public override void CalcPosition()
  {
    foreach (var child in children)
    {
      child.position = position + (size / 2 - child.size / 2) +
        alignment.pivot * (size / 2 - child.size / 2);
      child.CalcPosition();
    }
  }
}
