using Avalonia.Media.Imaging;
using Common;
using CSharpFunctionalExtensions;

namespace ImageViewer.Models
{
    public class ImageModel
    {
        private ImageModel(Bitmap image, MetadataModel meta)
        {
            Image = image;
            Metadata = meta;
        }

        public Bitmap Image { get; }
        public MetadataModel Metadata { get; }

        public static Result<ImageModel, Error> Create(Bitmap image, MetadataModel metadataModel)
        {
            if (image is null) return GeneralErrors.ValueIsRequired(nameof(image));
            if (metadataModel is null) return GeneralErrors.ValueIsRequired(nameof(metadataModel));
            
            return new ImageModel(image, metadataModel);
        }
    }
}