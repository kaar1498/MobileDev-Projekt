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
    private NewProgramPageModel Model { get; } = new NewProgramPageModel();
    public NewProgramPage()
    {
      InitializeComponent();
      BindingContext = Model;
    }
    
    private void ExerciseButton_OnClicked(object sender, EventArgs e)
    {
      var exerciseModel = new ExerciseModel();
      Model.ExerciseModels.Add(exerciseModel);
      Navigation.PushAsync(new NewExercisePage(exerciseModel, Model));
    }

    private void UndoButton_OnClicked(object sender, EventArgs e)
    {
      Navigation.PopAsync();
    }

    private void ActionButton_OnClicked(object sender, EventArgs e)
    {
      if (string.IsNullOrWhiteSpace(Model.InformationModel.Value))
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
      Model.ExerciseModels.Remove(exerciseModel);
      Navigation.PushAsync(new NewExercisePage(exerciseModel));
      Model.ExerciseModels.Add(exerciseModel);
    }

    private void DeleteSwipeItem_OnInvoked(object sender, EventArgs e)
    {
      var item = sender as SwipeItem;
      var exerciseModel = item.BindingContext as ExerciseModel;
      Model.ExerciseModels.Remove(exerciseModel);
    }
  }
}