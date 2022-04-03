using System;

namespace RandomPhotosAPI.Database.DatabaseModels
{
    public class Photo
    {
        public int ID { get; set; }
        public string Url { get; set; }
        public DateTime Date { get; set; }
    }
}
