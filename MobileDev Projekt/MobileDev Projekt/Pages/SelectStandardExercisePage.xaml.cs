using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Mapster;
using MobileDev_Projekt.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileDev_Projekt.Pages
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class SelectStandardExercisePage : ContentPage
  {
    private readonly ProgramModel _model;
    private readonly HomePageModel _homePageModel;
    private IEnumerable<ExerciseModel> AllExerciseModels { get; set; }
    public SelectStandardExercisePage(ProgramModel model, HomePageModel homePageModel)
    {
      InitializeComponent();
      _model = model;
      _homePageModel = homePageModel;
      AllExerciseModels = _homePageModel.StandardExerciseModels;
      BindingContext = _homePageModel;
    }

    private void SearchBox_OnTextChanged(object sender, TextChangedEventArgs e)
    {
      var query = SearchBox.Text.ToLower();
      if (string.IsNullOrEmpty(query))
      {
        _homePageModel.StandardExerciseModels = new ObservableCollection<ExerciseModel>(AllExerciseModels);
        return;
      }
      
      _homePageModel.StandardExerciseModels = new ObservableCollection<ExerciseModel>(AllExerciseModels.Where(model => model.Name.ToLower().Contains(query)));
    }

    private void DeleteSwipeItem_OnInvoked(object sender, EventArgs e)
    {
      var item = sender as SwipeItem;
      var exerciseModel = item.BindingContext as ExerciseModel;
      _homePageModel.StandardExerciseModels.Remove(exerciseModel);
    }

    private async void StandardExercise_OnTapped(object sender, EventArgs e)
    {
      var model = (ExerciseModel) ((StackLayout) sender).BindingContext;
      var modelCopy = model.Adapt<ExerciseModel>();
      modelCopy.IsStandard = false;
      _model.ExerciseModels.Add(modelCopy);
      await Navigation.PopAsync();
    }

    private void UndoButton_OnClicked(object sender, EventArgs e)
    {
      Navigation.PopAsync();
    }
  }
}