using Common;
using CSharpFunctionalExtensions;

namespace Domain.Model
{
    public class ImageEntity : Entity
    {
        private ImageEntity(string byte64, ImageMetadata metadata)
        {
            Byte64 = byte64;
            Metadata = metadata;
        }

        public string Byte64 { get; }
        public ImageMetadata Metadata { get; }

        public static Result<ImageEntity, Error> Create(string binaryData, ImageMetadata metadata)
        {
            if (string.IsNullOrWhiteSpace(binaryData)) return GeneralErrors.ValueIsRequired(nameof(binaryData));
            if (metadata is null) return GeneralErrors.ValueIsRequired(nameof(metadata));

            return new ImageEntity(binaryData, metadata);
        }
    }
}
