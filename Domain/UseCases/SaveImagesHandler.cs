using Domain.Models;
using Domain.Ports;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UseCases
{
    public class SaveImagesHandler : IRequestHandler<SaveImagesCommand, bool>
    {
        private readonly IImagesRepository _repository;

        public SaveImagesHandler(IImagesRepository repository)
        {
            _repository = repository;
        }

        public Task<bool> Handle(SaveImagesCommand request, CancellationToken cancellationToken)
        {
            _repository.ChangeAddress(request.Path);
            return _repository.SaveAsync(request.ImagesAggregate);
        }
    }
}
