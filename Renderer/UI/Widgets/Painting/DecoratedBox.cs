using open_tk_renderer.Renderer.UI.Widgets.Utils;
using OpenTK.Mathematics;

namespace open_tk_renderer.Renderer.UI.Widgets.Painting;

public class DecoratedBox : Widget
{
  public Color4 color;

  private readonly Material _material;
  private readonly Mesh _mesh;
  private Matrix4 model = Matrix4.Identity;

  public DecoratedBox(
    Color4? color = null,
    Widget? child = null
  )
  {
    _material = Window.DefaultMaterial;
    _mesh = Window.QuadMesh;

    this.color = color ?? Colors.DefaultBgColor;

    if (child != null) children.Add(child);
  }

  public override void Render()
  {
    UpdateModel();
    _material.SetMatrix("u_model", model);
    _material.SetMatrix("u_view", Window.View);
    _material.SetMatrix("u_projection", Window.Projection);

    _material.SetVector("u_color", (Vector4)color);
    _material.SetVector("u_resolution", Window.Resolution);
    _material.SetFloat("u_time", (float)Window.Time);

    Graphics.DrawMesh(_mesh, _material);
  }

  private void UpdateModel()
  {
    model =
      Matrix4.CreateScale(
        size.X,
        size.Y,
        1
      ) *
      Matrix4.CreateTranslation(
        position.X,
        position.Y,
        0
      );
  }

  public override void CalcSize(BoxConstraints constraints)
  {
    size = constraints.Biggest;

    foreach (var child in children) child.CalcSize(constraints);
  }

  public override void CalcPosition()
  {
    foreach (var child in children)
    {
      child.position = position;
      child.CalcPosition();
    }
  }
}
