using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileDev_Projekt.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileDev_Projekt.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        private readonly RestClient _restClient;
        public RegisterPage()
        {
            InitializeComponent();
            _restClient = new RestClient();
        }
        
        private async Task CreateNewUser(string name, string username, string password, string confirmPassword,
            string email, string address, int phoneNumber)
        {
            IsBusy = true;
            try
            {
                if (!CheckPassword(password, confirmPassword))
                {
                    return;
                }
                
                if (await _restClient.Register(name, username, password, email, address, phoneNumber))
                {
                    // hvis brugeren blev oprette og posted til serveren så redirect brugeren til mainPage.
                    await Navigation.PushModalAsync(new HomePage());
                }
            }
            catch (Exception e)
            {
                await DisplayAlert("Error", e.Message, "OK");
            }
            finally
            {
                IsBusy = false;
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
            await CreateNewUser(NameEntry.Text, UserName.Text, PasswordEntry.Text, PasswordRepeatEntry.Text, EmailEntry.Text, Address.Text, int.Parse(PhoneNumberEntry.Text));
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