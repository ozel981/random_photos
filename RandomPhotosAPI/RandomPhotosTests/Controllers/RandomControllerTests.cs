using Microsoft.AspNetCore.Mvc;
using Moq;
using RandomPhotosAPI.Controllers;
using RandomPhotosAPI.ModelsDTO;
using RandomPhotosAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RandomPhotosTests.Controllers
{
    public class RandomControllerTests
    {
        private const string url = "https://test.jpg";
        private readonly DateTime date = DateTime.Now;

        [Fact(DisplayName = "Return Ok result photo object for random request")]
        public void GetRandom_ValidCall()
        {

            PhotoDTO photo = new PhotoDTO
            {
                Url = url,
                DownloadDate = date
            };

            Mock<PhotoHistoryService> mockPhotoHistoryService = new Mock<PhotoHistoryService>();
            mockPhotoHistoryService.Setup(s => s.AddPhoto(It.IsAny<PhotoDTO>()));

            Mock<RedditRandomPhotoService> mockRandomPhotoService = new Mock<RedditRandomPhotoService>();
            mockRandomPhotoService.Setup(s => s.GetRandomPhoto()).Returns(photo);

            RandomController controller = new RandomController(mockPhotoHistoryService.Object, mockRandomPhotoService.Object);

            var result = controller.Get();

            Assert.IsType<OkObjectResult>(result);
        }
    }
}
