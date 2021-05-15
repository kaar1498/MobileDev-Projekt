using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using MobileDev_Projekt.Annotations;
using Xamarin.Forms;

namespace MobileDev_Projekt.Models
{
  public class ImageModel : INotifyPropertyChanged
  {
    private byte[] _bytes;
    private string _description;

    public ImageSource Image => ImageSource.FromStream(() => new MemoryStream(_bytes));

    public byte[] Bytes
    {
      set
      {
        if (Equals(value, _bytes)) return;
        _bytes = value;
        OnPropertyChanged(nameof(Image));
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