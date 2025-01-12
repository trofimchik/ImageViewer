using Common;
using CSharpFunctionalExtensions;
using System.Diagnostics.CodeAnalysis;

namespace Domain.Model
{
    public class ImageMetadata : ValueObject
    {
        [ExcludeFromCodeCoverage]
        public ImageMetadata()
        {
            
        }
        private ImageMetadata(string fileName, long size, DateTime creationTime)
        {
            FileName = fileName;
            Size = size;
            CreationTime = creationTime;
        }

        public string FileName { get; set; }
        public long Size { get; set; }
        public DateTime CreationTime { get; set; }

        public static Result<ImageMetadata, Error> Create(string filename, long size, DateTime creationTime)
        {
            if (string.IsNullOrWhiteSpace(filename)) return GeneralErrors.ValueIsRequired(nameof(filename));
            if (size < 0) return GeneralErrors.ValueIsInvalid(nameof(size));
            if (creationTime > DateTime.UtcNow) return GeneralErrors.ValueIsInvalid(nameof(creationTime));

            return new ImageMetadata(filename, size, creationTime);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FileName;
            yield return Size;
            yield return CreationTime;
        }
    }
}
