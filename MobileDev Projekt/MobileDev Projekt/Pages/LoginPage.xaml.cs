using System;
using MobileDev_Projekt.Entities;
using MobileDev_Projekt.Services;
using RestSharp;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileDev_Projekt.Pages
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class LoginPage
  {
    public LoginPage()
    {
      InitializeComponent();
    }

    private void RegisterButton_Clicked(object sender, EventArgs e)
    {
      Navigation.PushAsync(new RegisterPage());
    }

    private async void LoginButton_Clicked(object sender, EventArgs e)
    {
      await PopupNavigation.Instance.PushAsync(new BusyPopup("Logging in"));
      try
      {
        var username = UsernameEntry.Text ??= "Test";
        var password = PasswordEntry.Text ??= "123";
        if (string.IsNullOrWhiteSpace(username))
        {
          DependencyService.Get<IMessage>().LongAlert("You must enter a username");
          return;
        }
        if (string.IsNullOrWhiteSpace(password))
        {
          DependencyService.Get<IMessage>().LongAlert("You must enter a password");
          return;
        }
  
        var request = new RestRequest("/auth/login");
        request.AddJsonBody(new {username,password});
        request.Method = Method.POST;
        
        var auth = await App.ApiService.ExecuteAsync<Auth>(request);
        if (auth is null)
        {
          DependencyService.Get<IMessage>().LongAlert("Error logging in");
          return;
        }
        
        App.ApiService.AuthenticateClient(auth.Jwt, auth.HostKey);
        
        Application.Current.MainPage = new NavigationPage(new HomePage());
      }
      catch
      {
        DependencyService.Get<IMessage>().LongAlert("Error logging in");
      }
      finally
      {
        await PopupNavigation.Instance.PopAsync();
      }
    }
  }
}