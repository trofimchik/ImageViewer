using System;
using Common;
using CSharpFunctionalExtensions;

namespace ImageViewer.Models
{
    public class MetadataModel
    {
        private MetadataModel(string filename, int size, DateTime creationTime)
        {
            FileName = filename;
            Size = size;
            CreationTime = creationTime;
        }
        
        public string FileName { get; }
        public int Size { get; }
        public DateTime CreationTime { get; }

        public static Result<MetadataModel, Error> Create(string filename, int size, DateTime creationTime)
        {
            if (string.IsNullOrWhiteSpace(filename)) return GeneralErrors.ValueIsRequired(nameof(filename));
            if (size < 0) return GeneralErrors.ValueIsInvalid(nameof(size));
            if (creationTime > DateTime.UtcNow) return GeneralErrors.ValueIsInvalid(nameof(creationTime));
            
            return new MetadataModel(filename, size, creationTime);
        }
    }
}
