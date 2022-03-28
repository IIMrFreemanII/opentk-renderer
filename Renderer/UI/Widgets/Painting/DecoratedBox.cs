using open_tk_renderer.Renderer.UI.Widgets.Utils;
using OpenTK.Mathematics;

namespace open_tk_renderer.Renderer.UI.Widgets.Painting;

public class DecoratedBox : Widget
{
  public Color4 color;
  public Color4 borderColor = Colors.Blue;
  public float borderSize = 10;
  public Vector4 borderRadius = new(20);

  private readonly Material _material;
  private readonly Mesh _mesh;
  private Matrix4 backModel = Matrix4.Identity;
  private Matrix4 frontModel = Matrix4.Identity;

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
    RenderBack();
    // RenderRectFront();
  }

  private void RenderBack()
  {
    UpdateModel();
    _material.SetMatrix("u_model", backModel);
    _material.SetMatrix("u_view", Window.View);
    _material.SetMatrix("u_projection", Window.Projection);

    _material.SetVector("u_color", (Vector4)color);
    _material.SetVector("u_border_color", (Vector4)borderColor);
    _material.SetVector("u_border_radius", borderRadius);
    _material.SetFloat("u_border_size", borderSize);
    _material.SetVector("u_size", size);
    _material.SetVector("u_resolution", Window.Resolution);
    _material.SetFloat("u_time", (float)Window.Time);

    Graphics.DrawMesh(_mesh, _material);
  }

  private void RenderRectFront()
  {
    _material.SetMatrix("u_model", frontModel);
    _material.SetVector("u_color", (Vector4)color);
    Graphics.DrawMesh(_mesh, _material);
  }

  private void UpdateModel()
  {
    backModel =
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

    frontModel =
      Matrix4.CreateScale(
        size.X - borderSize * 2,
        size.Y - borderSize * 2,
        1
      ) *
      Matrix4.CreateTranslation(
        position.X + borderSize,
        position.Y + borderSize,
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
