using open_tk_renderer.Renderer.UI.Widgets.Utils;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace open_tk_renderer.Renderer.UI.Widgets.Painting;

public class Image : Widget
{
  private static Material? _material;
  private static Mesh? _mesh;
  private Texture? _texture;

  private Matrix4 _model = Matrix4.Identity;

  public Image(string name)
  {
    _material ??= MaterialsController.Get("texture");
    _mesh ??= Window.QuadMesh;
    _texture = TextureController.Get(name);
  }

  private void UpdateModel()
  {
    _model =
      Matrix4.CreateScale(
        size.X,
        size.Y,
        z: 1
      ) *
      Matrix4.CreateTranslation(
        position.X,
        position.Y,
        z: 0
      );
  }

  public override void Render()
  {
    UpdateModel();

    _material.SetMatrix("u_model", _model);
    _material.SetMatrix("u_view", Window.View);
    _material.SetMatrix("u_projection", Window.Projection);

    _material.SetInt("u_texture0", value: 0);
    _texture.Use(TextureUnit.Texture0);

    Graphics.DrawMesh(_mesh, _material);
    
    base.Render();
  }

  public override void CalcSize(BoxConstraints constraints)
  {
    size = constraints.Biggest;

    foreach (var child in children) child.CalcSize(constraints);
  }
}
