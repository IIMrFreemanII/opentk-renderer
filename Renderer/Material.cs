using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace open_tk_renderer.Renderer;

public delegate void Setter(int location, ref object value);

public class UniformData
{
  public string name;
  public int location;
  public object value;
  public Setter setter;

  public UniformData(
    string name,
    int location,
    object value,
    Setter setter
  )
  {
    this.name = name;
    this.location = location;
    this.value = value;
    this.setter = setter;
  }

  public void SetUniform()
  {
    setter(location, ref value);
  }
}

public class Material
{
  private static readonly Dictionary<ActiveUniformType, object> TypeToDefaultValue = new()
  {
    { ActiveUniformType.Bool, false },
    { ActiveUniformType.Float, 0.0f },
    { ActiveUniformType.Int, 0 },
    { ActiveUniformType.FloatVec2, new Vector2() },
    { ActiveUniformType.FloatVec3, new Vector3() },
    { ActiveUniformType.FloatVec4, new Vector4() },
    { ActiveUniformType.FloatMat3, new Matrix3() },
    { ActiveUniformType.FloatMat4, new Matrix4() }
  };

  private static readonly Dictionary<ActiveUniformType, Setter> TypeToSetter = new()
  {
    {
      ActiveUniformType.Bool,
      (int location, ref object value) => { GL.Uniform1(location, (int)value); }
    },
    {
      ActiveUniformType.Float,
      (int location, ref object value) => { GL.Uniform1(location, (float)value); }
    },
    {
      ActiveUniformType.Int,
      (int location, ref object value) => { GL.Uniform1(location, (int)value); }
    },
    {
      ActiveUniformType.FloatVec2,
      (int location, ref object value) => { GL.Uniform2(location, (Vector2)value); }
    },
    {
      ActiveUniformType.FloatVec3,
      (int location, ref object value) => { GL.Uniform3(location, (Vector3)value); }
    },
    {
      ActiveUniformType.FloatVec4,
      (int location, ref object value) => { GL.Uniform4(location, (Vector4)value); }
    },
    {
      ActiveUniformType.FloatMat3, (int location, ref object value) =>
      {
        var matrix3 = (Matrix3)value;
        GL.UniformMatrix3(
          location,
          false,
          ref matrix3
        );
      }
    },
    {
      ActiveUniformType.FloatMat4, (int location, ref object value) =>
      {
        var matrix4 = (Matrix4)value;
        GL.UniformMatrix4(
          location,
          false,
          ref matrix4
        );
      }
    }
  };

  public string name;
  public Shader shader;
  public Dictionary<string, UniformData> uniforms;

  public Material(string name, Shader shader)
  {
    this.name = name;
    this.shader = shader;
    this.shader.Recompile += ExtractUniforms;

    ExtractUniforms();
  }

  private void ExtractUniforms()
  {
    var uniforms = new Dictionary<string, UniformData>();
    foreach (var uniformInfo in shader.UniformInfos)
      uniforms.Add(
        uniformInfo.Key,
        new UniformData(
          uniformInfo.Key,
          uniformInfo.Value.location,
          TypeToDefaultValue[uniformInfo.Value.type],
          TypeToSetter[uniformInfo.Value.type]
        )
      );

    this.uniforms = uniforms;
  }

  public void SetUniforms()
  {
    foreach (var uniform in uniforms) uniform.Value.SetUniform();
  }

  public void SetFloat(string name, float value)
  {
    if (uniforms.TryGetValue(name, out var uniform)) uniform.value = value;
  }

  public void SetInt(string name, int value)
  {
    if (uniforms.TryGetValue(name, out var uniform)) uniform.value = value;
  }

  public void SetVector(string name, Vector2 value)
  {
    if (uniforms.TryGetValue(name, out var uniform)) uniform.value = value;
  }

  public void SetVector(string name, Vector3 value)
  {
    if (uniforms.TryGetValue(name, out var uniform)) uniform.value = value;
  }

  public void SetVector(string name, Vector4 value)
  {
    if (uniforms.TryGetValue(name, out var uniform)) uniform.value = value;
  }

  public void SetMatrix(string name, Matrix3 value)
  {
    if (uniforms.TryGetValue(name, out var uniform)) uniform.value = value;
  }

  public void SetMatrix(string name, Matrix4 value)
  {
    if (uniforms.TryGetValue(name, out var uniform)) uniform.value = value;
  }
}
