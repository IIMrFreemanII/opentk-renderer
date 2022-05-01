using open_tk_renderer.ECS.Components;
using open_tk_renderer.Renderer;
using open_tk_renderer.Renderer.UI.Widgets.Utils;
using OpenTK.Mathematics;

namespace open_tk_renderer.ECS.Systems;

public class DecoratedBoxSystem : BaseSystem
{
  public Registry registry;

  private static Material? _roundedRect;
  private static Material? _roundedRectFrame;
  private static Mesh? _mesh;

  private Matrix4 decoratedBoxModel = Matrix4.Identity;

  public DecoratedBoxSystem(Registry registry)
  {
    this.registry = registry;

    _roundedRect ??= MaterialsController.Get("roundedRect");
    _roundedRectFrame ??= MaterialsController.Get("roundedRectFrame");
    _mesh ??= Window.QuadMesh;
  }

  public override void Update()
  {
    registry.ForEach(
      (Entity entity, ref Decoration decoration) =>
      {
        var size = registry.GetComponent<Size>(entity);
        var position = registry.GetComponent<Position>(entity);

        UpdateModel(size.value, position.value);
        RenderBack(decoration.value, size.value);
        RenderFront(decoration.value, size.value);
      }
    );
  }

  private void UpdateModel(Vector2 size, Vector2 position)
  {
    decoratedBoxModel =
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

  private void RenderBack(BoxDecoration boxDecoration, Vector2 size)
  {
    _roundedRect.SetMatrix("u_model", decoratedBoxModel);
    _roundedRect.SetMatrix("u_view", Window.View);
    _roundedRect.SetMatrix("u_projection", Window.Projection);

    _roundedRect.SetVector("u_color", (Vector4)boxDecoration.color);
    _roundedRect.SetVector("u_border_radius", boxDecoration.borderRadius.ToVec4());
    _roundedRect.SetVector("u_size", size);
    _roundedRect.SetVector("u_resolution", Window.Resolution);
    _roundedRect.SetFloat("u_time", (float)Window.Time);

    Graphics.DrawMesh(_mesh, _roundedRect);
  }

  private void RenderFront(BoxDecoration boxDecoration, Vector2 size)
  {
    _roundedRectFrame.SetMatrix("u_model", decoratedBoxModel);
    _roundedRectFrame.SetMatrix("u_view", Window.View);
    _roundedRectFrame.SetMatrix("u_projection", Window.Projection);

    _roundedRectFrame.SetVector("u_color", (Vector4)boxDecoration.border.color);
    _roundedRectFrame.SetVector("u_border_radius", boxDecoration.borderRadius.ToVec4());
    _roundedRectFrame.SetVector("u_size", size);
    _roundedRectFrame.SetFloat("u_border_size", boxDecoration.border.bottom);
    _roundedRectFrame.SetVector("u_resolution", Window.Resolution);
    _roundedRectFrame.SetFloat("u_time", (float)Window.Time);

    Graphics.DrawMesh(_mesh, _roundedRectFrame);
  }
}
