using open_tk_renderer.Renderer.UI.Widgets.Utils;
using open_tk_renderer.Utils;
using OpenTK.Mathematics;

namespace open_tk_renderer.Renderer.UI.ImGui.Layout;

public class SizedBoxPool : Pool<SizedBox>
{
}

public class SizedBox : Node
{
  public float width;
  public float height;

  public static SizedBox Create(float width = 0, float height = 0)
  {
    var obj = SizedBoxPool.Create();
    obj.width = width;
    obj.height = height;
    return obj;
  }

  public override void Delete()
  {
    SizedBoxPool.Delete(this);
  }

  public override void CalcSize(BoxConstraints constraints)
  {
    Vector2 temp = new(width, height);
    temp = constraints.Constrain(temp);

    var minWidth = temp.X;
    var minHeight = temp.Y;
    var maxWidth = temp.X == 0
      ? constraints.maxWidth
      : temp.X;
    var maxHeight = temp.Y == 0
      ? constraints.maxHeight
      : temp.Y;

    var tempConstraints = new BoxConstraints(
      minWidth,
      maxWidth,
      minHeight,
      maxHeight
    );

    for (int i = 0; i < children.Count; i++)
    {
      var child = children[i];
      child.CalcSize(tempConstraints);
      size = tempConstraints.Constrain(child.size);
    }

    if (children.Count == 0) size = tempConstraints.Smallest;
  }
}
