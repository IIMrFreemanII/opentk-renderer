namespace open_tk_renderer.Renderer;

public class MaterialsController
{
  public static List<Material> materials = new();

  public static Material Create(string name, Shader shader)
  {
    var material = new Material(name, shader);
    Add(material);
    return material;
  }

  public static Material? Get(string name)
  {
    return materials.Find((material) => material.name == name);
  }

  public static void Add(Material material)
  {
    materials.Add(material);
  }

  public static void Remove(Material material)
  {
    materials.Remove(material);
  }

  public static void Remove(string name)
  {
    int index = materials.FindIndex((material) => material.name == name);
    materials.RemoveAt(index);
  }
}
