using Common;
using CSharpFunctionalExtensions;
using Domain.Model;
using System.Collections.ObjectModel;

namespace Domain.Models
{
    public class ImagesAggregate
    {
        private readonly List<ImageEntity> _images = [];

        private ImagesAggregate()
        {
            Items = _images.AsReadOnly();
        }

        public ReadOnlyCollection<ImageEntity> Items { get; }

        public static Result<ImagesAggregate, Error> Create() => new ImagesAggregate();

        public UnitResult<Error> AddImage(ImageEntity image)
        {
            if (image is null) return GeneralErrors.ValueIsRequired(nameof(image));

            _images.Add(image);

            return UnitResult.Success<Error>();
        }
    }
}
