using open_tk_renderer.Renderer;

namespace open_tk_renderer.Assets;

public static class TriangleMeshData
{
    public static readonly float[] vertices =
    {
        -0.5f, -0.5f, 0.0f, // Bottom-left vertex
        0.5f, -0.5f, 0.0f, // Bottom-right vertex
        0.0f, 0.5f, 0.0f // Top vertex
    };
    
    public static readonly VertexAttrib[] vertexAttribs =
    {
        new ("a_position", vertices, VertexAttribTypeCount.Three)
    };
}

public static class QuadMeshData
{
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
        new ("a_position", vertices, VertexAttribTypeCount.Three)
    };
}