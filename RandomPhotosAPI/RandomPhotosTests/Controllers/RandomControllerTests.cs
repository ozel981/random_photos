using Microsoft.AspNetCore.Mvc;
using Moq;
using RandomPhotosAPI.Controllers;
using RandomPhotosAPI.ModelsDTO;
using RandomPhotosAPI.Services;
using RandomPhotosAPI.Services.Interfaces;
using System;
using System.Threading.Tasks;
using Xunit;

namespace RandomPhotosTests.Controllers
{
    public class RandomControllerTests
    {
        private const string url = "https://test.jpg";
        private readonly DateTime date = DateTime.Now;

        [Fact(DisplayName = "Return Ok result for random request")]
        public async Task GetRandom_ValidCallAsync()
        {
            // arrange
            PhotoDTO photo = new PhotoDTO
            {
                Url = url,
                DownloadDate = date
            };

            Mock <IPhotoHistoryService> mockPhotoHistoryService = new Mock<IPhotoHistoryService>();
            mockPhotoHistoryService.Setup(s => s.AddPhotoAsync(It.IsAny<PhotoDTO>()));

            Mock<IRandomPhotoService> mockRandomPhotoService = new Mock<IRandomPhotoService>();
            mockRandomPhotoService.Setup(s => s.GetRandomPhotoAsync()).ReturnsAsync(photo);

            RandomController controller = new RandomController(mockPhotoHistoryService.Object, mockRandomPhotoService.Object);

            // act
            var result = await controller.GetAsync();

            //assert
            OkObjectResult okObjectResult = Assert.IsType<OkObjectResult>(result);
            PhotoDTO resultValue = Assert.IsType<PhotoDTO>(okObjectResult.Value);
            Assert.Equal((resultValue).Url, url);
            Assert.True((resultValue).DownloadDate.Equals(date));
            mockPhotoHistoryService.Verify(m => m.AddPhotoAsync(It.IsAny<PhotoDTO>()), Times.Once);
        }

        [Fact(DisplayName = "Return BadRequest on get random photo error")]
        public async Task GetRandom_BadRequest_GetRandomPhotoErrorAsync()
        {
            // arrange
            Mock<IPhotoHistoryService> mockPhotoHistoryService = new Mock<IPhotoHistoryService>();
            mockPhotoHistoryService.Setup(s => s.AddPhotoAsync(It.IsAny<PhotoDTO>()));

            Mock<IRandomPhotoService> mockRandomPhotoService = new Mock<IRandomPhotoService>();
            mockRandomPhotoService.Setup(s => s.GetRandomPhotoAsync()).Throws(new Exception());

            RandomController controller = new RandomController(mockPhotoHistoryService.Object, mockRandomPhotoService.Object);

            // act
            var result = await controller.GetAsync();

            // assety
            ObjectResult objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(400, objectResult.StatusCode);
        }

        [Fact(DisplayName = "Return BadRequest on add photo error")]
        public async Task GetRandom_BadRequest_ErrorAsync()
        {
            // arrange
            PhotoDTO photo = new PhotoDTO
            {
                Url = url,
                DownloadDate = date
            };

            Mock<IPhotoHistoryService> mockPhotoHistoryService = new Mock<IPhotoHistoryService>();
            mockPhotoHistoryService.Setup(s => s.AddPhotoAsync(It.IsAny<PhotoDTO>())).Throws(new Exception());

            Mock<IRandomPhotoService> mockRandomPhotoService = new Mock<IRandomPhotoService>();
            mockRandomPhotoService.Setup(s => s.GetRandomPhotoAsync()).ReturnsAsync(photo);

            RandomController controller = new RandomController(mockPhotoHistoryService.Object, mockRandomPhotoService.Object);

            // act
            var result = await controller.GetAsync();

            // assety
            ObjectResult objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(400, objectResult.StatusCode);
        }
    }
}
