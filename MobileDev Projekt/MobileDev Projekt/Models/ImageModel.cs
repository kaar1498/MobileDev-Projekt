﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using MobileDev_Projekt.Annotations;
using Xamarin.Forms;

namespace MobileDev_Projekt.Models
{
  public class ImageModel : INotifyPropertyChanged
  {
    private ImageSource _image;
    private string _description;

    public ImageSource Image
    {
      get => _image;
      set
      {
        if (Equals(value, _image)) return;
        _image = value;
        OnPropertyChanged();
      }
    }

    public string Description
    {
      get => _description;
      set
      {
        if (value == _description) return;
        _description = value;
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