using System;
using System.Threading.Tasks;
using MobileDev_Projekt.Entities;
using MobileDev_Projekt.Services;
using RestSharp;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileDev_Projekt.Pages
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class RegisterPage
  {
    public RegisterPage()
    {
      InitializeComponent();
    }

    private async Task CreateNewUser(string name, string username, string password, string confirmPassword,
      string email, string address, int phoneNumber)
    {
      await PopupNavigation.Instance.PushAsync(new BusyPopup("Creating user"));

      try
      {
        if (!CheckPassword(password, confirmPassword))
        {
          return;
        }

        var request = new RestRequest("/auth/register");
        request.AddJsonBody(new {username, password, name, email, address, phoneNumber});
        request.Method = Method.POST;
      
        var auth = await App.ApiService.ExecuteAsync<Auth>(request);
        if (auth is null)
        {
          DependencyService.Get<IMessage>().LongAlert("Error creating user");
          return;
        }
      
        App.ApiService.AuthenticateClient(auth.Jwt, auth.HostKey);

        Application.Current.MainPage = new NavigationPage(new HomePage());
      }
      catch
      {
        DependencyService.Get<IMessage>().LongAlert("Error creating user");
      }
      finally
      {
        await PopupNavigation.Instance.PopAsync();
      }
    }

    private async void CreateButton_Clicked(object sender, EventArgs e)
    {
      if (string.IsNullOrWhiteSpace(UserName.Text))
      {
        DependencyService.Get<IMessage>().LongAlert("Brugernavn skal være udfyldt");
        return;
      }

      if (string.IsNullOrWhiteSpace(NameEntry.Text))
      {
        DependencyService.Get<IMessage>().LongAlert("Navn skal være udfyldt");
        return;
      }

      if (string.IsNullOrWhiteSpace(PasswordEntry.Text))
      {
        DependencyService.Get<IMessage>().LongAlert("Adgangskode navn skal være udfyldt");
        return;
      }

      if (string.IsNullOrWhiteSpace(PasswordRepeatEntry.Text))
      {
        DependencyService.Get<IMessage>().LongAlert("Adgangskode navn skal være udfyldt");
        return;
      }

      if (string.IsNullOrWhiteSpace(EmailEntry.Text))
      {
        DependencyService.Get<IMessage>().LongAlert("Email navn skal være udfyldt");
        return;
      }

      if (string.IsNullOrWhiteSpace(Address.Text))
      {
        DependencyService.Get<IMessage>().LongAlert("Addresse navn skal være udfyldt");
        return;
      }

      if (string.IsNullOrWhiteSpace(PhoneNumberEntry.Text))
      {
        DependencyService.Get<IMessage>().LongAlert("Telefon nummer navn skal være udfyldt");
        return;
      }

      await CreateNewUser(NameEntry.Text, UserName.Text, PasswordEntry.Text, PasswordRepeatEntry.Text, EmailEntry.Text,
        Address.Text, int.Parse(PhoneNumberEntry.Text));
    }

    private static bool CheckPassword(string password, string confirmPassword)
    {
      return password == confirmPassword;
    }

    private void UndoButton_Clicked(object sender, EventArgs e)
    {
      Navigation.PopAsync();
    }
  }
}