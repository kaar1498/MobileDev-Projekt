using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MobileDev_Projekt.Annotations;

namespace MobileDev_Projekt.Models
{
  public class ProgramModel : INotifyPropertyChanged
  {
    private string _name;
    private ObservableCollection<ExerciseModel> _exerciseModels = new();
    private bool _isStandard;

    public string Name
    {
      get => _name;
      set
      {
        if (value == _name) return;
        _name = value;
        OnPropertyChanged();
      }
    }

    public ObservableCollection<ExerciseModel> ExerciseModels
    {
      get => _exerciseModels;
      set
      {
        if (Equals(value, _exerciseModels)) return;
        _exerciseModels = value;
        OnPropertyChanged();
      }
    }

    public bool IsStandard
    {
      get => _isStandard;
      set
      {
        if (value == _isStandard) return;
        _isStandard = value;
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