using Avalonia.Media.Imaging;
using Common;
using CSharpFunctionalExtensions;
using Domain.Model;
using Domain.Models;
using ImageViewer.Models;
using System;
using System.IO;

namespace ImageViewer.Helpers
{
    public static class ImagesMapper
    {
        public static Result<ImagesAggregate, Error> MapToDomain(ImagesModel model)
        {
            var creationResult = ImagesAggregate.Create();
            if (creationResult.IsFailure) return creationResult.Error;

            var images = creationResult.Value;

            foreach (var sourceImage in model.Images)
            {
                var sourceMetadata = sourceImage.Metadata;
                var metadataCreationResult = ImageMetadata.Create(
                    sourceMetadata.FileName,
                    sourceMetadata.Size,
                    sourceMetadata.CreationTime);
                if (metadataCreationResult.IsFailure) return metadataCreationResult.Error;
                var mappedMetadata = metadataCreationResult.Value;

                var base64Result = sourceImage.Image.ToBase64();
                if (base64Result.IsFailure) return base64Result.Error;
                var base64 = base64Result.Value;

                var imageModelCreationResult = ImageEntity.Create(base64, mappedMetadata);
                if (imageModelCreationResult.IsFailure) return imageModelCreationResult.Error;
                var mappedImage = imageModelCreationResult.Value;

                var addImageResult = images.AddImage(mappedImage);
                if (addImageResult.IsFailure) return addImageResult.Error;
            }

            return images;
        }

        public static Result<ImagesModel, Error> MapToModel(ImagesAggregate aggregate)
        {
            var imagesModelCreationResult = ImagesModel.Create();
            if (imagesModelCreationResult.IsFailure) return imagesModelCreationResult.Error;
            var images = imagesModelCreationResult.Value;

            foreach (var sourceImage in aggregate.Items)
            {
                var sourceMetadata = sourceImage.Metadata;
                var metadataCreationResult = MetadataModel.Create(
                    sourceMetadata.FileName,
                    sourceMetadata.Size,
                    sourceMetadata.CreationTime);
                if (metadataCreationResult.IsFailure) return metadataCreationResult.Error;
                var mappedMetadata = metadataCreationResult.Value;

                var bitmapResult = sourceImage.Byte64.ToBitmap();
                if (bitmapResult.IsFailure) return bitmapResult.Error;
                var bitmap = bitmapResult.Value;

                var imageModelCreationResult = ImageModel.Create(bitmap, mappedMetadata);
                if (imageModelCreationResult.IsFailure) return imageModelCreationResult.Error;
                var mappedImage = imageModelCreationResult.Value;

                var addImageResult = images.AddImage(mappedImage);
                if (addImageResult.IsFailure) return addImageResult.Error;
            }

            return images;
        }

        private static Result<Bitmap, Error> ToBitmap(this string base64)
        {
            try
            {
                var binaryData = Convert.FromBase64String(base64);
                using var stream = new MemoryStream(binaryData);
                return new Bitmap(stream);
            }
            catch (Exception e)
            {
                return GeneralErrors.ValueIsInvalid(nameof(base64));
            }
        }

        private static Result<string, Error> ToBase64(this Bitmap bitmap)
        {
            try
            {
                using var memoryStream = new MemoryStream();
                bitmap.Save(memoryStream);
                var binaryData = memoryStream.ToArray();
                return Convert.ToBase64String(binaryData);
            }
            catch (Exception ex)
            {
                return GeneralErrors.ValueIsInvalid(nameof(bitmap));
            }
        }
    }
}
