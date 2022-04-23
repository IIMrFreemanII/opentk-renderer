using open_tk_renderer.Renderer.UI.Widgets.Utils;
using OpenTK.Mathematics;

namespace open_tk_renderer.Renderer.ImGui;

public static partial class Ui
{
  public static void SizedBox(float width = 0, float height = 0)
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

    constraints = tempConstraints;
    size = tempConstraints.Smallest;
  }
}
