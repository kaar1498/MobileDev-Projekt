using System;
using MobileDev_Projekt.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileDev_Projekt
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new LoginPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
