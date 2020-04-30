using System;
using TravelMonkey.Models;
using TravelMonkey.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelMonkey.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DestinationPage : ContentPage
    {
        private readonly DestinationPageViewModel _destinationPageViewModel = new DestinationPageViewModel();

        public DestinationPage(Destination destination)
        {
            InitializeComponent();

            _destinationPageViewModel.Init(destination);

            BindingContext = _destinationPageViewModel;

            MessagingCenter.Subscribe<AddPicturePageViewModel>(this, Constants.PictureAddedMessage, (vm) =>
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    _destinationPageViewModel.Destination = vm.Destination;
                });
            });
        }

        private void BackButton_Tapped(object sender, System.EventArgs e)
        {
            Navigation.PopAsync();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _destinationPageViewModel.StartSlideShow();
        }

        protected override void OnDisappearing()
        {
            _destinationPageViewModel.StopSlideShow();

            base.OnDisappearing();
        }

        private void HappinessCheck_Tapped(object sender, System.EventArgs e)
        {
            Navigation.PushModalAsync(new AddPicturePage(_destinationPageViewModel.Destination));
        }
    }
}