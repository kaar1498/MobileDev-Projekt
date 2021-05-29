using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
  public partial class SelectStandardExercisePage : ContentPage
  {
    private readonly ProgramModel _programModel;
    private readonly SelectStandardExercisePageModel _model;
    private IEnumerable<ExerciseModel> AllExerciseModels { get; set; }
    
    public SelectStandardExercisePage(ProgramModel programModel)
    {
      InitializeComponent();

      _model = new SelectStandardExercisePageModel();
      _programModel = programModel;

      BindingContext = _model;
      
      Task.Run(async () =>
      {
        Busy(true);
        _model.SearchExerciseModels = await App.ExerciseRepository.ListAsync();
        AllExerciseModels = _model.SearchExerciseModels;
        Busy(false);
      });
    }

    private void SearchBox_OnTextChanged(object sender, TextChangedEventArgs e)
    {
      var query = SearchBox.Text.ToLower();
      if (string.IsNullOrEmpty(query))
      {
        _model.SearchExerciseModels = new ObservableCollection<ExerciseModel>(AllExerciseModels);
        return;
      }
      
      _model.SearchExerciseModels = new ObservableCollection<ExerciseModel>(AllExerciseModels.Where(model => model.Name.ToLower().Contains(query)));
    }

    private async void DeleteSwipeItem_OnInvoked(object sender, EventArgs e)
    {
      var item = sender as SwipeItem;
      var exerciseModel = item?.BindingContext as ExerciseModel;

      await PopupNavigation.Instance.PushAsync(new BusyPopup("Deleting exercise"));
      try
      {
        if (!await App.ExerciseRepository.DeleteAsync(exerciseModel?.Id))
        {
          DependencyService.Get<IMessage>().LongAlert("Error deleting exercise");
        }
      }
      catch
      {
        DependencyService.Get<IMessage>().LongAlert("Error deleting exercise");
      }
      finally
      {
        await PopupNavigation.Instance.PopAsync();
      }
    }

    private async void StandardExercise_OnTapped(object sender, EventArgs e)
    {
      var model = (ExerciseModel) ((StackLayout) sender).BindingContext;
      var modelCopy = model.Adapt<ExerciseModel>();
      modelCopy.IsStandard = false;
      _programModel.ExerciseModels.Add(modelCopy);
      await Navigation.PopAsync();
    }

    private void UndoButton_OnClicked(object sender, EventArgs e)
    {
      Navigation.PopAsync();
    }
    
    private void Busy(bool state)
    {
      ActivityIndicator.IsRunning = state;
      ActivityIndicator.IsEnabled = state;
    }
  }
}