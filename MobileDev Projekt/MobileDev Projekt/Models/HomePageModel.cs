using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MobileDev_Projekt.Annotations;

namespace MobileDev_Projekt.Models
{
  public class HomePageModel : INotifyPropertyChanged
  {
    private ObservableCollection<ProgramModel> _standardProgramModels;
    private ObservableCollection<ExerciseModel> _standardExerciseModels = new();

    public ObservableCollection<ProgramModel> StandardProgramModels
    {
      get => _standardProgramModels;
      set
      {
        if (Equals(value, _standardProgramModels)) return;
        _standardProgramModels = value;
        OnPropertyChanged();
      }
    }

    public ObservableCollection<ExerciseModel> StandardExerciseModels
    {
      get => _standardExerciseModels;
      set
      {
        if (Equals(value, _standardExerciseModels)) return;
        _standardExerciseModels = value;
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