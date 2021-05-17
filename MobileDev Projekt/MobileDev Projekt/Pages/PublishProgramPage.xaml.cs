using System;
using MobileDev_Projekt.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileDev_Projekt.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PublishProgramPage : ContentPage
    {
        public string Email { get; set; }
        public ProgramModel ProgramModel { get; set; }
        
        public PublishProgramPage(ProgramModel programModel)
        {
            InitializeComponent();
            ProgramModel = programModel;
            BindingContext = this;
        }
        
        private void UndoButton_OnClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void SendButton_OnClicked(object sender, EventArgs e)
        {
            //TODO Send Email
            Navigation.PopToRootAsync();
        }
    }
}