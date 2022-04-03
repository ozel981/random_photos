using RandomPhotosAPI.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomPhotosAPI.Services.Interfaces
{
    public interface IPhotoHistoryService
    {
        public Task<IEnumerable<PhotoDTO>> GetAll();
        public Task AddPhoto(PhotoDTO photo);
    }
}
