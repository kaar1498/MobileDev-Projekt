using System;
using Xamarin.Forms.Xaml;

namespace MobileDev_Projekt.Pages
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class NewExercisePage
  {
    public NewExercisePage()
    {
      InitializeComponent();
    }

    private void UndoButton_OnClicked(object sender, EventArgs e)
    {
      Navigation.PopAsync();
    }
  }
}