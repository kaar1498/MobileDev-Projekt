using Android.App;
using Android.Widget;
using MobileDev_Projekt.Droid.Services;
using MobileDev_Projekt.Services;

[assembly: Xamarin.Forms.Dependency(typeof(MessageAndroid))]
namespace MobileDev_Projekt.Droid.Services
{
  public class MessageAndroid : IMessage
  {
    public void LongAlert(string message)
    {
      Toast.MakeText(Application.Context, message, ToastLength.Long)?.Show();
    }

    public void ShortAlert(string message)
    {
      Toast.MakeText(Application.Context, message, ToastLength.Short)?.Show();
    }
  }
}