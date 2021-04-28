using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MobileDev_Projekt.Annotations;

namespace MobileDev_Projekt.Models
{
  public class NewProgramPageModel : INotifyPropertyChanged
  {
    private ObservableCollection<ExerciseModel> _exerciseModels;
    private InformationModel _informationModel;

    public NewProgramPageModel()
    {
      InformationModel = new InformationModel
      {
        Title = "Titel"
      };

      ExerciseModels = new ObservableCollection<ExerciseModel>();
    }

    public InformationModel InformationModel
    {
      get => _informationModel;
      set
      {
        if (Equals(value, _informationModel)) return;
        _informationModel = value;
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

    public event PropertyChangedEventHandler PropertyChanged;

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }

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

  public class ExerciseModel : INotifyPropertyChanged
  {
    private string _name;
    private string _image;

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

    public string Image
    {
      get => _image;
      set
      {
        if (value == _image) return;
        _image = value;
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