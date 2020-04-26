using System;
using System.ComponentModel;
using TravelMonkey.Models;
using TravelMonkey.ViewModels;
using Xamarin.Forms;

namespace TravelMonkey.Views
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private readonly MainPageViewModel _mainPageViewModel = new MainPageViewModel();

        public MainPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            InitializeComponent();

            BindingContext = _mainPageViewModel;
        }

        private async void AddNewDestination_Tapped(object sender, EventArgs e)
        {
            var destination = await DisplayPromptAsync("Enter a destination", "And start your journey here to find out if this place makes you happy.");

            if (!string.IsNullOrWhiteSpace(destination))
            {
                await _mainPageViewModel.AddDestination(destination);
            }
        }

        private async void Destination_Tapped(object sender, EventArgs e)
        {
            if ((sender as BindableObject).BindingContext is Destination destination)
            {
                await Navigation.PushAsync(new DestinationPage(destination));
            }
        }

        private async void Entry_Completed(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TranslateTextEntry.Text))
            {
                await DisplayAlert("No text entered", "You didn't enter any text!", "OK");
                return;
            }

            await Navigation.PushModalAsync(new TranslationResultPage(TranslateTextEntry.Text));
            TranslateTextEntry.Text = "";
        }
    }
}