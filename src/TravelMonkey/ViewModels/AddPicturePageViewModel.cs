using Acr.UserDialogs;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using TravelMonkey.Data;
using TravelMonkey.Models;
using TravelMonkey.Services;
using Xamarin.Forms;

namespace TravelMonkey.ViewModels
{
    public class AddPicturePageViewModel : BaseViewModel
    {
        private readonly FaceService _faceService = new FaceService();

        public Destination Destination { get; private set; }

        public bool ShowImagePlaceholder => !ShowPhoto;

        internal void Init(Destination destination)
        {
            Destination = destination;
        }

        public bool ShowPhoto => _photoSource != null;

        private MediaFile _photo;
        private StreamImageSource _photoSource;

        public StreamImageSource PhotoSource
        {
            get => _photoSource;
            set
            {
                if (Set(ref _photoSource, value))
                {
                    RaisePropertyChanged(nameof(ShowPhoto));
                    RaisePropertyChanged(nameof(ShowImagePlaceholder));
                }
            }
        }

        private bool _isPosting;
        public bool IsPosting
        {
            get => _isPosting;
            set => Set(ref _isPosting, value);
        }

        private Color _pictureAccentColor = Color.SteelBlue;
        public Color PictureAccentColor
        {
            get => _pictureAccentColor;
            set => Set(ref _pictureAccentColor, value);
        }

        //private string _pictureDescription;
        //public string PictureDescription
        //{
        //    get => _pictureDescription;
        //    set => Set(ref _pictureDescription, value);
        //}

        public Command TakePhotoCommand { get; }
        //public Command AddPictureCommand { get; }

        public AddPicturePageViewModel()
        {
            TakePhotoCommand = new Command(async () => await TakePhoto());
            //AddPictureCommand = new Command(async () =>
            // {
            //     if (_photoSource == null) return;

            //     byte[] bytes;
            //     using (var stream = await _photoSource.Stream(CancellationToken.None))
            //     {
            //         using (MemoryStream ms = new MemoryStream())
            //         {
            //             stream.CopyTo(ms);
            //             bytes = ms.ToArray();

            //             //await PersistentDataStore.AddPicture(_pictureDescription, bytes);
            //             MessagingCenter.Send(this, Constants.PictureAddedMessage);
            //         }
            //     }
            // });
        }

        private async Task TakePhoto()
        {
            var result = await UserDialogs.Instance.ActionSheetAsync("What do you want to do?",
                "Cancel", null, null, "Take photo", "Choose photo");

            if (result.Equals("Take photo"))
            {
                _photo = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions { PhotoSize = PhotoSize.Small });

                PhotoSource = (StreamImageSource)ImageSource.FromStream(() => _photo.GetStream());
            }
            else if (result.Equals("Choose photo"))
            {
                _photo = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions { PhotoSize = PhotoSize.Small });

                PhotoSource = (StreamImageSource)ImageSource.FromStream(() => _photo.GetStream());
            }
            else
            {
                return;
            }

            if (_photo != null)
                await Post();
        }

        private async Task Post()
        {
            if (_photo == null)
            {
                await UserDialogs.Instance.AlertAsync("Please select an image first", "No image selected");
                return;
            }

            IsPosting = true;

            try
            {
                var pictureStream = _photo.GetStreamWithImageRotatedForExternalStorage();

                var emotion = await _faceService.DetectFaceEmotion(pictureStream);

                Destination.Emotion = emotion.Happiness * 100.00;

                await PersistentDataStore.AddOrUpdateDestination(Destination);

                MessagingCenter.Send(this, Constants.PictureAddedMessage);
            }
            finally
            {
                IsPosting = false;
            }
        }
    }
}