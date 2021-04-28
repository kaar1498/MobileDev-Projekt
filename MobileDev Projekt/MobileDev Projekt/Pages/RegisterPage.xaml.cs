using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileDev_Projekt.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private bool PostUser(string username, string password, string email, string address, int phoneNumber)
        {
            var connectionString = "INSERT CONNECTIONSTRING HERE";

            if (true)
            {
                // Hvis der kan skabes connection til serveren brug inputs værdierne til at byg en bruger.

                //User newUser = new User();
                //newUser.userName = userName;
                //newUser.password = passWord;
                //newUser.email = email;
                //newUser.address = address;
                //newUser.phoneNumber = phoneNumber;
            }
            else
            {
                return false;
            }

            // Post newUser to DB.
            // Remember to Hash data.

            return true;
        }

        public async Task CreateNewUser(string username, string password, string confirmPassword, string email, string address, int phoneNumber)
        {
            bool userCreated = false;
            IsBusy = true;

            if (CheckPassword(password, confirmPassword) == true)
            {
                userCreated = PostUser(username, password, email, address, phoneNumber);
            }

            try
            {
                if (userCreated == true)
                {
                    // hvis brugeren blev oprette og posted til serveren så redirect brugeren til mainPage.
                    await Navigation.PushModalAsync(new HomePage());
                }
            }
            catch (Exception e)
            {
                await DisplayAlert("Error", e.Message.ToString(), "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void CreateButton_Clicked(object sender, EventArgs e)
        {
            await CreateNewUser(UserName.Text, PasswordEntry.Text, PasswordRepeatEntry.Text, EmailEntry.Text, Address.Text, int.Parse(PhoneNumberEntry.Text));
        }

        private bool CheckPassword(string password, string confirmPassword)
        {
            if (password == confirmPassword)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void UndoButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}