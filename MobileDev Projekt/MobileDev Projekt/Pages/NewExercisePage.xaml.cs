using System;
using MobileDev_Projekt.Models;
using Xamarin.Forms.Xaml;

namespace MobileDev_Projekt.Pages
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class NewExercisePage
  {
    private ExerciseModel _model;

    public NewExercisePage(ExerciseModel model)
    {
      _model = model;
      InitializeComponent();
      BindingContext = _model;
    }

    private void UndoButton_OnClicked(object sender, EventArgs e)
    {
      Navigation.PopAsync();
    }

    private void CreateButton_OnClicked(object sender, EventArgs e)
    {
      Navigation.PopAsync();
    }
  }
}