using System;
using System.Threading.Tasks;
using ImageViewer.Models;
using MediatR;

namespace ImageViewer.Services;

public class ImagesService : IImagesService
{
    private readonly IMediator _mediator;
    public ImagesService(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public async Task Serialize()
    {
        throw new NotImplementedException();
    }
    
    public async Task<ImageModel> Deserialize(string path)
    {
        throw new NotImplementedException();
    }
}