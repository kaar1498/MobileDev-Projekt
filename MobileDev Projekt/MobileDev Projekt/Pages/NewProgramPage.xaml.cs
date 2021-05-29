using System;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using MobileDev_Projekt.Models;
using MobileDev_Projekt.Services;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileDev_Projekt.Pages
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class NewProgramPage
  {
    private readonly ProgramModel _model;
    private readonly ProgramModel _remoteModel;
    private bool IsEditMode;

    public NewProgramPage(ProgramModel remoteModel = null, bool isEditMode = false)
    {
      InitializeComponent();

      IsEditMode = isEditMode;
      _remoteModel = remoteModel ??= new ProgramModel();

      if (IsEditMode is true)
      {
        SetEditMode();
        _model = _remoteModel?.Adapt<ProgramModel>();
      }

      _model ??= new ProgramModel();

      BindingContext = _model;
    }

    private void SetEditMode()
    {
      TitleLabel.Text = "Rediger program";
    }

    private async void ExerciseButton_OnClicked(object sender, EventArgs e)
    {
      await Navigation.PushAsync(new NewExercisePage(new ExerciseModel(), _model));
    }

    private async void StandardExerciseButton_OnClicked(object sender, EventArgs e)
    {
      await Navigation.PushAsync(new SelectStandardExercisePage(_model));
    }
    
    private void UndoButton_OnClicked(object sender, EventArgs e)
    {
      Navigation.PopAsync();
    }

    private async void ActionButton_OnClicked(object sender, EventArgs e)
    {
      if (string.IsNullOrWhiteSpace(_model.Name))
      {
        DependencyService.Get<IMessage>().LongAlert("Titel skal udfyldes");
        return;
      }

      _model.Adapt(_remoteModel);

      if (_remoteModel.IsStandard)
      {
        await PopupNavigation.Instance.PushAsync(new BusyPopup("Creating program"));
        try
        {
          if (!await App.ProgramRepository.AddAsync(_model))
          {
            DependencyService.Get<IMessage>().LongAlert("Error adding program");
            return;
          }
        }
        catch
        {
          DependencyService.Get<IMessage>().LongAlert("Error adding program");
        }
        finally
        {
          await PopupNavigation.Instance.PopAsync();
        }
        await Navigation.PopAsync();
        return;
      }
      
      await Navigation.PushAsync(new PublishProgramPage(_remoteModel));
    }

    private void EditSwipeItem_OnInvoked(object sender, EventArgs e)
    {
      var item = sender as SwipeItem;
      var exerciseModel = item.BindingContext as ExerciseModel;
      exerciseModel.IsStandard = false;
      Navigation.PushAsync(new NewExercisePage(exerciseModel, _model, true));
    }

    private void DeleteSwipeItem_OnInvoked(object sender, EventArgs e)
    {
      var item = sender as SwipeItem;
      var exerciseModel = item.BindingContext as ExerciseModel;
      _model.ExerciseModels.Remove(exerciseModel);
    }
  }
}