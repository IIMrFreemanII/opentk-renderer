using open_tk_renderer.Renderer.UI.Widgets.Utils;
using open_tk_renderer.Utils;

namespace open_tk_renderer.Renderer.UI.ImGui.Layout;

public class ExpandedPool : Pool<Expanded> { }

public class Expanded : Node
{
  public int flex;
  
  public static Expanded Create(int flex)
  {
    var obj = ExpandedPool.Create();
    obj.flex = flex;
    return obj;
  }
  public override void Delete()
  {
    base.Delete();
    ExpandedPool.Delete(this);
  }
  
  public override void CalcSize(BoxConstraints constraints)
  {
    for (int i = 0; i < children.Count; i++)
    {
      var child = children[i];
      child.CalcSize(constraints);
      size = child.size;
    }
  }
}
