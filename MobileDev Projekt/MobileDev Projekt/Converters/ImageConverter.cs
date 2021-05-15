using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using MobileDev_Projekt.Models;
using Xamarin.Forms;

namespace MobileDev_Projekt.Converters
{
  public class ImageConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      var data = (IEnumerable<ImageModel>) value;
      return data.FirstOrDefault()?.Image;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}