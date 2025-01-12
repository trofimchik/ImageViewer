using Common;
using CSharpFunctionalExtensions;
using Domain.Model;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace Domain.Models
{
    public class ImagesAggregate
    {
        public ImagesAggregate()
        {

        }

        public List<ImageEntity> Items { get; set; } = [];

        public static Result<ImagesAggregate, Error> Create() => new ImagesAggregate();

        public UnitResult<Error> AddImage(ImageEntity image)
        {
            if (image is null) return GeneralErrors.ValueIsRequired(nameof(image));

            Items.Add(image);

            return UnitResult.Success<Error>();
        }
    }
}
