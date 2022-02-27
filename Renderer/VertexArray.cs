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

        foreach (VertexBuffer vertexBuffer in vertexBuffers)
        {
            vertexBuffer.Bind();

            foreach (BufferElement element in vertexBuffer.layout.elements)
            {
                GL.EnableVertexAttribArray(_vertexBufferIndex);
                // GL.VertexAttribPointer(_vertexBufferIndex, 0, Ver);
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