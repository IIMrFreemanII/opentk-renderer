namespace open_tk_renderer.ECS;

public class View
{
  
}

public class Registry
{
  private List<Entity> _entities = new(1000);
  private Dictionary<Type, List<object>> _views = new();

  public Entity Create()
  {
    var entity = new Entity();
    _entities.Add(entity);
    return entity;
  }

  public void Emplace<T1>(Entity entity, T1 component) where T1 : struct
  {
    if (_views.TryGetValue(typeof(T1), out List<object> temp))
    {
      temp.Add(component); 
    }
    else
    {
      var view = new List<object> { component };
      _views.Add(typeof(T1), view);
    }
  }

  public List<T1>? View<T1>()
  {
    return _views[typeof(T1)] as List<T1>;
  }
}
