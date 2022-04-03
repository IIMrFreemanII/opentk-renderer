namespace open_tk_renderer.Renderer.Primitives;

public static class Quad
{
  public static float[] GetQuadVertices3D(
    float w,
    float h,
    float x = 0,
    float y = 0
  )
  {
    return new[]
    {
      x, y, 0, // top-left
      x, y + h, 0, // bottom-left
      x + w, y + h, 0, // bottom-right
      x, y, 0, // top-left
      x + w, y + h, 0, // bottom-right
      x + w, y, 0 // top-right
    };
  }

  public static float[] GetQuadVertices2D(
    float w,
    float h,
    float x = 0,
    float y = 0
  )
  {
    return new[]
    {
      x, y, // top-left
      x, y + h, // bottom-left
      x + w, y + h, // bottom-right
      x, y, // top-left
      x + w, y + h, // bottom-right
      x + w, y // top-right
    };
  }

  public static readonly float[] vertices =
  {
    -0.5f, -0.5f, 0.0f, // Bottom-left vertex
    0.5f, -0.5f, 0.0f, // Bottom-right vertex
    0.5f, 0.5f, 0.0f, // Top-right vertex
    -0.5f, -0.5f, 0.0f, // Bottom-left vertex
    0.5f, 0.5f, 0.0f, // Top-right vertex
    -0.5f, 0.5f, 0.0f // Top-left vertex
  };

  public static readonly VertexAttrib[] vertexAttribs =
  {
    new(
      "a_position",
      GetQuadVertices3D(1, 1),
      VertexAttribTypeCount.Three
    ),
    new(
      "a_texcoord",
      GetQuadVertices2D(1, 1),
      VertexAttribTypeCount.Two
    )
  };
}
