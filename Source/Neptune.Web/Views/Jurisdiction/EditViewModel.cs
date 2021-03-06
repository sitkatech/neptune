﻿using System.ComponentModel.DataAnnotations;
using LtInfo.Common.Models;
using Neptune.Web.Models;

namespace Neptune.Web.Views.Jurisdiction
{
    public class EditViewModel : FormViewModel
    {
        public int StormwaterJurisdictionID { get; set; }

        [Required(ErrorMessage = "Choose a Public BMP Visibility Type")]
        public int? StormwaterJurisdictionPublicBMPVisibilityTypeID { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditViewModel()
        {
        }

        public EditViewModel(Models.StormwaterJurisdiction stormwaterJurisdiction)
        {
            StormwaterJurisdictionID = stormwaterJurisdiction.StormwaterJurisdictionID;
            StormwaterJurisdictionPublicBMPVisibilityTypeID =
                stormwaterJurisdiction.StormwaterJurisdictionPublicBMPVisibilityTypeID;
        }

        public void UpdateModel(Models.StormwaterJurisdiction stormwaterJurisdiction, Person currentPerson)
        {
            stormwaterJurisdiction.StormwaterJurisdictionPublicBMPVisibilityTypeID =
                StormwaterJurisdictionPublicBMPVisibilityTypeID.Value;
        }
    }
}