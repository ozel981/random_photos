using RandomPhotosAPI.ModelsDTO;
using RandomPhotosAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomPhotosAPI.Services
{
    public class PhotoHistoryService : IPhotoHistoryService
    {
        public virtual void AddPhoto(PhotoDTO photo)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<PhotoDTO> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
