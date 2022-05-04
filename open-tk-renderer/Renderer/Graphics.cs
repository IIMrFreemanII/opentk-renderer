using open_tk_renderer.Renderer.Text;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace open_tk_renderer.Renderer;

public static class Graphics
{
  public static void DrawMesh(Mesh mesh, Material material)
  {
    material.shader.Use();
    material.SetUniforms();
    DrawArrays(mesh);
  }

  public static void DrawArrays(Mesh mesh)
  {
    mesh.vertexArray.Bind();
    GL.DrawArrays(
      mesh.mode,
      0,
      mesh.count
    );
  }

  // Todo: add max width and word wrapping
  public static void DrawText(
    string fontName,
    string text,
    Vector2 position,
    uint fontSize,
    Color4 color
  )
  {
    var font = FontsController.Get(fontName);
    if (font is null)
    {
      Console.WriteLine($"Font: '{fontName}' is not found!");
      return;
    }

    GL.ActiveTexture(TextureUnit.Texture0);

    var mesh = Window.QuadMesh;
    var textMaterial = MaterialsController.Get("text");

    textMaterial.SetMatrix("u_view", Window.View);
    textMaterial.SetMatrix("u_projection", Window.Projection);
    textMaterial.SetVector("u_color", (Vector4)color);
    textMaterial.SetInt("u_texture0", 0);

    float charX = 0;
    foreach (var c in text)
    {
      var character = font.GetCharBySize(c, fontSize);

      Vector3 size = new(
        character.Size.X,
        character.Size.Y,
        1
      );
      var xRel = position.X + charX + character.Bearing.X;
      var yRel = (position.Y + -character.Size.Y + fontSize) +
                 (character.Size.Y - character.Bearing.Y);

      // Now advance cursors for next glyph (note that advance is number of 1/64 pixels)
      // Bitshift by 6 to get value in pixels (2^6 = 64 (divide amount of 1/64th pixels by 64 to get amount of pixels))
      charX += character.Advance >> 6;

      var scaleM = Matrix4.CreateScale(
        new Vector3(
          size.X,
          size.Y,
          1
        )
      );
      var translRelM = Matrix4.CreateTranslation(
        new Vector3(
          xRel,
          yRel,
          0
        )
      );
      var modelM = scaleM * translRelM; // OpenTK `*`-operator is reversed
      textMaterial.SetMatrix("u_model", modelM);

      GL.BindTexture(TextureTarget.Texture2D, character.TextureId);
      DrawMesh(mesh, textMaterial);
    }

    GL.BindTexture(TextureTarget.Texture2D, 0);
  }
}
