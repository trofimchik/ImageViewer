using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ImageViewer.Helpers;
using ImageViewer.Models;
using ImageViewer.Services;
using MediatR;
using Tmds.DBus.Protocol;
using static System.Net.WebRequestMethods;

namespace ImageViewer.ViewModels
{
    public partial class ImagesViewModel : ViewModelBase
    {
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(
            nameof(PickImagesCommand),
            nameof(SerializeImagesCommand),
            nameof(DeserializeImagesCommand))]
        private bool _inProcess;

        [ObservableProperty]
        private string _message;

        private readonly IPickerService _pickerService;
        private readonly IImagesService _imagesService;

        public ImagesViewModel(IPickerService pickerService, IImagesService service)
        {
            _pickerService = pickerService;
            _imagesService = service;
        }

        public ObservableCollection<ImageModel> Images { get; } = [];
        private bool CanExecute => !InProcess;

        [RelayCommand(CanExecute = nameof(CanExecute))]
        public async Task PickImages()
        {
            Message = "Выполнение...";
            InProcess = true;

            var imagesFiles = await _pickerService.PickImagesAsync();
            if (imagesFiles == null)
            {
                Message = "Отменено";
                return;
            }

            List<ImageModel> images = [];
            try
            {
                images = await imagesFiles.MapToModelAsync();
            }
            catch(AggregateException exc)
            {
                //todo: Логи
                Message = "Ошибка мапинга";
                InProcess = false;
            }
            catch (Exception exc)
            {
                //todo: Логи
                Message = "Ошибка мапинга";
                InProcess = false;
            }

            Images.Clear();
            foreach (var image in images) Images.Add(image);

            InProcess = false;
            Message = "Выполнено";
        }

        [RelayCommand(CanExecute = nameof(CanExecute))]
        public async Task SerializeImages()
        {
            Message = "Выполнение...";
            InProcess = true;

            var file = await _pickerService.CreateJsonAsync();
            if (file == null)
            {
                Message = "Отменено";
                return;
            }

            try
            {
                await _imagesService.SerializeAsync(file.Path.AbsolutePath, Images);
            }
            catch (AggregateException exc)
            {
                //todo: Логи
                Message = "Ошибка сериализации";
                InProcess = false;
            }
            catch (Exception exc)
            {
                //todo: Логи
                Message = "Ошибка сериализации";
                InProcess = false;
            }

            InProcess = false;
            Message = "Выполнено";
        }

        [RelayCommand(CanExecute = nameof(CanExecute))]
        public async Task DeserializeImages()
        {
            Message = "Выполнение...";
            InProcess = true;

            var json = await _pickerService.PickJsonAsync();
            if (json == null)
            {
                Message = "Отменено";
                return;
            }

            List<ImageModel>? images = [];
            try
            {
                images = await _imagesService.DeserializeAsync(json.Path.AbsolutePath);
            }
            catch (AggregateException exc)
            {
                //todo: Логи
                Message = "Ошибка десериализации";
                InProcess = false;
            }
            catch (Exception exc)
            {
                //todo: Логи
                Message = "Ошибка десериализации";
                InProcess = false;
            }

            if (images is null) return;

            Images.Clear();
            foreach (var image in images) Images.Add(image);

            InProcess = false;
            Message = "Выполнено";
        }
    }
}
