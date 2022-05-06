namespace open_tk_renderer.ECS;

// public struct EntityComponent
// {
//   public Type type;
//   // component index in components arr
//   public int location;
//   // public int entityId;
//
//   public EntityComponent(
//     Type type,
//     int location
//     // int entityId
//   )
//   {
//     this.type = type;
//     this.location = location;
//     // this.entityId = entityId;
//   }
// }

public class Archetype
{
  public Dictionary<Type, List<IComponent>> typeToComponent;
  public List<Entity> entities;
  public List<Type> types;
  public bool typeless;
  private int _size = 1000;
  
  public Archetype(params Type[] types)
  {
    if (types.Length == 0)
    {
      typeless = true;
    }
    
    this.types = new(types);
    entities = new(_size);

    typeToComponent = new();
    foreach (var type in types)
    {
      typeToComponent.Add(type, new(_size));
    }
  }

  public void Add(Entity entity)
  {
    entities.Add(entity);
    for (int i = 0; i < types.Count; i++)
    {
      var type = types[i];
      var component = Activator.CreateInstance(type);
      if (component is IComponent temp)
      {
        typeToComponent[type].Add(temp);
      }
    }
  }
  
  public void Remove(Entity entity)
  {
    int index = entities.FindIndex(
      (item) => item.id == entity.id
    );
    
    if (index == -1)
    {
      throw new Exception("Entity not found");
    }
    
    for (int i = 0; i < types.Count; i++)
    {
      typeToComponent[types[i]].RemoveAt(index);
    }
    
    entities.RemoveAt(index);
  }

  public int IndexOf(Entity entity)
  {
    return entities.FindIndex(
      (item) => item.id == entity.id
    );
  }

  public bool Contains(Entity entity)
  {
    return entities.Contains(entity);
  }
  
  public bool Contains(Type type)
  {
    return types.Contains(type);
  }
  
  public bool Contains(Type[] types)
  {
    int count = 0;

    for (int i = 0; i < types.Length; i++)
    {
      if (this.types.Contains(types[i]))
      {
        count++;
      }
    }

    return count == types.Length;
  }
}

// todo: how to compare archetypes
// todo: how to add archetype to entity
// todo: how to move entity from one to another archetype
// todo: how to add component to entity and move to another archetype
// todo: how to query components and entities

public class Registry
{
  public int entitiesCount = 0;
  public List<Archetype> archetypes = new() { new Archetype() };
  
  // private List<Entity> _entities = new(1000);
  
  // maps entities to its components
  // private List<List<EntityComponent>> _entityIndexToItsComponents = new(1000);
  // public Dictionary<Type, List<IComponent>> typeToComponents = new(20);

  public Entity Create()
  {
    var entity = new Entity(entitiesCount++);
    for (int i = 0; i < archetypes.Count; i++)
    {
      var archetype = archetypes[i];
      if (archetype.typeless)
      {
        archetype.Add(entity);
      }
    }
    return entity;
  }

  public Archetype CreateArchetype(params Type[] types)
  {
    for (int i = 0; i < archetypes.Count; i++)
    {
      var archetype = archetypes[i];
      if (archetype.Contains(types))
      {
        return archetype;
      }
    }
    return new Archetype(types);
  }
  
  public Entity Create(Archetype archetype)
  {
    var entity = new Entity(entitiesCount++);
    int index = archetypes.IndexOf(archetype);
    if (index != -1)
    {
      archetypes[index].Add(entity);
    }
    else
    {
      throw new Exception("Archetype not found");
    }
    return entity;
  }

  /// <summary>
  /// Finds entity by id
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  public Entity Find(int id)
  {
    // return _entities[id];
    throw new Exception();
  }

  /// <summary>
  /// Deletes entity
  /// </summary>
  /// <param name="entity"></param>
  public void Delete(Entity entity)
  {
    throw new Exception();
  }

  public void AddComponent<T1>(Entity entity, T1 component) where T1 : IComponent
  {
    throw new Exception();
  }
  
  public T GetComponent<T>(Entity entity, T component) where T : IComponent, new()
  {
    var type = typeof(T);
    
    for (int i = 0; i < archetypes.Count; i++)
    {
      var archetype = archetypes[i];
      int entityIndex = archetype.IndexOf(entity);
      if (entityIndex != -1)
      {
        return (T)archetype.typeToComponent[type][entityIndex];
      }
    }

    return new T();
  }
  
  public void SetComponent<T1>(Entity entity, T1 component) where T1 : IComponent
  {
    throw new Exception();
  }

  public void RemoveComponent<T1>(Entity entity) where T1 : IComponent
  {
    throw new Exception();
  }

  public T1? GetComponent<T1>(Entity entity) where T1 : IComponent
  {
    throw new Exception();
  }

  public bool HasComponent<T>(Entity entity) where T : IComponent
  {
    throw new Exception();
  }


  public delegate void ForEachDelegate<T>(Entity entity, ref T component)
    where T : IComponent;

  // todo: support multiple generics
  public void ForEach<T>(ForEachDelegate<T> action)
    where T : IComponent
  {
    var type = typeof(T);
    
    for (int i = 0; i < archetypes.Count; i++)
    {
      var archetype = archetypes[i];

      if (archetype.Contains(type))
      {
        var entities = archetype.entities;
        var components = archetype.typeToComponent[type] as List<T>;
        
        for (int j = 0; j < entities.Count; j++)
        {
          T component = components[i];
          action(entities[i], ref component);
          components[i] = component;
        }
      }
    }
  }
}
