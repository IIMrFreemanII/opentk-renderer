using OpenTK.Graphics.OpenGL4;

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
        GL.DrawArrays(mesh.mode, 0, mesh.count);
    }
}