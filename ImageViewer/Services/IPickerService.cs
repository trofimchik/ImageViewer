using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Platform.Storage;

namespace ImageViewer.Services;

public interface IPickerService
{
    Task<IStorageFile?> PickJsonAsync();
    Task<IStorageFile?> CreateJsonAsync();
    Task<IReadOnlyList<IStorageFile>?> PickImagesAsync();
}