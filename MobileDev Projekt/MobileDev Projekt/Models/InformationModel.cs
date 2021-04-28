using System.ComponentModel;
using System.Runtime.CompilerServices;
using MobileDev_Projekt.Annotations;

namespace MobileDev_Projekt.Models
{
  public class InformationModel : INotifyPropertyChanged
  {
    private string _title;
    private string _value;

    public string Title
    {
      get => _title;
      set
      {
        if (value == _title) return;
        _title = value;
        OnPropertyChanged();
      }
    }

    public string Value
    {
      get => _value;
      set
      {
        if (value == _value) return;
        _value = value;
        OnPropertyChanged();
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}