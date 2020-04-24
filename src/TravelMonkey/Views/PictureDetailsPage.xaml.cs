using TravelMonkey.Models;
using TravelMonkey.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelMonkey.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PictureDetailsPage : ContentPage
    {
        private readonly PictureDetailsPageViewModel _pictureDetailsPageViewModel = new PictureDetailsPageViewModel();

        public PictureDetailsPage(PictureEntry picture)
        {
            InitializeComponent();

            _pictureDetailsPageViewModel.Init(picture);

            BindingContext = _pictureDetailsPageViewModel;
        }

        private void BackButton_Tapped(object sender, System.EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}