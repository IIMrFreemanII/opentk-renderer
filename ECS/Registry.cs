namespace open_tk_renderer.ECS;

public class View
{
  public Registry registry;
  // public List<IComponent> components;
  public List<Type> types;
  // components
  public View(Registry registry, List<Type> types) { }


  // method to iterate on data (using ref modifier)
}

public class ComponentData
{
  public Type type;
  // component index in components arr
  public int location;
  public int entityId;

  public ComponentData(
    Type type,
    int location,
    int entityId
  )
  {
    this.type = type;
    this.location = location;
    this.entityId = entityId;
  }
}

public class Registry
{
  private List<Entity> _entities = new(1000);
  // maps entities to its components
  private Dictionary<int, List<ComponentData>> _entityIndexToComponentData = new(1000);
  public Dictionary<Type, List<IComponent>> typeToComponents = new(20);

  public void Clear()
  {
    _entities.Clear();
    _entityIndexToComponentData.Clear();
    foreach (var typeToComponent in typeToComponents)
    {
      typeToComponent.Value.Clear();
    }
  }

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
        typeToComponents[componentData.type].RemoveAt(componentData.location);
      }

      componentsData.Clear();
    }
  }

  public void AddComponent<T1>(Entity entity, T1 component) where T1 : IComponent
  {
    Type type = typeof(T1);
    int location;
    if (typeToComponents.TryGetValue(type, out var components))
    {
      location = components.Count;
      components.Add(component);
    }
    else
    {
      location = 0;
      typeToComponents.Add(type, new() { component });
    }

    if (_entityIndexToComponentData.TryGetValue(
      entity.id,
      out var componentsData
    ))
    {
      componentsData.Add(
        new ComponentData(
          type,
          location,
          entity.id
        )
      );
    }
    else
    {
      _entityIndexToComponentData.Add(
        entity.id,
        new()
        {
          new ComponentData(
            type,
            location,
            entity.id
          )
        }
      );
    }
  }

  public void RemoveComponent<T1>(Entity entity) where T1 : IComponent
  {
    if (_entityIndexToComponentData.TryGetValue(entity.id, out var entityComponentsData))
    {
      var index =
        entityComponentsData.FindIndex((componentData) => componentData.type == typeof(T1));
      if (index != -1)
      {
        var componentData = entityComponentsData[index];
        typeToComponents[componentData.type].RemoveAt(componentData.location);
        entityComponentsData.RemoveAt(index);
      }
    }
  }

  // public void RemoveComponent(Entity entity, Type type)
  // {
  //   if (_entityIndexToComponentData.TryGetValue(entity.id, out var entityComponentsData))
  //   {
  //     var index = entityComponentsData.FindIndex((componentData) => componentData.type == type);
  //     if (index != -1)
  //     {
  //       var componentData = entityComponentsData[index];
  //       typeToComponents[componentData.type].RemoveAt(componentData.location);
  //       entityComponentsData.RemoveAt(index);
  //     }
  //   }
  // }

  public T1? GetComponent<T1>(Entity entity) where T1 : IComponent
  {
    if (_entityIndexToComponentData.TryGetValue(entity.id, out var componentsData))
    {
      var componentData =
        componentsData.Find((componentData) => componentData.type == typeof(T1));
      if (componentData is { })
      {
        return (T1)typeToComponents[componentData.type][componentData.location];
      }
    }

    return default;
  }

  public bool HasComponent<T>(Entity entity) where T : IComponent
  {
    if (_entityIndexToComponentData.TryGetValue(entity.id, out var componentsData))
    {
      var componentData = componentsData.Find((componentData) => componentData.type == typeof(T));
      if (componentData is { })
      {
        return true;
      }
    }

    return false;
  }


  public delegate void ForEachDelegate<T>(Entity entity, ref T component)
    where T : IComponent;

  // todo: support multiple generics
  public void ForEach<T>(ForEachDelegate<T> action)
    where T : IComponent
  {
    List<int> locations = new(100);
    List<Entity> entities = new(100);

    Type type = typeof(T);
    foreach (var data in _entityIndexToComponentData)
    {
      var temp = data.Value.Find(
        componentData => componentData.type == type
      );
      if (temp is { } componentData)
      {
        locations.Add(componentData.location);
        entities.Add(_entities[temp.entityId]);
      }
    }

    for (int i = 0; i < entities.Count; i++)
    {
      var list = typeToComponents[type];
      int location = locations[i];

      // todo: optimize (in, ref) when in don't copy back
      T temp = (T)list[location];
      action(entities[i], ref temp);
      list[location] = temp;
    }
  }
}
