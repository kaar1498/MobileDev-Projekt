using System;
using Xamarin.Forms.Xaml;

namespace MobileDev_Projekt.Pages
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class NewProgramPage
  {
    public NewProgramPage()
    {
      InitializeComponent();
    }
    
    private void ExerciseButton_OnClicked(object sender, EventArgs e)
    {
      Navigation.PushAsync(new NewExercisePage());
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
      
    }

    private void DeleteSwipeItem_OnInvoked(object sender, EventArgs e)
    {
      throw new NotImplementedException();
    }
  }
}