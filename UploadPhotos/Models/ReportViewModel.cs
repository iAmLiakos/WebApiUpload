﻿using Microsoft.PowerBI.Api.V1.Models;

namespace UploadPhotos.Models
{
    public class ReportViewModel
    {
        public Report Report { get; set; }

        public string AccessToken { get; set; }
    }
}