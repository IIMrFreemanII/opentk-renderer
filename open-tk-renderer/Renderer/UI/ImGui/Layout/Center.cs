using open_tk_renderer.Renderer.UI.Widgets.Utils;
using open_tk_renderer.Utils;
using OpenTK.Mathematics;

namespace open_tk_renderer.Renderer.UI.ImGui.Layout;

public class CenterPool : Pool<Center> { }

public class Center : Node
{
  public Vector2? sizeFactor;

  public static Center Create(Vector2? sizeFactor = null)
  {
    var obj = CenterPool.Create();
    obj.sizeFactor = sizeFactor;

    return obj;
  }
  
  public override void Delete()
  {
    base.Delete();
    CenterPool.Delete(this);
  }

  public override void CalcSize(BoxConstraints constraints)
  {
    size = constraints.Biggest;
    var newConstraints = BoxConstraints.Loose(size);
    foreach (var child in children) child.CalcSize(newConstraints);
  }

  public override void CalcPosition()
  {
    foreach (var child in children)
    {
      if (sizeFactor != null) size = child.size * (Vector2)sizeFactor;
      child.position = size / 2 - child.size / 2;
      child.CalcPosition();
    }
  }
}
