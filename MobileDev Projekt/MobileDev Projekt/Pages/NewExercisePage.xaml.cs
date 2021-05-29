using System;
using System.Diagnostics;
using FFImageLoading;
using FFImageLoading.Svg.Forms;
using Mapster;
using MobileDev_Projekt.Models;
using MobileDev_Projekt.Services;
using Rg.Plugins.Popup.Services;
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
    private bool IsEditMode;

    public NewExercisePage(ExerciseModel remoteModel, ProgramModel programPageModel, bool isEditMode = false)
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

    private async void CreateButton_OnClicked(object sender, EventArgs e)
    {
      if (string.IsNullOrWhiteSpace(_model.Name))
      {
        DependencyService.Get<IMessage>().LongAlert("Øvelse navn skal være udfyldt");
        return;
      }

      _model.Adapt(_remoteModel);

      if (_remoteModel.IsStandard)
      {
        await PopupNavigation.Instance.PushAsync(new BusyPopup("Creating exercise"));
        try
        {
          if (!await App.ExerciseRepository.AddAsync(_remoteModel))
          {
            DependencyService.Get<IMessage>().LongAlert("Error adding exercise");
            return;
          }

          if (IsEditMode)
          {
            _programPageModel.ExerciseModels.Add(_remoteModel.Adapt<ExerciseModel>());
          }
        }
        catch (Exception exception)
        {
          DependencyService.Get<IMessage>().LongAlert("Error adding exercise");
        }
        finally
        {
          await PopupNavigation.Instance.PopAsync();
        }
      }

      if (!IsEditMode)
      {
        _programPageModel.ExerciseModels.Add(_remoteModel);
      }

      await Navigation.PopAsync();
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