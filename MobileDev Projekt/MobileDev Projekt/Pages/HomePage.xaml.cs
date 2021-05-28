using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using MobileDev_Projekt.Models;
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
        _model.StandardProgramModels = await App.ProgramRepository.ListAsync();
        AllProgramModels = _model.StandardProgramModels;
      });
    }

    private void NewProgramButton_Clicked(object sender, EventArgs e)
    {
      Navigation.PushAsync(new NewProgramPage(_model));
    }

    private void StandardProgram_OnTapped(object sender, EventArgs e)
    {
      var model = (ProgramModel) ((StackLayout) sender).BindingContext;

      var modelCopy = model.Adapt<ProgramModel>();
      modelCopy.IsStandard = false;

      Navigation.PushAsync(new NewProgramPage(_model, modelCopy, true));
    }

    private void DeleteSwipeItem_OnInvoked(object sender, EventArgs e)
    {
      var item = sender as SwipeItem;
      var programModel = item.BindingContext as ProgramModel;
      _model.StandardProgramModels.Remove(programModel);
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
  }
}