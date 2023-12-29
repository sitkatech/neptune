﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Neptune.WebMvc.Common.Models;
using Neptune.WebMvc.Common.Mvc;

namespace Neptune.WebMvc.Views.ParcelLayerUpload
{
    public class UploadParcelLayerViewModel : FormViewModel
    {
        [Required]
        [DisplayName("Zipped File Geodatabase to Upload")]
        [SitkaFileExtensions("zip")]
        public IFormFile FileResourceData { get; set; }
    }
}
