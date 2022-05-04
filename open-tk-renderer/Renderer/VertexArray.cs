using OpenTK.Graphics.OpenGL4;

namespace open_tk_renderer.Renderer;

public class VertexArray
{
    public int id;
    public VertexBuffer[] vertexBuffers;

    private int _vertexBufferIndex = 0;

    public VertexArray(VertexBuffer[] vertexBuffers)
    {
        this.vertexBuffers = vertexBuffers;

        id = GL.GenVertexArray();
        Bind();

        foreach (var vertexBuffer in vertexBuffers)
        {
            vertexBuffer.Bind();

            foreach (var element in vertexBuffer.layout.elements)
            {
                GL.EnableVertexAttribArray(_vertexBufferIndex);
                GL.VertexAttribPointer(
                    _vertexBufferIndex,
                    element.count,
                    element.type,
                    element.normalized,
                    vertexBuffer.layout.stride,
                    element.offset
                );

                _vertexBufferIndex++;
            }
        }
    }

    public void Bind()
    {
        GL.BindVertexArray(id);
    }

    public void Unbind()
    {
        GL.BindVertexArray(0);
    }
}