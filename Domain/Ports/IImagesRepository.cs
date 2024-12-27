using Domain.Models;

namespace Domain.Ports
{
    public interface IImagesRepository
    {
        void ChangeAddress(string path);
        Task<ImagesAggregate> GetAsync();
        Task SaveAsync(ImagesAggregate images);
    }
}