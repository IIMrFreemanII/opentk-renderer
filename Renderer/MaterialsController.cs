namespace open_tk_renderer.Renderer;

public class MaterialsController
{
  public static readonly List<Material> Materials = new();

  public static Material Create(string name, Shader shader)
  {
    var material = new Material(name, shader);
    Add(material);
    return material;
  }

  public static Material? Get(string name)
  {
    return Materials.Find((material) => material.name == name);
  }

  public static void Add(Material material)
  {
    Materials.Add(material);
  }

  public static void Remove(Material material)
  {
    Materials.Remove(material);
  }

  public static void Remove(string name)
  {
    var index = Materials.FindIndex((material) => material.name == name);
    Materials.RemoveAt(index);
  }
}
