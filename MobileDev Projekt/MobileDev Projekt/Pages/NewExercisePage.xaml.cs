using System;
using System.Diagnostics;
using System.IO;
using FFImageLoading;
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
        private readonly ProgramModel _programPageModel;

        public NewExercisePage(ExerciseModel model, ProgramModel programPageModel = null)
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

            _remoteModel.ImageModels = _model.ImageModels;
            _remoteModel.Name = _model.Name;
            _remoteModel.Repetitions = _model.Repetitions;
            _remoteModel.Duration = _model.Duration;
            _remoteModel.RestFrequency = _model.RestFrequency;
            _remoteModel.RestDuration = _model.RestDuration;
            Navigation.PopAsync();
        }
        
        private async void TakePhotoAction()
        {
            var photo = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions { Title = "Tag et billede" });
            if (photo == null) return;
            
            using var stream = await photo.OpenReadAsync();
            _model.ImageModels.Add(new ImageModel
            {
                Bytes = stream.ToByteArray()
            });
        }

        private async void PickPhotoAction()
        {
            var photo = await MediaPicker.PickPhotoAsync(new MediaPickerOptions { Title = "Vælg et billede" });
            if (photo == null) return;
            
            using var stream = await photo.OpenReadAsync();
            _model.ImageModels.Add(new ImageModel
            {
                Bytes = stream.ToByteArray()
            });
        }

        private async void AddPictureButton_Clicked(object sender, EventArgs e)
        {
            var action = await DisplayActionSheet("Hvordan vil du tilføje billedet?", "Cancel", null, "Kamera", "Galleri");
            Debug.WriteLine("Action:" + action);

            switch (action)
            {
                case "Kamera":
                    TakePhotoAction();
                    break;
                case "Galleri":
                    PickPhotoAction();
                    break;
            }
        }
    }
}