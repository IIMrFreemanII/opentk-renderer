using OpenTK.Graphics.OpenGL4;

namespace open_tk_renderer.Renderer;

public enum VertexAttribTypeCount
{
    One = 1,
    Two = 2,
    Three = 3,
    Four = 4,
}

public class VertexAttrib
{
    public string name;
    public float[] data;
    public VertexAttribTypeCount count;

    public VertexAttrib(string name, float[] data, VertexAttribTypeCount count)
    {
        this.name = name;
        this.data = data;
        this.count = count;
    }
}

public class Mesh
{
    public VertexArray vertexArray;

    public int count;
    public PrimitiveType mode = PrimitiveType.Triangles;

    public Mesh(VertexAttrib[] vertexAttribs)
    {
        count = vertexAttribs[0].data.Length / (int)vertexAttribs[0].count;

        var vertexBuffers = vertexAttribs.Select(vertexAttrib =>
        {
            return new VertexBuffer(
                vertexAttrib.data,
                new BufferLayout(
                    new[]
                    {
                        new BufferElement(
                            vertexAttrib.name,
                            VertexAttribPointerType.Float,
                            (int)vertexAttrib.count,
                            sizeof(float) * (int)vertexAttrib.count
                        )
                    }
                )
            );
        }).ToArray();

        vertexArray = new VertexArray(vertexBuffers);
    }
}