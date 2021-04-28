using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MobileDev_Projekt.Models;
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
            Task.Run(async () =>
            {
                await Task.Delay(3000);
                _model.Exercises = new ObservableCollection<string>
                {
                    "Range of Motion",
                    "Stretching",
                    "Aerobic",
                    "Balancing",
                    "Aquatic",
                    "Strengthening",
                    "Breathing",
                    "Relaxation"
                };
            }); //TODO Load from API
        }

        private void NewProgramButton_Clicked(object sender, EventArgs e)
        {
           Navigation.PushAsync(new NewProgramPage());
        }

        private void StndardProgramButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new StandardPrograms());
        }
    }
}