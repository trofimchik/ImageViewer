using System.Threading.Tasks;
using ImageViewer.Models;

namespace ImageViewer.Services
{
    public interface IImagesService
    {
        Task Serialize();

        Task<ImageModel> Deserialize(string path);
    }
}
