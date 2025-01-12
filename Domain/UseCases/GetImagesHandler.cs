using Domain.Models;
using Domain.Ports;
using MediatR;

namespace Domain.UseCases;

public class GetImagesHandler : IRequestHandler<GetImagesCommand, ImagesAggregate?>
{
    private readonly IImagesRepository _repository;
    
    public GetImagesHandler(IImagesRepository repository)
    {
        _repository = repository;
    }
    
    public Task<ImagesAggregate?> Handle(GetImagesCommand request, CancellationToken cancellationToken)
    {
        _repository.ChangeAddress(request.Path);
        return _repository.GetAsync();
    }
}