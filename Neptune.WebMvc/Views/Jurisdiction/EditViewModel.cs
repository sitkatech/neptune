﻿using System.ComponentModel.DataAnnotations;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common.Models;

namespace Neptune.WebMvc.Views.Jurisdiction
{
    public class EditViewModel : FormViewModel
    {
        public int StormwaterJurisdictionID { get; set; }

        [Required(ErrorMessage = "Choose a Public BMP Visibility Type")]
        public int? StormwaterJurisdictionPublicBMPVisibilityTypeID { get; set; }
        
        [Required(ErrorMessage = "Choose a Public WQMP Visibility Type")]
        public int? StormwaterJurisdictionPublicWQMPVisibilityTypeID { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditViewModel()
        {
        }

        public EditViewModel(StormwaterJurisdiction stormwaterJurisdiction)
        {
            StormwaterJurisdictionID = stormwaterJurisdiction.StormwaterJurisdictionID;
            StormwaterJurisdictionPublicBMPVisibilityTypeID =
                stormwaterJurisdiction.StormwaterJurisdictionPublicBMPVisibilityTypeID;
            StormwaterJurisdictionPublicWQMPVisibilityTypeID =
                stormwaterJurisdiction.StormwaterJurisdictionPublicWQMPVisibilityTypeID;
        }

        public void UpdateModel(StormwaterJurisdiction stormwaterJurisdiction, Person currentPerson)
        {
            stormwaterJurisdiction.StormwaterJurisdictionPublicBMPVisibilityTypeID =
                StormwaterJurisdictionPublicBMPVisibilityTypeID.Value;
            stormwaterJurisdiction.StormwaterJurisdictionPublicWQMPVisibilityTypeID =
                StormwaterJurisdictionPublicWQMPVisibilityTypeID.Value;
        }
    }
}