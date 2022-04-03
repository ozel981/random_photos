using RandomPhotosAPI.ModelsDTO;
using RandomPhotosAPI.Services.Interfaces;
using System;
using System.Collections.Generic;

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
