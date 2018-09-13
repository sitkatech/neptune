﻿/*-----------------------------------------------------------------------
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
using LtInfo.Common.DbSpatial;
using LtInfo.Common.Models;
using Neptune.Web.Models;
using Neptune.Web.Views.FieldVisit;

namespace Neptune.Web.Views.Shared.Location
{
    public class EditLocationViewModel : FieldVisitViewModel
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

        public virtual void UpdateModel(Models.TreatmentBMP treatmentBMP, Person currentPerson)
        {
            // note that these nullables will never be null due to the Required attribute
            treatmentBMP.LocationPoint = DbSpatialHelper.MakeDbGeometryFromCoordinates(TreatmentBMPPointX.GetValueOrDefault(), TreatmentBMPPointY.GetValueOrDefault(), MapInitJson.CoordinateSystemId);
        }

    }
}
