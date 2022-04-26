using open_tk_renderer.Renderer.UI.Widgets.Utils;
using OpenTK.Mathematics;

namespace open_tk_renderer.Renderer.ImGui;

public class SizedBox : Node
{
  public float width;
  public float height;

  public SizedBox(
    float width = 0,
    float height = 0
  )
  {
    this.width = width;
    this.height = height;
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

public static partial class Ui
{
  public static void SizedBox(float width = 0, float height = 0)
  {
    S_SizedBox(width, height);
    E_SizedBox();
  }
  
  public static void S_SizedBox(float width = 0, float height = 0)
  {
    var elem = new SizedBox(width, height);
    currentChildren.Peek().Add(elem);
    currentChildren.Push(elem.children);
  }

  public static void E_SizedBox()
  {
    currentChildren.Pop();
  }
}
