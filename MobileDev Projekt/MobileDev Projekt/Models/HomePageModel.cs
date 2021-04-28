﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MobileDev_Projekt.Annotations;

namespace MobileDev_Projekt.Models
{
  public class HomePageModel : INotifyPropertyChanged
  {
    private ObservableCollection<ProgramModel> _programModels;

    public ObservableCollection<ProgramModel> ProgramModels
    {
      get => _programModels;
      set
      {
        if (Equals(value, _programModels)) return;
        _programModels = value;
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