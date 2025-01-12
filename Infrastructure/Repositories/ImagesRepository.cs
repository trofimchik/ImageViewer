using Avalonia.Media.Imaging;
using Avalonia.Platform.Storage;
using Domain.Models;
using Domain.Ports;
using System.ComponentModel;
using System.Diagnostics;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Infrastructure.Repositories;

public class ImagesRepository : IImagesRepository
{
    private static readonly JsonSerializerOptions SerializeOptions = new() { WriteIndented = true };
    private string _path;
    public void ChangeAddress(string path)
    {
        _path = path;
    }

    public async Task<ImagesAggregate?> GetAsync()
    {
        var json = await File.ReadAllTextAsync(_path);
        var aggregate = JsonSerializer.Deserialize<ImagesAggregate>(json);
        return aggregate;
    }

    public async Task<bool> SaveAsync(ImagesAggregate images)
    {
        var json = JsonSerializer.Serialize(images, SerializeOptions);
        await File.WriteAllTextAsync(_path, json);
        return true;
    }
}