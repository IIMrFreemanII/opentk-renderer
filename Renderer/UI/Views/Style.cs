using open_tk_renderer.Renderer.UI.Widgets.Utils;
using OpenTK.Mathematics;

namespace open_tk_renderer.Renderer.UI.Views;

public class Style
{
  public float? Width { get; set; }
  public float? Height { get; set; }

  public EdgeInsets? Padding { get; set; }
  public EdgeInsets? Margin { get; set; }

  public Color4 BackgroundColor { get; set; } = Colors.DefaultBgColor;
  public Color4 Color { get; set; } = Color4.Black;
}
