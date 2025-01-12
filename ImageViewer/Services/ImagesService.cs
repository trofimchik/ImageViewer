using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using Common;
using Domain.UseCases;
using ImageViewer.Helpers;
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

    public async Task SerializeAsync(string path, IEnumerable<ImageModel> images)
    {
        var mapped = await Task.Run(() => ImagesMapper.MapToDomain(images));

        SaveImagesCommand saveImagesCommand = new(path, mapped);
        var isSuccess = await _mediator.Send(saveImagesCommand);
        if (!isSuccess) throw new Exception("Ошибка сериализации");
    }

    public async Task<List<ImageModel>?> DeserializeAsync(string path)
    {
        GetImagesCommand getImagesCommand = new(path);
        var aggregate = await _mediator.Send(getImagesCommand);
        if(aggregate == null) return null;
        var mapped = aggregate.MapToModel();

        return mapped;
    }
}