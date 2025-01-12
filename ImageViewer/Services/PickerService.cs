using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using ImageViewer.Models;

namespace ImageViewer.Services;

public class PickerService : IPickerService
{

    private readonly static FilePickerFileType _json = new("json")
    {
        Patterns = ["*.json"]
    };
    private readonly static FilePickerOpenOptions _jsonPickerOptions = new()
    {
        Title = "Select Json",
        AllowMultiple = false,
        FileTypeFilter = [_json]
    };
    private readonly static FilePickerSaveOptions _saveFilePickerOptions = new()
    {
        Title = "Save Json",
        SuggestedFileName = "images.json",
        FileTypeChoices = [ _json ]
    };
    private readonly static FilePickerOpenOptions _imagesPickerOptions = new()
    {
        Title = "Select Images",
        AllowMultiple = true,
        FileTypeFilter = [FilePickerFileTypes.ImageAll]
    };

    private readonly Window _target;

    public PickerService(Window target)
    {
        _target = target;
    }

    public async Task<IStorageFile?> PickJsonAsync()
    {
        var files = await _target.StorageProvider.OpenFilePickerAsync(_jsonPickerOptions);

        return files.Count == 1 ? files[0] : null;
    }

    public async Task<IStorageFile?> CreateJsonAsync()
    {
        var file = await _target.StorageProvider.SaveFilePickerAsync(_saveFilePickerOptions);

        return file;
    }

    public async Task<IReadOnlyList<IStorageFile>?> PickImagesAsync()
    {
        var files = await _target.StorageProvider.OpenFilePickerAsync(_imagesPickerOptions);

        return files.Count >= 1 ? files : null;
    }
}