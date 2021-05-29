using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MobileDev_Projekt.Annotations;

namespace MobileDev_Projekt.Models
{
  public class ExerciseModel : INotifyPropertyChanged
  {
    private string _name;
    private ObservableCollection<ImageModel> _imageModels = new();
    private int? _duration;
    private int? _restFrequency;
    private int? _restDuration;
    private int? _repetitions;
    private bool _isStandard;

    public Guid Id { get; set; }
    public int? Duration
    {
      get => _duration;
      set
      {
        if (value == _duration) return;
        _duration = value;
        OnPropertyChanged();
      }
    }

    public int? Repetitions
    {
      get => _repetitions;
      set
      {
        if (value == _repetitions) return;
        _repetitions = value;
        OnPropertyChanged();
      }
    }

    public int? RestFrequency
    {
      get => _restFrequency;
      set
      {
        if (value == _restFrequency) return;
        _restFrequency = value;
        OnPropertyChanged();
      }
    }

    public int? RestDuration
    {
      get => _restDuration;
      set
      {
        if (value == _restDuration) return;
        _restDuration = value;
        OnPropertyChanged();
      }
    }

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

    public ObservableCollection<ImageModel> ImageModels
    {
      get => _imageModels;
      set
      {
        _imageModels = value;
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