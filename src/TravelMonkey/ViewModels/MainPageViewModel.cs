using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TravelMonkey.Data;
using TravelMonkey.Models;
using TravelMonkey.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TravelMonkey.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private readonly BingSearchService _bingSearchService = new BingSearchService();
        private readonly ComputerVisionService _computerVisionService = new ComputerVisionService();

        private ObservableCollection<Destination> _destination = new ObservableCollection<Destination>();
        public ObservableCollection<Destination> Destinations
        {
            get => _destination;
            set => Set(ref _destination, value);
        }

        private bool _isProcessing = false;
        public bool IsProcessing
        {
            get => _isProcessing;
            set => Set(ref _isProcessing, value);
        }

        public Command<string> OpenUrlCommand { get; } = new Command<string>(async (url) =>
        {
            if (!string.IsNullOrWhiteSpace(url))
            {
                await Browser.OpenAsync(url, options: new BrowserLaunchOptions
                {
                    Flags = BrowserLaunchFlags.PresentAsFormSheet,
                    PreferredToolbarColor = Color.SteelBlue,
                    PreferredControlColor = Color.White
                });
            }
        });

        public MainPageViewModel()
        {
            //MessagingCenter.Subscribe<AddPicturePageViewModel>(this, Constants.PictureAddedMessage, async (vm) => await RefreshDestinations());
        }

        public async Task AddDestination(string destinationName)
        {
            IsProcessing = true;

            try
            {
                var images = await _bingSearchService.GetDestinationImages(destinationName);

                foreach(var image in images)
                {
                    var result = await _computerVisionService.AnalyzePicture(image.ImageUrl);
                    if (result.Succeeded)
                    {
                        image.Description = result.Description;
                        if (!string.IsNullOrWhiteSpace(result.LandmarkDescription))
                        {
                            image.Description += $". {result.LandmarkDescription}";
                        }
                    }
                }

                var destination = new Destination(destinationName, images);
                await PersistentDataStore.AddDestination(destination);

                var destinations = await PersistentDataStore.GetDestinations();
                Destinations = new ObservableCollection<Destination>(destinations);
            }
            catch (Exception ex)
            {
            }
            finally
            {
                IsProcessing = false;
            }
        }
    }
}