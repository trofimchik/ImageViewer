using Common;
using CSharpFunctionalExtensions;

namespace Domain.Model
{
    public class ImageMetadata : ValueObject
    {
        private ImageMetadata(string fileName, int size, DateTime creationTime)
        {
            FileName = fileName;
            Size = size;
            CreationTime = creationTime;
        }

        public string FileName { get; }
        public int Size { get; }
        public DateTime CreationTime { get; }

        public static Result<ImageMetadata, Error> Create(string filename, int size, DateTime creationTime)
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
