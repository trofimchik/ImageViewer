using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Platform.Storage;

namespace ImageViewer.Services;

public class ImagesPickerService : IImagesPickerService
{
    private readonly Window _target;

    public ImagesPickerService(Window target)
    {
        _target = target;
    }

    public async Task<IReadOnlyList<IStorageFile>?> SelectImagesAsync()
    {
        var files = await _target.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Select Images",
            AllowMultiple = true,
            FileTypeFilter = [FilePickerFileTypes.ImageAll]
        });

        return files.Count >= 1 ? files : null;
    }
}