/*-----------------------------------------------------------------------
<copyright file="EditLocationViewModel.cs" company="Tahoe Regional Planning Agency">
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

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Spatial;
using LtInfo.Common.DbSpatial;
using LtInfo.Common.Models;
using Neptune.Web.Models;

namespace Neptune.Web.Views.Shared.Location
{
    public class EditLocationViewModel : FormViewModel
    {
        [Required(ErrorMessage =  "Must specify a BMP Longitude")]
        [DisplayName("Longitude")]
        [Range(-180, 180, ErrorMessage = "Longitude must be between -180 and 180")]
        public double? TreatmentBMPPointX { get; set; }

        [Required(ErrorMessage = "Must specify a BMP Latitude")]
        [DisplayName("Latitude")]
        [Range(-90, 90, ErrorMessage = "Latitude must be between -90 and 90")]
        public double? TreatmentBMPPointY { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditLocationViewModel()
        {
        }

        public EditLocationViewModel(Models.TreatmentBMP treatmentBMP, DbGeometry treatmentBMPPoint)
        {
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
        }

        public void UpdateModel(Models.TreatmentBMP treatmentBMP, Person currentPerson)
        {
            treatmentBMP.LocationPoint = DbSpatialHelper.MakeDbGeometryFromCoordinates(TreatmentBMPPointX.Value, TreatmentBMPPointY.Value, MapInitJson.CoordinateSystemId);
        }

    }
}
