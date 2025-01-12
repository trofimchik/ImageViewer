using System.Threading;

namespace ImageViewer.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel(ImagesViewModel imagesVM)
        {
            ImagesVM = imagesVM;
        }
        public ImagesViewModel ImagesVM { get; }
    }
}
