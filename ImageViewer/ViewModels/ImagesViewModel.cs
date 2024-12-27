using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using ImageViewer.Models;
using ImageViewer.Services;

namespace ImageViewer.ViewModels
{
    public partial class ImagesViewModel : ViewModelBase
    {
        [ObservableProperty]
        private string _path;
        
        private readonly IImagesService _imagesService;
        private readonly IFolderPickerService _folderPickerService;
        private readonly IImagesPickerService _imagesPickerService;
        public ImagesViewModel(IImagesService service, IFolderPickerService folderPickerService, IImagesPickerService imagesPickerService)
        {
            _imagesService = service;
            _folderPickerService = folderPickerService;
            _imagesPickerService = imagesPickerService;
        }

        public ObservableCollection<ImageModel> Images { get; } = [];

        private async void SelectImages(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var files = await _imagesPickerService.SelectImagesAsync();
            if (files is null) return;

            // todo: Read images
        }
    }
}
