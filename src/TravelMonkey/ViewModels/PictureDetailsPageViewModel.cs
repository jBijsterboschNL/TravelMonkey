using TravelMonkey.Models;

namespace TravelMonkey.ViewModels
{
    public class PictureDetailsPageViewModel : BaseViewModel
    {
        private PictureEntry _picture;
        public PictureEntry Picture
        {
            get => _picture;
            set => Set(ref _picture, value);
        }

        public PictureDetailsPageViewModel()
        {
        }

        public void Init(PictureEntry picture)
        {
            Picture = picture;
        }
    }
}
