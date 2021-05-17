using System;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using MobileDev_Projekt.Models;
using MobileDev_Projekt.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileDev_Projekt.Pages
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class NewProgramPage
  {
    private readonly ProgramModel _model;
    private readonly ProgramModel _remoteModel;
    private readonly HomePageModel _homePageModel;
    private bool IsEditMode;

    public NewProgramPage(HomePageModel homePageModel, ProgramModel remoteModel = null, bool isEditMode = false)
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
      _homePageModel = homePageModel;

      BindingContext = _model;
    }

    private void SetEditMode()
    {
      TitleLabel.Text = "Rediger program";
    }

    private async void ExerciseButton_OnClicked(object sender, EventArgs e)
    {
      await Navigation.PushAsync(new NewExercisePage(new ExerciseModel(), _model, _homePageModel));
    }

    private async void StandardExerciseButton_OnClicked(object sender, EventArgs e)
    {
      await Navigation.PushAsync(new SelectStandardExercisePage(_model, _homePageModel));
    }
    
    private void UndoButton_OnClicked(object sender, EventArgs e)
    {
      if (IsEditMode is false)
      {
        _homePageModel?.StandardProgramModels.Remove(_remoteModel);
      }

      Navigation.PopAsync();
    }

    private void ActionButton_OnClicked(object sender, EventArgs e)
    {
      if (string.IsNullOrWhiteSpace(_model.Name))
      {
        DependencyService.Get<IMessage>().LongAlert("Tiitel skal udfyldes");
        return;
      }

      _model.Adapt(_remoteModel);

      if (_remoteModel.IsStandard)
      {
        _homePageModel.StandardProgramModels.Add(_model);
        Navigation.PopAsync();
        return;
      }
      
      Navigation.PushAsync(new PublishProgramPage(_remoteModel));
    }

    private void EditSwipeItem_OnInvoked(object sender, EventArgs e)
    {
      var item = sender as SwipeItem;
      var exerciseModel = item.BindingContext as ExerciseModel;
      exerciseModel.IsStandard = false;
      Navigation.PushAsync(new NewExercisePage(exerciseModel, _model, _homePageModel, true));
    }

    private void DeleteSwipeItem_OnInvoked(object sender, EventArgs e)
    {
      var item = sender as SwipeItem;
      var exerciseModel = item.BindingContext as ExerciseModel;
      _model.ExerciseModels.Remove(exerciseModel);
    }
  }
}