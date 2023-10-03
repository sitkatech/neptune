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
using Neptune.Common.GeoSpatial;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Models;
using Neptune.WebMvc.Views.FieldVisit;

namespace Neptune.WebMvc.Views.Shared.Location
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

        public virtual void UpdateModel(NeptuneDbContext dbContext, EFModels.Entities.TreatmentBMP treatmentBMP,
            Person currentPerson, EFModels.Entities.Delineation? delineation)
        {
            SetTreatmentBMPLocationAndPointInPolygonData(dbContext, treatmentBMP);

            treatmentBMP.UpdateUpstreamBMPReferencesIfNecessary(dbContext);
            treatmentBMP.UpdatedCentralizedBMPDelineationIfPresent(dbContext, delineation);
        }

        protected void SetTreatmentBMPLocationAndPointInPolygonData(NeptuneDbContext dbContext, EFModels.Entities.TreatmentBMP treatmentBMP)
        {
            var locationPoint4326 =
                GeometryHelper.CreateLocationPoint4326FromLatLong(TreatmentBMPPointY.Value,
                    TreatmentBMPPointX.GetValueOrDefault());
            var locationPoint = locationPoint4326.ProjectTo2771();
            treatmentBMP.LocationPoint = locationPoint;
            treatmentBMP.LocationPoint4326 = locationPoint4326;
            // associate watershed, model basin, precipitation zone
            treatmentBMP.SetTreatmentBMPPointInPolygonDataByLocationPoint(locationPoint, dbContext);
        }
    }
}
