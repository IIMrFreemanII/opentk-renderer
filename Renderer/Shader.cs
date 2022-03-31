using open_tk_renderer.Utils;
using OpenTK.Graphics.OpenGL4;

namespace open_tk_renderer.Renderer;

public struct UniformInfo
{
  public string name;
  public int location;
  public ActiveUniformType type;

  public UniformInfo(
    string name,
    int location,
    ActiveUniformType type
  )
  {
    this.name = name;
    this.location = location;
    this.type = type;
  }
}

public class Shader : IDisposable
{
  public string Name { get; private set; }
  public string FilePath { get; private set; }
  public int Id { get; private set; }
  private static string[] types = { "#vertex", "#fragment" };
  public Dictionary<string, UniformInfo> UniformInfos { get; private set; }

  public DateTime lastWriteTime;
  public event Action? Recompile;

  private Shader(string path)
  {
    var absPath = PathUtils.FromLocal(path);
    var fileName = Path.GetFileName(absPath);
    lastWriteTime = File.GetLastWriteTime(absPath);

    FilePath = absPath;
    Name = fileName;

    Compile();
  }

  public void Compile()
  {
    try
    {
      var fileContent = File.ReadAllText(FilePath);
      var shaders = Preprocess(fileContent);

      var vertexSrc = shaders[0].content;
      var fragmentSrc = shaders[1].content;

      var vertexShader = CompileShader(ShaderType.VertexShader, vertexSrc);
      var fragmentShader = CompileShader(ShaderType.FragmentShader, fragmentSrc);

      Id = CreateProgram(vertexShader, fragmentShader);
    }
    catch (Exception e)
    {
      if (ShadersController.ErrorShader is { } shader)
      {
        Id = shader.Id;
        Console.WriteLine("Using error shader!");
      }

      Console.WriteLine(e);
    }

    UniformInfos = ExtractUniformInfos();
    Recompile?.Invoke();
  }

  public static (string type, string content)[] Preprocess(string fileContent)
  {
    var startPositions = types.Select(
                                (type) => fileContent.IndexOf(type, StringComparison.Ordinal)
                              )
                              .ToArray();
    (int strart, int end)[] ranges = startPositions
                                     .Select(
                                       (pos, i) =>
                                       {
                                         if (i == startPositions.Length - 1)
                                           return (pos, fileContent.Length);

                                         return (pos, startPositions[i + 1]);
                                       }
                                     )
                                     .ToArray();

    var shaders = ranges
                  .Select(
                    (range, i) =>
                    {
                      var type = types[i];
                      var content = fileContent.Substring(
                                                 range.strart,
                                                 range.end - range.strart
                                               )
                                               .Remove(0, type.Length)
                                               .Trim();
                      return (
                        type,
                        content
                      );
                    }
                  )
                  .ToArray();
    return shaders;
  }

  public static Shader FromFile(string filePath)
  {
    return new Shader(filePath);
  }

  private int CompileShader(ShaderType type, string shaderSrc)
  {
    var shader = GL.CreateShader(type);
    GL.ShaderSource(shader, shaderSrc);
    GL.CompileShader(shader);

    GL.GetShader(
      shader,
      ShaderParameter.CompileStatus,
      out var code
    );
    if (code != (int)All.True)
    {
      var infoLog = GL.GetShaderInfoLog(shader);
      throw new Exception($"Failed to compile shader {shader}.\n{infoLog}");
    }

    return shader;
  }

  private int CreateProgram(int vertexShader, int fragmentShader)
  {
    var program = GL.CreateProgram();
    GL.AttachShader(program, vertexShader);
    GL.AttachShader(program, fragmentShader);

    GL.LinkProgram(program);

    GL.GetProgram(
      program,
      GetProgramParameterName.LinkStatus,
      out var code
    );
    if (code != (int)All.True) throw new Exception($"Failed to link program {program}");

    GL.DetachShader(program, vertexShader);
    GL.DetachShader(program, fragmentShader);
    GL.DeleteShader(vertexShader);
    GL.DeleteShader(fragmentShader);

    return program;
  }

  private Dictionary<string, UniformInfo> ExtractUniformInfos()
  {
    GL.GetProgram(
      Id,
      GetProgramParameterName.ActiveUniforms,
      out var numberOfUniforms
    );

    var uniformInfos = new Dictionary<string, UniformInfo>();
    for (var i = 0; i < numberOfUniforms; i++)
    {
      var key = GL.GetActiveUniform(
        Id,
        i,
        out _,
        out var type
      );
      var location = GL.GetUniformLocation(Id, key);
      uniformInfos.Add(
        key,
        new UniformInfo(
          key,
          location,
          type
        )
      );
    }

    return uniformInfos;
  }

  // The shader sources provided with this project use hardcoded layout(location)-s. If you want to do it dynamically,
  // you can omit the layout(location=X) lines in the vertex shader, and use this in VertexAttribPointer instead of the hardcoded values.
  public int GetAttribLocation(string attribName)
  {
    return GL.GetAttribLocation(Id, attribName);
  }

  ~Shader()
  {
    GL.DeleteProgram(Id);
    ShadersController.Remove(this);
  }

  public void Use()
  {
    GL.UseProgram(Id);
  }

  private bool _disposedValue = false;

  public void Dispose()
  {
    if (!_disposedValue)
    {
      GL.DeleteProgram(Id);

      _disposedValue = true;
    }
  }
}
