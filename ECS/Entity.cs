namespace open_tk_renderer.ECS;

public struct Entity
{
  public static int count = 1;
  public int id;

  public Entity()
  {
    id = count++;
  }
}
