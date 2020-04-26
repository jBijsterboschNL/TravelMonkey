using TravelMonkey.Models;

namespace TravelMonkey.ViewModels
{
    public class DestinationPageViewModel : BaseViewModel
    {
        private PictureEntry _picture;
        public PictureEntry Picture
        {
            get => _picture;
            set => Set(ref _picture, value);
        }

        public DestinationPageViewModel()
        {
        }

        public void Init(PictureEntry picture)
        {
            Picture = picture;
        }
    }
}
