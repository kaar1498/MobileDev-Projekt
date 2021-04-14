using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileDev_Projekt
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new Pages.Login();
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
