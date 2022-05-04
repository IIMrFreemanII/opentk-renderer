using System.ComponentModel;
using System.Reflection;

namespace open_tk_renderer.temp;

public class Bindings
{
  public Dictionary<string, string> notifyPropToObjPropMap;
  public INotifyPropertyChanged objectNotifier;
  public object? obj;
  private List<PropertyInfo> _notifyPropertyInfos = new();
  private List<PropertyInfo> _objPropertyInfos = new();

  public Bindings(object objectNotifier, Dictionary<string, string> notifyPropToObjPropMap)
  {
    this.notifyPropToObjPropMap = notifyPropToObjPropMap;
    this.objectNotifier = (INotifyPropertyChanged)objectNotifier;

    foreach (var map in notifyPropToObjPropMap)
    {
      var notifyPropName = map.Key;
      var notifyPropInfo = objectNotifier.GetType().GetProperty(notifyPropName);
      _notifyPropertyInfos.Add(notifyPropInfo);
    }
  }

  ~Bindings()
  {
    objectNotifier.PropertyChanged -= OnChange;
  }

  public void BindTo(object obj)
  {
    this.obj = obj;

    foreach (var map in notifyPropToObjPropMap)
    {
      var objPropName = map.Value;
      var objPropInfo = obj.GetType().GetProperty(objPropName);
      _objPropertyInfos.Add(objPropInfo);
    }
    
    objectNotifier.PropertyChanged += OnChange;
  }

  private void OnChange(object? objNotifier, PropertyChangedEventArgs e)
  {
    for (int i = 0; i < _notifyPropertyInfos.Count; i++)
    {
      if (_notifyPropertyInfos[i].Name == e.PropertyName)
      {
        var notifyPropInfo = _notifyPropertyInfos[i];
        var objPropInfo = _objPropertyInfos[i];
        var newValue = notifyPropInfo.GetValue(objNotifier);
        objPropInfo.SetValue(obj, newValue);
      }
    }
  }
}
