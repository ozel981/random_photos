using Microsoft.AspNetCore.Mvc;
using Moq;
using RandomPhotosAPI.Controllers;
using RandomPhotosAPI.ModelsDTO;
using RandomPhotosAPI.Services;
using System;
using System.Threading.Tasks;
using Xunit;

namespace RandomPhotosTests.Controllers
{
    public class RandomControllerTests
    {
        private const string url = "https://test.jpg";
        private readonly DateTime date = DateTime.Now;

        [Fact(DisplayName = "Return Ok result photo object for random request")]
        public async Task GetRandom_ValidCallAsync()
        {

            PhotoDTO photo = new PhotoDTO
            {
                Url = url,
                DownloadDate = date
            };

            Mock<PhotoHistoryService> mockPhotoHistoryService = new Mock<PhotoHistoryService>();
            mockPhotoHistoryService.Setup(s => s.AddPhoto(It.IsAny<PhotoDTO>()));

            Mock<RedditRandomPhotoService> mockRandomPhotoService = new Mock<RedditRandomPhotoService>();
            mockRandomPhotoService.Setup(s => s.GetRandomPhoto()).ReturnsAsync(photo);

            RandomController controller = new RandomController(mockPhotoHistoryService.Object, mockRandomPhotoService.Object);

            var result = await controller.GetAsync();

            OkObjectResult okObjectResult = Assert.IsType<OkObjectResult>(result);
            PhotoDTO resultValue = Assert.IsType<PhotoDTO>(okObjectResult.Value);
            Assert.Equal(((PhotoDTO)resultValue).Url, url);
            Assert.True(((PhotoDTO)resultValue).DownloadDate.Equals(date));
        }

        [Fact(DisplayName = "Return BadRequest error")]
        public async Task GetRandom_BadRequest_ErrorAsync()
        {
            Mock<PhotoHistoryService> mockPhotoHistoryService = new Mock<PhotoHistoryService>();
            mockPhotoHistoryService.Setup(s => s.AddPhoto(It.IsAny<PhotoDTO>()));

            Mock<RedditRandomPhotoService> mockRandomPhotoService = new Mock<RedditRandomPhotoService>();
            mockRandomPhotoService.Setup(s => s.GetRandomPhoto()).Throws(new Exception());

            RandomController controller = new RandomController(mockPhotoHistoryService.Object, mockRandomPhotoService.Object);

            var result = await controller.GetAsync();

            ObjectResult objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(400, objectResult.StatusCode);
        }
    }
}
