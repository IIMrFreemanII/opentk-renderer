using open_tk_renderer.Renderer.UI.Widgets.Utils;

namespace open_tk_renderer.Renderer.ImGui;

public static partial class Ui
{
  public static void Padding(EdgeInsets insets)
  {
    size = insets.DeflateSize(size);
    constraints = BoxConstraints.Loose(size);
    position += insets.TopLeft;
  }
}
