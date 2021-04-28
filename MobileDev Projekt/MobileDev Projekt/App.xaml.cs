using MobileDev_Projekt.Pages;
using Xamarin.Forms;

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

            MainPage = new NavigationPage(new LoginPage());
            MainPage.SetValue(NavigationPage.BarBackgroundColorProperty, Color.Black);
            MainPage.SetValue(NavigationPage.BarTextColorProperty, Color.White);
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
