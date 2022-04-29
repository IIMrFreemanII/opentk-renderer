namespace open_tk_renderer.ECS;

public class View
{
  // components

  // method to iterate on data (using ref modifier)
}

public class ComponentData
{
  public Type type;
  // component index in components arr
  public int location;
  public int entityId;
}

public class Registry
{
  private List<Entity> _entities = new(1000);
  // maps entities to its components
  private Dictionary<int, List<ComponentData>> _entityIndexToComponentData = new(1000);
  private Dictionary<Type, List<IComponent>> _typeToComponents = new(20);

  public Entity Create()
  {
    var entity = new Entity();
    _entities.Add(entity);
    return entity;
  }
  public void Delete(Entity entity)
  {
    
  }

  public void AddComponent<T1>(Entity entity, T1 component) where T1 : IComponent
  {
    int location;
    if (_typeToComponents.TryGetValue(typeof(T1), out List<IComponent> components))
    {
      location = components.Count;
      components.Add(component);
    }
    else
    {
      location = 0;
      _typeToComponents.Add(typeof(T1), new() { component });
    }

    if (_entityIndexToComponentData.TryGetValue(
      entity.id,
      out List<ComponentData> componentsData
    ))
    {
      componentsData.Add(
        new ComponentData
        {
          type = typeof(T1),
          location = location,
          entityId = entity.id
        }
      );
    }
    else
    {
      _entityIndexToComponentData.Add(
        entity.id,
        new()
        {
          new ComponentData
          {
            type = typeof(T1),
            location = location,
            entityId = entity.id
          }
        }
      );
    }
  }

  public void RemoveComponent<T1>(Entity entity) where T1 : IComponent
  {
    
  }

  public void GetComponent<T1>(Entity entity) where T1 : IComponent
  {
    
  }

  // iterate over a view of components
  // public List<T1>? View<T1>()
  // {
  //   return _views[typeof(T1)] as List<T1>;
  // }
}
