/*-----------------------------------------------------------------------
<copyright file="EditWqmpBoundaryViewModel.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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


using Neptune.Common.GeoSpatial;
using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Common.Models;
using Neptune.Web.Models;

namespace Neptune.Web.Views.WaterQualityManagementPlan
{
    public class EditWqmpBoundaryViewModel : FormViewModel
    {
        public List<WktAndAnnotation> WktAndAnnotations { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditWqmpBoundaryViewModel()
        {
        }

        public void UpdateModel(EFModels.Entities.WaterQualityManagementPlan waterQualityManagementPlan, NeptuneDbContext dbContext)
        {
            var newWaterQualityManagementPlanParcels = new List<WaterQualityManagementPlanParcel>();
            var waterQualityManagementPlanBoundary = waterQualityManagementPlan.WaterQualityManagementPlanBoundary;
            if (waterQualityManagementPlanBoundary == null)
            {
                waterQualityManagementPlanBoundary = new WaterQualityManagementPlanBoundary
                    { WaterQualityManagementPlan = waterQualityManagementPlan };
            }

            if (WktAndAnnotations != null)
            {
                var dbGeometries = WktAndAnnotations.Select(x =>
                    GeometryHelper.FromWKT(x.Wkt, Proj4NetHelper.WEB_MERCATOR).Buffer(0));
                var newGeometry4326 = dbGeometries.ToList().UnionListGeometries();
                newWaterQualityManagementPlanParcels = dbContext.ParcelGeometries
                    .Where(x => x.Geometry4326.Intersects(newGeometry4326))
                    .ToList()
                    .Select(x =>
                        new WaterQualityManagementPlanParcel
                        {
                            WaterQualityManagementPlanID = waterQualityManagementPlan.WaterQualityManagementPlanID, 
                            ParcelID = x.ParcelID
                        })
                    .ToList();

                // since this is coming from the browser, we have to transform to State Plane
                waterQualityManagementPlanBoundary.GeometryNative = newGeometry4326.ProjectTo2771();
                waterQualityManagementPlanBoundary.Geometry4326 = newGeometry4326;
            }

            waterQualityManagementPlan.WaterQualityManagementPlanParcels.Merge(
                newWaterQualityManagementPlanParcels,
                dbContext.WaterQualityManagementPlanParcels,
                (x, y) => x.WaterQualityManagementPlanParcelID == y.WaterQualityManagementPlanParcelID);

            if (WktAndAnnotations == null)
            {
                waterQualityManagementPlanBoundary.GeometryNative = null;
                waterQualityManagementPlanBoundary.Geometry4326 = null;
            }

            dbContext.SaveChanges();
            
        }
    }
}

