using System;
using System.Diagnostics;
using FFImageLoading;
using FFImageLoading.Svg.Forms;
using Mapster;
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
    private readonly HomePageModel _homePageModel;
    private bool IsEditMode;

    public NewExercisePage(ExerciseModel remoteModel, ProgramModel programPageModel, HomePageModel homePageModel , bool isEditMode = false)
    {
      InitializeComponent();
      
      IsEditMode = isEditMode;
      _remoteModel = remoteModel;
      
      if (IsEditMode is true)
      {
        SetEditMode();
        _model = _remoteModel?.Adapt<ExerciseModel>();
      }

      _model ??= new ExerciseModel();
      _programPageModel = programPageModel;
      _homePageModel = homePageModel;

      BindingContext = _model;
    }

    private void SetEditMode()
    {
      TitleLabel.Text = "Rediger øvelse";
      CreateButton.Text = "Gem";
    }

    private void UndoButton_OnClicked(object sender, EventArgs e)
    {
      if (IsEditMode is false)
      {
        _programPageModel?.ExerciseModels.Remove(_remoteModel);
      }
      
      Navigation.PopAsync();
    }

    private void CreateButton_OnClicked(object sender, EventArgs e)
    {
      if (string.IsNullOrWhiteSpace(_model.Name))
      {
        DependencyService.Get<IMessage>().LongAlert("Øvelse navn skal være udfyldt");
        return;
      }

      _model.Adapt(_remoteModel);

      if (_remoteModel.IsStandard)
      {
        _homePageModel.StandardExerciseModels.Add(_remoteModel);
      }

      if (!IsEditMode)
      {
        _programPageModel.ExerciseModels.Add(_remoteModel);
      }
      
      Navigation.PopAsync();
    }

    private void DeleteImage_OnTapped(object sender, EventArgs e)
    {
      var model = (ImageModel) ((SvgCachedImage) sender).BindingContext;
      _model.ImageModels.Remove(model);
    }
    
    private async void TakePhotoAction()
    {
      var photo = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions {Title = "Tag et billede"});
      if (photo == null) return;

      using var stream = await photo.OpenReadAsync();
      _model.ImageModels.Add(new ImageModel
      {
        Bytes = stream.ToByteArray()
      });
    }

    private async void PickPhotoAction()
    {
      var photo = await MediaPicker.PickPhotoAsync(new MediaPickerOptions {Title = "Vælg et billede"});
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