using Domain.Models;
using MediatR;

namespace Domain.UseCases;

public class GetImagesCommand : IRequest<ImagesAggregate>
{
    public GetImagesCommand(string path)
    {
        if(string.IsNullOrWhiteSpace(path)) throw new ArgumentNullException(nameof(path));
        Path = path;
    }

    public string Path { get; }
}