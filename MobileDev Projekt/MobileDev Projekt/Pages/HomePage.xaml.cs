using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Mapster.Models;
using MobileDev_Projekt.Models;
using MobileDev_Projekt.Services;
using Plugin.Connectivity;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileDev_Projekt.Pages
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class HomePage : ContentPage
  {
    private readonly HomePageModel _model;
    public IEnumerable<ProgramModel> AllProgramModels { get; set; }

    public HomePage()
    {
      InitializeComponent();
      
      _model = new HomePageModel();
      BindingContext = _model;
      
      Task.Run(async () =>
      {
        Busy(true);
        _model.StandardProgramModels = await App.ProgramRepository.ListAsync();
        AllProgramModels = _model.StandardProgramModels;
        Busy(false);
      });
    }

    private void NewProgramButton_Clicked(object sender, EventArgs e)
    {
      Navigation.PushAsync(new NewProgramPage());
    }

    private async void StandardProgram_OnTapped(object sender, EventArgs e)
    {
      var model = (ProgramModel) ((StackLayout) sender).BindingContext;
      var modelCopy = model.Adapt<ProgramModel>();
      modelCopy.IsStandard = false;
      await Navigation.PushAsync(new NewProgramPage(modelCopy, true));
    }

    private async void DeleteSwipeItem_OnInvoked(object sender, EventArgs e)
    {
      var item = sender as SwipeItem;
      var programModel = item?.BindingContext as ProgramModel;
      
      await PopupNavigation.Instance.PushAsync(new BusyPopup("Deleting program"));
      try
      {
        if (!await App.ProgramRepository.DeleteAsync(programModel?.Id))
        {
          DependencyService.Get<IMessage>().LongAlert("Error deleting program");
        }
      }
      catch
      {
        DependencyService.Get<IMessage>().LongAlert("Error deleting program");
      }
      finally
      {
        await PopupNavigation.Instance.PopAsync();
        
      }
    }

    private void SearchBox_OnTextChanged(object sender, TextChangedEventArgs e)
    {
      var query = SearchBox.Text.ToLower();
      if (string.IsNullOrEmpty(query))
      {
        _model.StandardProgramModels = new ObservableCollection<ProgramModel>(AllProgramModels);
        return;
      }
      
      _model.StandardProgramModels = new ObservableCollection<ProgramModel>(AllProgramModels.Where(model => model.Name.ToLower().Contains(query)));
    }

    private void Busy(bool state)
    {
      ActivityIndicator.IsRunning = state;
      ActivityIndicator.IsEnabled = state;
    }
  }
}