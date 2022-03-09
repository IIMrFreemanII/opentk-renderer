using OpenTK.Mathematics;

namespace open_tk_renderer.Renderer.UI.Widgets;

public class Container : Widget
{
  public Color4 color = Color4.WhiteSmoke;

  public int rotation = 0;
  public EdgeInsets? padding;
  public EdgeInsets margin;
  public Alignment? alignment;

  private Matrix4 model = Matrix4.Identity;

  private readonly Material _material;
  private readonly Mesh _mesh;

  public Container(
    Color4? color = null,
    Vector2? size = null,
    Widget? child = null,
    EdgeInsets? margin = null,
    EdgeInsets? padding = null,
    Alignment? alignment = null
  )
  {
    _material = Window.DefaultMaterial;
    _mesh = Window.QuadMesh;

    if (size != null)
    {
      this.size = (Vector2)size;
    }

    if (color != null)
    {
      this.color = (Color4)color;
    }

    this.padding = padding;
    this.margin = margin ?? new EdgeInsets(0);
    this.alignment = alignment;

    if (child != null)
    {
      children.Add(new Padding(this.padding, new Align(this.alignment, child)));
    }
  }

  public override void Render()
  {
    UpdateModel();
    _material.uniforms["u_model"].value = model;
    _material.uniforms["u_view"].value = Window.View;
    _material.uniforms["u_projection"].value = Window.Projection;
    _material.uniforms["u_color"].value = (Vector4)color;
    Graphics.DrawMesh(_mesh, _material);
  }

  private void UpdateModel()
  {
    model =
      Matrix4.CreateScale(size.X, size.Y, 1) *
      Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(rotation)) *
      Matrix4.CreateTranslation(position.X, position.Y, 0);
  }

  public override void CalcLayout(Vector2 parentSize)
  {
    size = size == Vector2.Zero
      ? parentSize
      : size;
    // size = size == Vector2i.Zero ? parentSize : Vector2i.Clamp(size, Vector2i.Zero, parentSize);

    position += margin.size;
    size -= margin.size * 2;
    
    if (children.Count > 0)
    {
      Widget child = children[0];
      child.position = position;
      child.CalcLayout(size);

      // size to fit child
      // size = child.size;
    }
  }
}