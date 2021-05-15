using System;
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

    public NewProgramPage(ProgramModel model = null)
    {
      _model = model;
      _model ??= new ProgramModel();
      InitializeComponent();
      BindingContext = _model;
    }
    
    private async void ExerciseButton_OnClicked(object sender, EventArgs e)
    {
      var exerciseModel = new ExerciseModel();
      _model.ExerciseModels.Add(exerciseModel);
      await Navigation.PushAsync(new NewExercisePage(exerciseModel, _model));
    }

    private void UndoButton_OnClicked(object sender, EventArgs e)
    {
      Navigation.PopAsync();
    }

    private void ActionButton_OnClicked(object sender, EventArgs e)
    {
      if (string.IsNullOrWhiteSpace(_model.Name))
      {
        DependencyService.Get<IMessage>().LongAlert("Tiitel skal udfyldes");
        return;
      }
      Navigation.PushAsync(new PublishProgramPage());
    }

    private void EditSwipeItem_OnInvoked(object sender, EventArgs e)
    {
      var item = sender as SwipeItem;
      var exerciseModel = item.BindingContext as ExerciseModel;
      _model.ExerciseModels.Remove(exerciseModel);
      Navigation.PushAsync(new NewExercisePage(exerciseModel));
      _model.ExerciseModels.Add(exerciseModel);
    }

    private void DeleteSwipeItem_OnInvoked(object sender, EventArgs e)
    {
      var item = sender as SwipeItem;
      var exerciseModel = item.BindingContext as ExerciseModel;
      _model.ExerciseModels.Remove(exerciseModel);
    }
  }
}