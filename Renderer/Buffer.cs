using OpenTK.Graphics.OpenGL4;

namespace open_tk_renderer.Renderer;

public class BufferElement
{
    public string name;
    public VertexAttribPointerType type;
    public int size;
    public int offset;
    public bool normalized;
}

public class BufferLayout
{
    public BufferElement[] elements;
    public int stride = 0;

    public BufferLayout(BufferElement[] elements)
    {
        this.elements = elements;
        
        CalculateOffsetAndStride();
    }

    private void CalculateOffsetAndStride()
    {
        int offset = 0;
        foreach (BufferElement element in elements)
        {
            element.offset = offset;
            offset += element.size;
            stride += element.size;
        }
    }
}

public class VertexBuffer
{
    public int id;
    public BufferLayout layout;

    public VertexBuffer(float[] data, BufferLayout layout)
    {
        id = GL.GenBuffer();
        this.layout = layout;
        
        Bind();
        GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float) * data.Length, data, BufferUsageHint.StaticDraw);
    }

    public void Bind()
    {
        GL.BindBuffer(BufferTarget.ArrayBuffer, id);
    }
    
    public void Unbind()
    {
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
    }
}