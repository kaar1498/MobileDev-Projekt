using Xamarin.Forms.Xaml;

namespace MobileDev_Projekt.Pages
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class BusyPopup
  {
    public BusyPopup(string text)
    {
      InitializeComponent();
      BusyReason.Text = text;
    }
    
    protected override bool OnBackButtonPressed()
    {
      return true;
    }
    protected override bool OnBackgroundClicked()
    {
      return false;
    }
  }
}