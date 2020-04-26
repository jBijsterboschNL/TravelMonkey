using System.Linq;
using System.Timers;
using TravelMonkey.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace TravelMonkey.ViewModels
{
    public class DestinationPageViewModel : BaseViewModel
    {
        private readonly Timer _slideShowTimer = new Timer(5000);

        private Destination _destination;
        public Destination Destination
        {
            get => _destination;
            set
            {
                Set(ref _destination, value);
                CurrentImage = Destination.Images.FirstOrDefault();
            }
        }

        private DestinationImage _currentImage;
        public DestinationImage CurrentImage
        {
            get => _currentImage;
            set => Set(ref _currentImage, value);
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

        public DestinationPageViewModel()
        {
            _slideShowTimer.Elapsed += SlideShowTimer_Elapsed;
        }

        private void SlideShowTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            var currentIdx = Destination.Images.IndexOf(CurrentImage);

            if (currentIdx == Destination.Images.Count() - 1)
            {
                CurrentImage = Destination.Images.ElementAt(0);
            }
            else
            {
                CurrentImage = Destination.Images.ElementAt(currentIdx + 1);
            }
        }

        public void Init(Destination destination)
        {
            Destination = destination;
        }

        public void StartSlideShow()
        {
            _slideShowTimer.Start();

        }

        public void StopSlideShow()
        {
            _slideShowTimer.Stop();
        }
    }
}
