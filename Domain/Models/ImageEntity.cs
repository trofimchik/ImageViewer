using Common;
using CSharpFunctionalExtensions;
using System.Diagnostics.CodeAnalysis;

namespace Domain.Model
{
    public class ImageEntity : Entity
    {
        [ExcludeFromCodeCoverage]
        public ImageEntity()
        {
            
        }

        private ImageEntity(byte[] bytes, ImageMetadata metadata)
        {
            Bytes = bytes;
            Metadata = metadata;
        }

        public byte[] Bytes { get; set; }
        public ImageMetadata Metadata { get; set; }

        public static Result<ImageEntity, Error> Create(byte[] bytes, ImageMetadata metadata)
        {
            if (bytes.Length == 0) return GeneralErrors.ValueIsRequired(nameof(bytes));
            if (metadata is null) return GeneralErrors.ValueIsRequired(nameof(metadata));

            return new ImageEntity(bytes, metadata);
        }
    }
}
