using System;
using MobileDev_Projekt.Models;
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
      var exerciseModel = new ExerciseModel()
      {
        Image = "https://picsum.photos/50"
      };
      Model.ExerciseModels.Add(exerciseModel);
      Navigation.PushAsync(new NewExercisePage(exerciseModel));
    }

    private void UndoButton_OnClicked(object sender, EventArgs e)
    {
      //Navigation.PopAsync();
    }

    private void ActionButton_OnClicked(object sender, EventArgs e)
    {
      //Navigation.PushAsync(new PublishPage());
    }

    private void EditSwipeItem_OnInvoked(object sender, EventArgs e)
    {
      var item = sender as SwipeItem;
      var model = item.BindingContext as ExerciseModel;
      Navigation.PushAsync(new NewExercisePage(model));
    }

    private void DeleteSwipeItem_OnInvoked(object sender, EventArgs e)
    {
      var item = sender as SwipeItem;
      var model = item.BindingContext as ExerciseModel;
      Model.ExerciseModels.Remove(model);
    }
  }
}