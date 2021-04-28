using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileDev_Projekt.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileDev_Projekt.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void RegisterButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterPage());
        }

        private void LoginButton_Clicked(object sender, EventArgs e)
        {
            var username = UsernameEntry.Text;
            var password = PasswordEntry.Text;
            var isValid = !string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password); //TODO Validate with API
            if (!isValid)
            {
                DependencyService.Get<IMessage>().LongAlert("Username or Password not valid");
                return;
            }
            Navigation.PushAsync(new HomePage());
        }
    }
}