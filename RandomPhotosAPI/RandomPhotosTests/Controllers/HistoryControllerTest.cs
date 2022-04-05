using Microsoft.AspNetCore.Mvc;
using Moq;
using RandomPhotosAPI.Controllers;
using RandomPhotosAPI.ModelsDTO;
using RandomPhotosAPI.Services;
using RandomPhotosAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace RandomPhotosTests.Controllers
{
    public class HistoryControllerTests
    {
        private const string url1 = "https://test1.jpg";
        private const string url2 = "https://test2.jpg";
        private readonly DateTime date = DateTime.Now;

        [Fact(DisplayName = "Return Ok result for history request")]
        public async Task GetRandom_ValidCallAsync()
        {
            // arrange
            List<PhotoDTO> photos = new List<PhotoDTO>() {
            new PhotoDTO
            {
                Url = url1,
                DownloadDate = date
            },
            new PhotoDTO
            {
                Url = url2,
                DownloadDate = date
            }};

            Mock<IPhotoHistoryService> mockPhotoHistoryService = new Mock<IPhotoHistoryService>();
            mockPhotoHistoryService.Setup(s => s.GetAllAsync()).ReturnsAsync(photos);

            HistoryController controller = new HistoryController(mockPhotoHistoryService.Object);

            // act
            var result = await controller.GetAsync();

            // assert
            OkObjectResult okObjectResult = Assert.IsType<OkObjectResult>(result);
            List<PhotoDTO> resultValue = Assert.IsType<List<PhotoDTO>>(okObjectResult.Value);
            Assert.Equal(2, resultValue.Count);
            Assert.Equal(url1, resultValue[0].Url);
            Assert.Equal(url2, resultValue[1].Url);
        }

        [Fact(DisplayName = "Return BadRequest on error")]
        public async Task GetRandom_BadRequest_ErrorAsync()
        {
            // arragne
            Mock<IPhotoHistoryService> mockPhotoHistoryService = new Mock<IPhotoHistoryService>();
            mockPhotoHistoryService.Setup(s => s.GetAllAsync()).Throws(new Exception());

            HistoryController controller = new HistoryController(mockPhotoHistoryService.Object);

            // act
            var result = await controller.GetAsync();

            // assert
            ObjectResult objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(400, objectResult.StatusCode);
        }
    }
}
