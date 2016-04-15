using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UploadPhotos.Models
{
    public class Photos
    {
        public string PhotoName { get; set; }
        public DateTime DateCreated { get; set; }       
        public int PhotoDimensions { get; set; }

    }
}