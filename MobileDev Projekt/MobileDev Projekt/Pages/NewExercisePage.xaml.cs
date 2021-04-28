using System;
using System.Diagnostics;
using MobileDev_Projekt.Models;
using MobileDev_Projekt.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileDev_Projekt.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewExercisePage
    {
        private readonly ExerciseModel _model;
        private readonly ExerciseModel _remoteModel;
        private readonly NewProgramPageModel _programPageModel;

        public NewExercisePage(ExerciseModel model, NewProgramPageModel programPageModel = null)
        {
            _model = model ??= new ExerciseModel();
            _remoteModel = model;
            _programPageModel = programPageModel;
            InitializeComponent();
            BindingContext = _model;
        }

        private void UndoButton_OnClicked(object sender, EventArgs e)
        {
            _programPageModel?.ExerciseModels.Remove(_remoteModel);
            Navigation.PopAsync();
        }

        private void CreateButton_OnClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_model.Name))
            {
                DependencyService.Get<IMessage>().LongAlert("Øvelse navn skal være udfyldt");
                return;
            }

            _remoteModel.Name = _model.Name;
            _remoteModel.Image = _model.Image;
            Navigation.PopAsync();
        }
        private async void TakePhotoAction()
        {
            var photo = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions { Title = "Tag et billede" });
            if (photo == null) return;
            var stream = await photo.OpenReadAsync();
            var imageSource = ImageSource.FromStream(() => stream);
        }

        private async void PickPhotoAction()
        {
            var photo = await MediaPicker.PickPhotoAsync(new MediaPickerOptions { Title = "Vælg et billede" });
            if (photo == null) return;
            var stream = await photo.OpenReadAsync();
            PhotoImage.Source = ImageSource.FromStream(() => stream); //PhotoImage.Source
        }

        private async void AddPictureButton_Clicked(object sender, EventArgs e)
        {
            string action = await DisplayActionSheet("Hvordan vil du tilføje billedet?", "Cancel", null, "Kamera", "Galleri");
            Debug.WriteLine("Action:" + action);

            if (action == "Kamera")
            {
                TakePhotoAction();
            }
            else if (action == "Galleri")
            {
                PickPhotoAction();
            }
        }
    }
}