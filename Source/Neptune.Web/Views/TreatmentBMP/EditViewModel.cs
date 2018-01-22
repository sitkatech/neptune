/*-----------------------------------------------------------------------
<copyright file="EditViewModel.cs" company="Tahoe Regional Planning Agency">
Copyright (c) Tahoe Regional Planning Agency. All rights reserved.
<author>Sitka Technology Group</author>
</copyright>

<license>
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License <http://www.gnu.org/licenses/> for more details.

Source code is available upon request via <support@sitkatech.com>.
</license>
-----------------------------------------------------------------------*/

using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Spatial;
using System.Linq;
using LtInfo.Common;
using LtInfo.Common.DbSpatial;
using LtInfo.Common.Models;
using Neptune.Web.Common;
using Neptune.Web.Models;

namespace Neptune.Web.Views.TreatmentBMP
{
    public class EditViewModel : FormViewModel, IValidatableObject
    {
        public int TreatmentBMPID { get; set; }

        [Required]
        [DisplayName("Name")]
        [StringLength(Models.TreatmentBMP.FieldLengths.TreatmentBMPName)]
        public string TreatmentBMPName { get; set; }

        [Required]
        [FieldDefinitionDisplay(FieldDefinitionEnum.Jurisdiction)]
        public int StormwaterJurisdictionID { get; set; }

        [Required(ErrorMessage = "Choose a BMP Type")]
        [FieldDefinitionDisplay(FieldDefinitionEnum.TreatmentBMPType)]
        public int TreatmentBMPTypeID { get; set; }

        [Required(ErrorMessage =  "Must specify a BMP Longitude")]
        [DisplayName("Longitude")]
        [Range(-180, 180, ErrorMessage = "Longitude must be between -180 and 180")]
        public double? TreatmentBMPPointX { get; set; }

        [Required(ErrorMessage = "Must specify a BMP Latitude")]
        [DisplayName("Latitude")]
        [Range(-90, 90, ErrorMessage = "Latitude must be between -90 and 90")]
        public double? TreatmentBMPPointY { get; set; }

        [DisplayName("Notes")]
        [StringLength(Models.TreatmentBMP.FieldLengths.Notes)]
        public string Notes { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditViewModel()
        {
        }

        public EditViewModel(Models.TreatmentBMP treatmentBMP, DbGeometry treatmentBMPPoint)
        {
            TreatmentBMPID = treatmentBMP.TreatmentBMPID;
            TreatmentBMPName = treatmentBMP.TreatmentBMPName;
            StormwaterJurisdictionID = treatmentBMP.StormwaterJurisdictionID;
            TreatmentBMPTypeID = treatmentBMP.TreatmentBMPTypeID;
            if (treatmentBMPPoint != null)
            {
                TreatmentBMPPointX = treatmentBMPPoint.XCoordinate;
                TreatmentBMPPointY = treatmentBMPPoint.YCoordinate;
            }
            else
            {
                TreatmentBMPPointX = null;
                TreatmentBMPPointY = null;
            }

            Notes = treatmentBMP.Notes;
        }

        public void UpdateModel(Models.TreatmentBMP treatmentBMP, Person currentPerson)
        {
            treatmentBMP.TreatmentBMPName = TreatmentBMPName;
            treatmentBMP.LocationPoint = DbSpatialHelper.MakeDbGeometryFromCoordinates(TreatmentBMPPointX.Value, TreatmentBMPPointY.Value, MapInitJson.CoordinateSystemId);
            treatmentBMP.Notes = Notes;

            if (!ModelObjectHelpers.IsRealPrimaryKeyValue(treatmentBMP.TreatmentBMPID))
            {
                treatmentBMP.StormwaterJurisdictionID = StormwaterJurisdictionID;
                treatmentBMP.TreatmentBMPTypeID = TreatmentBMPTypeID;
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            var treatmentBMPsWithSameName = HttpRequestStorage.DatabaseEntities.TreatmentBMPs.Where(x => x.TreatmentBMPName == TreatmentBMPName);

            if (treatmentBMPsWithSameName.Any(x => x.TreatmentBMPID != TreatmentBMPID))
            {
                validationResults.Add(new SitkaValidationResult<EditViewModel, string>("A BMP with this name already exists.", x => x.TreatmentBMPName));
            }
            return validationResults;
        }

    }
}
