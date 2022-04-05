using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RandomPhotosAPI.ModelsDTO;
using RandomPhotosAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace RandomPhotosAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class HistoryController : Controller
    {
        private readonly IPhotoHistoryService _photoHistoryService;
        public HistoryController(IPhotoHistoryService photoHistoryService)
        {
            _photoHistoryService = photoHistoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            List<PhotoDTO> photos;
            try
            {
                photos = (await _photoHistoryService.GetAllAsync()).ToList();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ex.Message);
            }
            return Ok(photos);
        }
    }
}
