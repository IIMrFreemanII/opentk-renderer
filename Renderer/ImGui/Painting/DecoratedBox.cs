using open_tk_renderer.Renderer.UI.Widgets.Utils;
using OpenTK.Mathematics;

namespace open_tk_renderer.Renderer.ImGui;

public static partial class Ui
{
  public static void DecoratedBox(
    BoxDecoration? decoration = null
  )
  {
    BoxDecoration boxDecoration = decoration ?? new BoxDecoration();
    var uPosition = position;
    var uSize = constraints.Biggest;
    constraints = BoxConstraints.Loose(uSize);
    
    Matrix4 model = Matrix4.CreateScale(
        uSize.X,
        uSize.Y,
        z: 1
      ) *
      Matrix4.CreateTranslation(
        uPosition.X,
        uPosition.Y,
        z: 0
      );
    
    void RenderBack()
    {
      Vector4 uColor = (Vector4)boxDecoration.color;
      Vector4 uBorderRadius = boxDecoration.borderRadius.ToVec4();
      
      _roundedRect.SetMatrix("u_model", model);
      _roundedRect.SetMatrix("u_view", Window.View);
      _roundedRect.SetMatrix("u_projection", Window.Projection);

      _roundedRect.SetVector("u_color", uColor);
      _roundedRect.SetVector("u_border_radius", uBorderRadius);
      _roundedRect.SetVector("u_size", uSize);
      _roundedRect.SetVector("u_resolution", Window.Resolution);
      _roundedRect.SetFloat("u_time", (float)Window.Time);

      Graphics.DrawMesh(_mesh, _roundedRect);
    }
    
    void RenderFront()
    {
      Vector4 uBorderColor = (Vector4)boxDecoration.border.color;
      Vector4 uBorderRadius = boxDecoration.borderRadius.ToVec4();
      float uBorderSize = boxDecoration.border.bottom;
      
      _roundedRectFrame.SetMatrix("u_model", model);
      _roundedRectFrame.SetMatrix("u_view", Window.View);
      _roundedRectFrame.SetMatrix("u_projection", Window.Projection);

      _roundedRectFrame.SetVector("u_color", uBorderColor);
      _roundedRectFrame.SetVector("u_border_radius", uBorderRadius);
      _roundedRectFrame.SetVector("u_size", uSize);
      _roundedRectFrame.SetFloat("u_border_size", uBorderSize);
      _roundedRectFrame.SetVector("u_resolution", Window.Resolution);
      _roundedRectFrame.SetFloat("u_time", (float)Window.Time);

      Graphics.DrawMesh(_mesh, _roundedRectFrame);
    }
    
    RenderBack();
    RenderFront();
  }
}
