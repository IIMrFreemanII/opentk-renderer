namespace open_tk_renderer.ECS;

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

public class ArchetypeChunk
{
  public Dictionary<Type, List<IComponent>> typeToComponentsMap = new();
  public List<Entity> entities;
  public int size;
  public bool isFull;

  public ArchetypeChunk(Type[] types, int size)
  {
    this.size = size;
    entities = new(size);

    for (int i = 0; i < types.Length; i++)
    {
      typeToComponentsMap.Add(types[i], new (size));
    }
  }

  public void Add(Entity entity)
  {
    if (isFull)
    {
      return;
    }

    entities.Add(entity);
    // foreach (var typeToComponent in typeToComponentsMap)
    // {
    //   var component = Activator.CreateInstance(typeToComponent.Key);
    //   if (component is { })
    //   {
    //     typeToComponent.Value.Add((IComponent)component); 
    //   }
    // }
    if (entities.Count == size)
    {
      isFull = true;
    }
  }

  public void Remove(Entity entity)
  {
    for (int i = 0; i < entities.Count; i++)
    {
      if (entities[i].id == entity.id)
      {
        entities.RemoveAt(i);
        isFull = false;
        break;
      }
    }
  }
}

public class Archetype
{
  public List<ArchetypeChunk> chunks = new();
  public Type[] types;
  public Archetype(params Type[] types)
  {
    this.types = types;
  }
}

public class Registry
{
  public List<Archetype> archetypes = new();


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
