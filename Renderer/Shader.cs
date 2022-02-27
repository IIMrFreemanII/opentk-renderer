using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace open_tk_renderer.Renderer;

public class Shader : IDisposable
{
    public readonly int id;

    private readonly Dictionary<string, int> _uniformLocations;

    public Shader(string vertexSrc, string fragmentSrc)
    {
        int vertexShader = CompileShader(ShaderType.VertexShader, vertexSrc);
        int fragmentShader = CompileShader(ShaderType.FragmentShader, fragmentSrc);

        id = CreateProgram(vertexShader, fragmentShader);
        _uniformLocations = ExtractUniformLocations();
    }

    private int CompileShader(ShaderType type, string shaderSrc)
    {
        int shader = GL.CreateShader(type);
        GL.ShaderSource(shader, shaderSrc);
        GL.CompileShader(shader);

        GL.GetShader(shader, ShaderParameter.CompileStatus, out int code);
        if (code != (int)All.True)
        {
            string infoLog = GL.GetShaderInfoLog(shader);
            throw new Exception($"Failed to compile shader {shader}.\n{infoLog}");
        }

        return shader;
    }

    private int CreateProgram(int vertexShader, int fragmentShader)
    {
        int program = GL.CreateProgram();
        GL.AttachShader(program, vertexShader);
        GL.AttachShader(program, fragmentShader);

        GL.LinkProgram(program);

        GL.GetProgram(program, GetProgramParameterName.LinkStatus, out int code);
        if (code != (int)All.True)
        {
            throw new Exception($"Failed to link program {program}");
        }

        GL.DetachShader(program, vertexShader);
        GL.DetachShader(program, fragmentShader);
        GL.DeleteShader(vertexShader);
        GL.DeleteShader(fragmentShader);

        return program;
    }

    private Dictionary<string, int> ExtractUniformLocations()
    {
        GL.GetProgram(id, GetProgramParameterName.ActiveUniforms, out int numberOfUniforms);
        
        var uniformLocations = new Dictionary<string, int>();
        for (int i = 0; i < numberOfUniforms; i++)
        {
            var key = GL.GetActiveUniform(id, i, out _, out ActiveUniformType type);
            var location = GL.GetUniformLocation(id, key);
            uniformLocations.Add(key, location);
        }

        return uniformLocations;
    }

    // The shader sources provided with this project use hardcoded layout(location)-s. If you want to do it dynamically,
    // you can omit the layout(location=X) lines in the vertex shader, and use this in VertexAttribPointer instead of the hardcoded values.
    public int GetAttribLocation(string attribName)
    {
        return GL.GetAttribLocation(id, attribName);
    }

    ~Shader()
    {
        GL.DeleteProgram(id);
    }

    public void Use()
    {
        GL.UseProgram(id);
    }

    private bool _disposedValue = false;

    public void Dispose()
    {
        if (!_disposedValue)
        {
            GL.DeleteProgram(id);

            _disposedValue = true;
        }
    }

    public void SetInt(string name, int data)
    {
        GL.Uniform1(_uniformLocations[name], data);
    }

    public void SetFloat(string name, float data)
    {
        GL.Uniform1(_uniformLocations[name], data);
    }

    public void SetMatrix4(string name, Matrix4 data)
    {
        GL.UniformMatrix4(_uniformLocations[name], true, ref data);
    }

    public void SetVector3(string name, Vector3 data)
    {
        GL.Uniform3(_uniformLocations[name], data);
    }
}