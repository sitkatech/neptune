﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Neptune.WebMvc.Common.Models;
using Neptune.WebMvc.Common.Mvc;

namespace Neptune.WebMvc.Views.LandUseBlockGeometry
{
    public class UpdateLandUseBlockGeometryViewModel : FormViewModel
    {
        [Required]
        [DisplayName("Zipped File Geodatabase to Upload")]
        [SitkaFileExtensions("zip")]
        public IFormFile FileResourceData { get; set; }
        public int PersonID { get; set; }
        [Required]
        [DisplayName("Stormwater Jurisdiction")]
        public int? StormwaterJurisdictionID { get; set; }
    }
}
