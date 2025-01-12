using Avalonia.Media.Imaging;
using Avalonia.Platform.Storage;
using Common;
using CSharpFunctionalExtensions;
using Domain.Model;
using Domain.Models;
using ImageViewer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ImageViewer.Helpers
{
    public static class ImagesMapper
    {
        public static ImagesAggregate MapToDomain(this IEnumerable<ImageModel> modelImages)
        {
            if (modelImages.Count() == 0) throw new ArgumentException(nameof(modelImages));
            var creationResult = ImagesAggregate.Create();
            if (creationResult.IsFailure) throw new Exception(creationResult.Error.Message);

            var images = creationResult.Value;

            foreach (var sourceImage in modelImages)
            {
                var sourceMetadata = sourceImage.Metadata;
                var metadataCreationResult = ImageMetadata.Create(
                    sourceMetadata.FileName,
                    sourceMetadata.Size,
                    sourceMetadata.CreationTime);
                if (metadataCreationResult.IsFailure) throw new Exception(metadataCreationResult.Error.Message);
                var mappedMetadata = metadataCreationResult.Value;

                var bytes = sourceImage.Image.ToByteArray();

                var imageModelCreationResult = ImageEntity.Create(bytes, mappedMetadata);
                if (imageModelCreationResult.IsFailure) throw new Exception(imageModelCreationResult.Error.Message);
                var mappedImage = imageModelCreationResult.Value;

                var addImageResult = images.AddImage(mappedImage);
                if (addImageResult.IsFailure) throw new Exception(addImageResult.Error.Message);
            }

            return images;
        }

        public static List<ImageModel> MapToModel(this ImagesAggregate aggregate)
        {
            if (aggregate == null) throw new ArgumentNullException(nameof(aggregate));

            List<ImageModel> images = [];

            foreach (var sourceImage in aggregate.Items)
            {
                var sourceMetadata = sourceImage.Metadata;
                var metadataCreationResult = MetadataModel.Create(
                    sourceMetadata.FileName,
                    sourceMetadata.Size,
                    sourceMetadata.CreationTime);

                if (metadataCreationResult.IsFailure) throw new Exception(metadataCreationResult.Error.Message);
                var mappedMetadata = metadataCreationResult.Value;

                var bitmap = sourceImage.Bytes.ToBitmap();

                var imageModelCreationResult = ImageModel.Create(bitmap, mappedMetadata);
                if (imageModelCreationResult.IsFailure) throw new Exception(imageModelCreationResult.Error.Message);
                var mappedImage = imageModelCreationResult.Value;

                images.Add(mappedImage);
            }

            return images;
        }

        public static async Task<List<ImageModel>> MapToModelAsync(this IReadOnlyList<IStorageFile> files)
        {
            if (files == null) throw new ArgumentNullException(nameof(files));

            List<ImageModel> images = [];

            var tasks = files.Select(async f => 
            {
                var fileInfo = new FileInfo(f.Path.LocalPath);
                if (!fileInfo.Exists) throw new FileNotFoundException();

                var name = fileInfo.Name;
                var length = fileInfo.Length;
                var creationTime = fileInfo.CreationTimeUtc;

                var metadataResult = MetadataModel.Create(name, length, creationTime);
                if (metadataResult.IsFailure) throw new Exception(metadataResult.Error.Message);

                using var stream = await f.OpenReadAsync();
                var bitmap = await Task.Run(() => Bitmap.DecodeToWidth(stream, 400));

                var imageModelResult = ImageModel.Create(bitmap, metadataResult.Value);
                if (imageModelResult.IsFailure) throw new Exception(imageModelResult.Error.Message);

                return imageModelResult.Value;
            });

            try
            {
                var results = await Task.WhenAll(tasks);
                return results.Where(imageModel => imageModel != null).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Не удалось преобразовать объекты", ex);
            }
        }
    }
}
