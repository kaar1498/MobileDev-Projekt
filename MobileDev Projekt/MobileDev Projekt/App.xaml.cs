using MobileDev_Projekt.Pages;
using MobileDev_Projekt.Services;
using Xamarin.Forms;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;

[assembly: ExportFont("OpenSans-Bold.ttf", Alias = "TitleFont")]
[assembly: ExportFont("OpenSans-Light.ttf", Alias = "TextLightFont")]
[assembly: ExportFont("OpenSans-Regular.ttf", Alias = "TextFont")]
namespace MobileDev_Projekt
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Sharpnado.HorizontalListView.Initializer.Initialize(true, false);

            CrossConnectivity.Current.ConnectivityChanged += CurrentOnConnectivityChanged;

            MainPage = new NavigationPage(new HomePage());

            MainPage.SetValue(NavigationPage.BarBackgroundColorProperty, Color.Black);
            MainPage.SetValue(NavigationPage.BarTextColorProperty, Color.White);
        }

        private void CurrentOnConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            DependencyService.Get<IMessage>()
                .LongAlert(CrossConnectivity.Current.IsConnected ? "Connection Established" : "Connection Lost");
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
