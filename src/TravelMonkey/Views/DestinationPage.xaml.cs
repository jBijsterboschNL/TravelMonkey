using TravelMonkey.Models;
using TravelMonkey.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelMonkey.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DestinationPage : ContentPage
    {
        private readonly DestinationPageViewModel _destinationPageViewModel = new DestinationPageViewModel();

        public DestinationPage()//PictureEntry picture)
        {
            InitializeComponent();

            //_destinationPageViewModel.Init(picture);

            BindingContext = _destinationPageViewModel;
        }

        private void BackButton_Tapped(object sender, System.EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}