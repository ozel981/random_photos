using RandomPhotosAPI.ModelsDTO;
using RandomPhotosAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomPhotosAPI.Services
{
    public class RedditRandomPhotoService : IRandomPhotoService
    {
        public virtual PhotoDTO GetRandomPhoto()
        {
            throw new NotImplementedException();
        }
    }
}
