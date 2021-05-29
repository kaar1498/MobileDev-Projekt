using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MobileDev_Projekt.Annotations;

namespace MobileDev_Projekt.Models
{
  public class SelectStandardExercisePageModel : INotifyPropertyChanged
  {
    private ObservableCollection<ExerciseModel> _searchExerciseModels;

    public ObservableCollection<ExerciseModel> SearchExerciseModels
    {
      get => _searchExerciseModels;
      set
      {
        if (Equals(value, _searchExerciseModels)) return;
        _searchExerciseModels = value;
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