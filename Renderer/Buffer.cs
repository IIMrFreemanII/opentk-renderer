using OpenTK.Graphics.OpenGL4;

namespace open_tk_renderer.Renderer;

public class BufferElement
{
    public string name;
    public VertexAttribPointerType type;
    // bytes amount
    public int bytes;
    // count of elements
    public int count;
    public int offset = 0;
    public bool normalized = false;

    public BufferElement(string name, VertexAttribPointerType type, int count, int bytes)
    {
        this.name = name;
        this.type = type;
        this.count = count;
        this.bytes = bytes;
    }
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
            offset += element.bytes;
            stride += element.bytes;
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