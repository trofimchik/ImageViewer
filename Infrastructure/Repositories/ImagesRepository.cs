using Domain.Models;
using Domain.Ports;
using static System.Net.Mime.MediaTypeNames;

namespace Infrastructure.Repositories;

public class ImagesRepository : IImagesRepository
{
    private string _path;

    public void ChangeAddress(string path)
    {
        path = _path;
    }

    public Task<ImagesAggregate> GetAsync()
    {
        throw new NotImplementedException();
    }

    public Task SaveAsync(ImagesAggregate images)
    {
        throw new NotImplementedException();
    }
}