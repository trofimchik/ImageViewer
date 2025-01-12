using Domain.Model;
using FluentAssertions;

namespace Domain.UnitTests
{
    public class ImageMetadataShould
    {
        [Fact]
        public void BeCreatedIfParamsAreCorrect()
        {
            var metadataCreationResult = ImageMetadata.Create("some name", 1234, DateTime.UtcNow);

            metadataCreationResult.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void ReturnErrorWhenValueIsInvalid()
        {
            var metadataCreationResult = ImageMetadata.Create("some name", -1234, DateTime.UtcNow);

            metadataCreationResult.IsSuccess.Should().BeFalse();
        }

        // todo: Тесты
    }
}