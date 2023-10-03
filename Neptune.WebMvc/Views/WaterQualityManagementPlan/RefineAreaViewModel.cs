/*-----------------------------------------------------------------------
<copyright file="RefineAreaViewModel.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
Copyright (c) Tahoe Regional Planning Agency and Sitka Technology Group. All rights reserved.
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


using Neptune.Common;
using Neptune.Common.GeoSpatial;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common.Models;
using Neptune.WebMvc.Models;

namespace Neptune.WebMvc.Views.WaterQualityManagementPlan
{
    public class RefineAreaViewModel : FormViewModel
    {
        public List<WktAndAnnotation> WktAndAnnotations { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public RefineAreaViewModel()
        {
        }

        public void UpdateModel(EFModels.Entities.WaterQualityManagementPlan waterQualityManagementPlan, NeptuneDbContext dbContext, WaterQualityManagementPlanBoundary? waterQualityManagementPlanBoundary)
        {
            if (waterQualityManagementPlanBoundary == null)
            {
                waterQualityManagementPlanBoundary = new WaterQualityManagementPlanBoundary
                    { WaterQualityManagementPlan = waterQualityManagementPlan };
            }

            if (WktAndAnnotations != null)
            {
                var dbGeometries = WktAndAnnotations.Select(x => GeometryHelper.FromWKT(x.Wkt, Proj4NetHelper.WEB_MERCATOR).Buffer(0));
                var newGeometry4326 = dbGeometries.ToList().UnionListGeometries();
                // since this is coming from the browser, we have to transform to State Plane
                waterQualityManagementPlanBoundary.GeometryNative = newGeometry4326.ProjectTo2771();
                waterQualityManagementPlanBoundary.Geometry4326 = newGeometry4326;
            }

            if (WktAndAnnotations == null)
            {
                waterQualityManagementPlanBoundary.GeometryNative = null;
                waterQualityManagementPlanBoundary.Geometry4326 = null;
            }
        }
    }
}

