using System.Collections.Generic;
using System.Threading.Tasks;
using ImageViewer.Models;

namespace ImageViewer.Services
{
    public interface IImagesService
    {
        Task SerializeAsync(string path, IEnumerable<ImageModel> images);

        Task<List<ImageModel>?> DeserializeAsync(string json);
    }
}
