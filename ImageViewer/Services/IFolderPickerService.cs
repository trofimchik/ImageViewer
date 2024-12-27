using System.Threading.Tasks;
using Avalonia.Platform.Storage;

namespace ImageViewer.Services;

public interface IFolderPickerService
{
    Task<IStorageFolder?> SelectFolderAsync();
}