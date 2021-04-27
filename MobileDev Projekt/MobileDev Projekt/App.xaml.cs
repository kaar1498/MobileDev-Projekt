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

            //LoginPage

            MainPage = new NavigationPage(new PublishProgramPage());
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
