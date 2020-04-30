using System;
using TravelMonkey.Models;
using TravelMonkey.ViewModels;
using Xamarin.Forms;

namespace TravelMonkey.Views
{
    public partial class AddPicturePage : ContentPage
    {
        private readonly AddPicturePageViewModel _addPicturePageViewModel = new AddPicturePageViewModel();
        
        public AddPicturePage(Destination destination)
        {
            InitializeComponent();

            _addPicturePageViewModel.Init(destination);
            BindingContext = _addPicturePageViewModel;

            MessagingCenter.Subscribe<AddPicturePageViewModel>(this, Constants.PictureAddedMessage, async (vm) =>
            {
                await DisplayAlert("Happiness result", $"Your happiness for {vm.Destination.Name} is {vm.Destination.Emotion}%.", "Ok");
                await Navigation.PopModalAsync(true);
            });

            MessagingCenter.Subscribe<AddPicturePageViewModel>(this, Constants.PictureFailedMessage, async (vm) => await DisplayAlert("Uh-oh!", "Can you hand me my glasses? Something went wrong while analyzing this image", "OK"));
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                Navigation.PopModalAsync();
            }
            catch (Exception ex)
            {
            }
        }
    }
}