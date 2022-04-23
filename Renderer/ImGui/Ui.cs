using open_tk_renderer.Renderer.UI.Widgets.Utils;
using OpenTK.Mathematics;

namespace open_tk_renderer.Renderer.ImGui;

public static partial class Ui
{
  public static Vector2 size;
  public static Vector2 position;
  public static BoxConstraints constraints;
  
  private static Mesh _mesh;
  private static Material _roundedRect;
  private static Material _roundedRectFrame;

  public static void Init()
  {
    _roundedRect = MaterialsController.Get("roundedRect")!;
    _roundedRectFrame = MaterialsController.Get("roundedRectFrame")!;
    _mesh = Window.QuadMesh;
  }

  public static void Page(Vector2 size, BoxConstraints constraints)
  {
    Ui.constraints = constraints;
    Ui.size = Ui.constraints.Constrain(size);
    position = new(0);
  }
}
