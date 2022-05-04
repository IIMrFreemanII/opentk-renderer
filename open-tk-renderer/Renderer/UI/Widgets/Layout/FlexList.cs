using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace open_tk_renderer.Renderer.UI.Widgets.Layout;

public class FlexList<T> : Flex
{
  public ObservableCollection<T> observableCollection;
  public Func<T, Widget> onAdd;
  public FlexList(ObservableCollection<T> observableCollection, Func<T, Widget> onAdd)
  {
    this.onAdd = onAdd;
    this.observableCollection = observableCollection;
    this.observableCollection.CollectionChanged += OnCollectionChanged;

    children = this.observableCollection.Select(
                     (item) =>
                     {
                       var widget = this.onAdd(item);
                       widget.parent = this;
                       return widget;
                     }
                   )
                   .ToList();
  }

  ~FlexList()
  {
    observableCollection.CollectionChanged -= OnCollectionChanged;
  }

  private void OnCollectionChanged(object? obj, NotifyCollectionChangedEventArgs e)
  {
    switch (e.Action)
    {
      case NotifyCollectionChangedAction.Add:
      {
        if (e.NewItems?[0] is T newItem)
        {
          Append(onAdd(newItem));
        }

        break;
      }
      case NotifyCollectionChangedAction.Remove:
      {
        RemoveAt(e.OldStartingIndex);
        break;
      }
      case NotifyCollectionChangedAction.Replace:
      {
        if (e.NewItems?[0] is T newItem)
        {
          Replace(e.OldStartingIndex, onAdd(newItem));
        }

        break;
      }
      case NotifyCollectionChangedAction.Reset:
      {
        Clear();
        break;
      }
    }
  }
}
