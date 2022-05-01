namespace open_tk_renderer.ECS;

public class View
{
  // components

  // method to iterate on data (using ref modifier)
}

public class ComponentData
{
  public string type = string.Empty;
  // component index in components arr
  public int location;
  public int entityId;
}

public class Registry
{
  private List<Entity> _entities = new(1000);
  // maps entities to its components
  private Dictionary<int, List<ComponentData>> _entityIndexToComponentData = new(1000);
  private Dictionary<string, List<IComponent>> _typeToComponents = new(20);

  /// <summary>
  /// Creates entity
  /// </summary>
  /// <returns></returns>
  public Entity Create()
  {
    var entity = new Entity();
    _entities.Add(entity);
    return entity;
  }
  
  /// <summary>
  /// Finds entity by id
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  public Entity Find(int id)
  {
    return _entities[id];
  }
  
  /// <summary>
  /// Deletes entity
  /// </summary>
  /// <param name="entity"></param>
  public void Delete(Entity entity)
  {
    if (_entityIndexToComponentData.TryGetValue(entity.id, out var componentsData))
    {
      for (int i = 0; i < componentsData.Count; i++)
      {
        var componentData = componentsData[i];
        _typeToComponents[componentData.type].RemoveAt(componentData.location);
      }

      componentsData.Clear();
    }
  }

  public void AddComponent<T1>(Entity entity, T1 component) where T1 : IComponent
  {
    int location;
    if (_typeToComponents.TryGetValue(nameof(T1), out List<IComponent> components))
    {
      location = components.Count;
      components.Add(component);
    }
    else
    {
      location = 0;
      _typeToComponents.Add(nameof(T1), new() { component });
    }

    if (_entityIndexToComponentData.TryGetValue(
      entity.id,
      out var componentsData
    ))
    {
      componentsData.Add(
        new ComponentData
        {
          type = nameof(T1),
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
            type = nameof(T1),
            location = location,
            entityId = entity.id
          }
        }
      );
    }
  }

  public void RemoveComponent<T1>(Entity entity) where T1 : IComponent
  {
    if (_entityIndexToComponentData.TryGetValue(entity.id, out var entityComponentsData))
    {
      var index =
        entityComponentsData.FindIndex((componentData) => componentData.type == nameof(T1));
      if (index != -1)
      {
        var componentData = entityComponentsData[index];
        _typeToComponents[componentData.type].RemoveAt(componentData.location);
        entityComponentsData.RemoveAt(index);
      }
    }
  }

  public void RemoveComponent(Entity entity, string type)
  {
    if (_entityIndexToComponentData.TryGetValue(entity.id, out var entityComponentsData))
    {
      var index = entityComponentsData.FindIndex((componentData) => componentData.type == type);
      if (index != -1)
      {
        var componentData = entityComponentsData[index];
        _typeToComponents[componentData.type].RemoveAt(componentData.location);
        entityComponentsData.RemoveAt(index);
      }
    }
  }

  public T1? GetComponent<T1>(Entity entity) where T1 : IComponent
  {
    if (_entityIndexToComponentData.TryGetValue(entity.id, out var componentsData))
    {
      var componentData = componentsData.Find((componentData) => componentData.type == nameof(T1));
      if (componentData is { })
      {
        return (T1)_typeToComponents[componentData.type][componentData.location];
      }
    }

    return default;
  }

  public bool HasComponent<T1>(Entity entity) where T1 : IComponent
  {
    if (_entityIndexToComponentData.TryGetValue(entity.id, out var componentsData))
    {
      var componentData = componentsData.Find((componentData) => componentData.type == nameof(T1));
      if (componentData is { })
      {
        return true;
      }
    }
    
    return false;
  }

  // iterate over a view of components
  // public List<T1>? View<T1>()
  // {
  //   return _views[typeof(T1)] as List<T1>;
  // }
}
