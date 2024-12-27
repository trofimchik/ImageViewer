using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Platform.Storage;

namespace ImageViewer.Services;

public class FolderPickerService : IFolderPickerService
{
    private readonly Window _target;

    public FolderPickerService(Window target)
    {
        _target = target;
    }
    
    public async Task<IStorageFolder?> SelectFolderAsync()
    {
        var folders = await _target.StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions
        {
            Title = "Select Folder",
            AllowMultiple = false
        });

        return folders.Count >= 1 ? folders[0] : null;
    }
}