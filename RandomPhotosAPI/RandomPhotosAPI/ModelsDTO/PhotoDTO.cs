using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomPhotosAPI.ModelsDTO
{
    public class PhotoDTO
    {
        public string Url { get; set; }
        public DateTime DownloadDate { get; set; }
    }
}
