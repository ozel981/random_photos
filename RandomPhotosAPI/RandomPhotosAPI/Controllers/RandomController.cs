﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RandomPhotosAPI.ModelsDTO;
using RandomPhotosAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomPhotosAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class RandomController : Controller
    {
        private readonly IPhotoHistoryService _photoHistoryService;
        private readonly IRandomPhotoService _randomPhotoService;
        public RandomController(IPhotoHistoryService photoHistoryService, IRandomPhotoService randomPhotoService)
        {
            _photoHistoryService = photoHistoryService;
            _randomPhotoService = randomPhotoService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new PhotoDTO { Url = "https://tiny.pl/9kszp", DownloadDate = DateTime.Now });
        }
    }
}