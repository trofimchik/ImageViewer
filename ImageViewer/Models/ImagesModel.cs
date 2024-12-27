using CSharpFunctionalExtensions;
using System.Collections.ObjectModel;
using Common;

namespace ImageViewer.Models
{
    public class ImagesModel
    {
        private readonly ObservableCollection<ImageModel> _images = [];

        private ImagesModel()
        {
            Images = new ReadOnlyObservableCollection<ImageModel>(_images);
        }

        public ReadOnlyObservableCollection<ImageModel> Images { get; }
        
        public static Result<ImagesModel, Error> Create() => new();
        
        public UnitResult<Error> AddImage(ImageModel image)
        {
            if (image is null) return GeneralErrors.ValueIsRequired(nameof(image));

            _images.Add(image);

            return UnitResult.Success<Error>();
        }
    }
}
