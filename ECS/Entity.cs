namespace open_tk_renderer.ECS;

public class Entity
{
  public static int count = 0;
  public int id;

  public Entity()
  {
    id = count++;
  }
}
