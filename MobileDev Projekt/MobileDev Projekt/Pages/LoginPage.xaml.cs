using System;
using MobileDev_Projekt.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileDev_Projekt.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private readonly FastCastleApi _fastCastleApi;
        public LoginPage()
        {
            InitializeComponent();
            _fastCastleApi = new FastCastleApi();
        }

        private void RegisterButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterPage());
        }

        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            var username = UsernameEntry.Text ??= "Test@Test.Test";
            var password = PasswordEntry.Text ??= "Test";
            
            var isValid = !string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password);
            isValid = await _fastCastleApi.Login(username, password) && isValid; //How can we validate password with strapi?
            
            if (!isValid)
            {
                DependencyService.Get<IMessage>().LongAlert("Username or Password not valid");
                return;
            }
            
            Application.Current.MainPage = new NavigationPage(new HomePage());
        }
    }
}