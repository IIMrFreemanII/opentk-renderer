namespace open_tk_renderer.Renderer.Primitives;

public static class Triangle
{
  public static readonly float[] vertices =
  {
    -0.5f, -0.5f, 0.0f, // Bottom-left vertex
    0.5f, -0.5f, 0.0f, // Bottom-right vertex
    0.0f, 0.5f, 0.0f // Top vertex
  };

  public static readonly VertexAttrib[] vertexAttribs =
  {
    new(
      "a_position",
      vertices,
      VertexAttribTypeCount.Three
    )
  };
}
