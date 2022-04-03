using Microsoft.EntityFrameworkCore;
using RandomPhotosAPI.Database;
using RandomPhotosAPI.ModelsDTO;
using RandomPhotosAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RandomPhotosAPI.Services
{
    public class PhotoHistoryService : IPhotoHistoryService
    {
        RandomPhotosDBContext _randomPhotosDBContext;
        public PhotoHistoryService(RandomPhotosDBContext randomPhotosDBContext)
        {
            _randomPhotosDBContext = randomPhotosDBContext;
        }
        public async virtual Task AddPhoto(PhotoDTO photo)
        {
            Database.DatabaseModels.Photo photoDB = new Database.DatabaseModels.Photo
            {
                Url = photo.Url,
                Date = photo.DownloadDate,
            };
            await _randomPhotosDBContext.Photos.AddAsync(photoDB);
            await _randomPhotosDBContext.SaveChangesAsync();
        }

        public async virtual Task<IEnumerable<PhotoDTO>> GetAll()
        {
            return (await _randomPhotosDBContext.Photos.ToListAsync()).ConvertAll<PhotoDTO>(photo => new PhotoDTO
            {
                Url = photo.Url,
                DownloadDate = photo.Date
            });
        }
    }
}
