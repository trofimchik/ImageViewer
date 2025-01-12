using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UseCases
{
    public class SaveImagesCommand : IRequest<bool>
    {
        public SaveImagesCommand(string path, ImagesAggregate imagesAggregate)
        {
            if (string.IsNullOrWhiteSpace(path)) throw new ArgumentNullException(nameof(path));
            if (imagesAggregate == null) throw new ArgumentNullException(nameof(imagesAggregate));

            Path = path;
            ImagesAggregate = imagesAggregate;
        }

        public string Path { get; }

        public ImagesAggregate ImagesAggregate { get; }
    }
}
