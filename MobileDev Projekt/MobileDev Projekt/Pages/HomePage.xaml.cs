using System;
using System.Threading.Tasks;
using MobileDev_Projekt.Models;
using MobileDev_Projekt.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileDev_Projekt.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        private readonly HomePageModel _model;
        public HomePage()
        {
            InitializeComponent();
            _model = new HomePageModel();
            BindingContext = _model;
            var apiService = new ApiService();
            Task.Run(async () =>
            {
                _model.ProgramModels = await apiService.GetPrograms();
            });
        }

        private void NewProgramButton_Clicked(object sender, EventArgs e)
        {
           Navigation.PushAsync(new NewProgramPage());
        }

        private void StandardProgramButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new StandardPrograms());
        }
    }
}