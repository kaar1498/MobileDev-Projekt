using System;
using MobileDev_Projekt.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileDev_Projekt.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private readonly RestClient _restClient;
        public LoginPage()
        {
            InitializeComponent();
            _restClient = new RestClient();
        }

        private void RegisterButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterPage());
        }

        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            var username = UsernameEntry.Text;
            var password = PasswordEntry.Text;
            
            var isValid = !string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password);
            isValid = await _restClient.Login() && isValid; //TODO Validate with API
            
            if (!isValid)
            {
                DependencyService.Get<IMessage>().LongAlert("Username or Password not valid");
                return;
            }
            
            await Navigation.PushAsync(new HomePage());
        }
    }
}