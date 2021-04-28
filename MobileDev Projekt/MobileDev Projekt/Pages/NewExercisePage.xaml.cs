using System;
using MobileDev_Projekt.Models;
using MobileDev_Projekt.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileDev_Projekt.Pages
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class NewExercisePage
  {
    private readonly ExerciseModel _model;
    private readonly ExerciseModel _remoteModel;
    private readonly NewProgramPageModel _programPageModel;

    public NewExercisePage(ExerciseModel model, NewProgramPageModel programPageModel = null)
    {
      _model = model ??= new ExerciseModel();
      _remoteModel = model;
      _programPageModel = programPageModel;
      InitializeComponent();
      BindingContext = _model;
    }

    private void UndoButton_OnClicked(object sender, EventArgs e)
    {
      _programPageModel?.ExerciseModels.Remove(_remoteModel);
      Navigation.PopAsync();
    }

    private void CreateButton_OnClicked(object sender, EventArgs e)
    {
      if (string.IsNullOrWhiteSpace(_model.Name))
      {
        DependencyService.Get<IMessage>().LongAlert("Øvelse navn skal være udfyldt");
        return;
      }
      
      _remoteModel.Name = _model.Name;
      _remoteModel.Image = _model.Image;
      Navigation.PopAsync();
    }
  }
}