using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Platform.Storage;

namespace ImageViewer.Services;

public interface IImagesPickerService
{
    Task<IReadOnlyList<IStorageFile>?> SelectImagesAsync();
}