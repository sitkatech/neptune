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


using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using LtInfo.Common;
using LtInfo.Common.DbSpatial;
using LtInfo.Common.Models;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Views.Shared;

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

        public EditWqmpBoundaryViewModel(Models.WaterQualityManagementPlan waterQualityManagementPlan)
        {
        }

        public void UpdateModel(Models.WaterQualityManagementPlan waterQualityManagementPlan)
        {
            var newWaterQualityManagementPlanParcels = new List<WaterQualityManagementPlanParcel>();

            if (WktAndAnnotations != null)
            {
                var dbGeometries = WktAndAnnotations.Select(x =>
                    DbGeometry.FromText(x.Wkt, CoordinateSystemHelper.NAD_83_HARN_CA_ZONE_VI_SRID).ToSqlGeometry().MakeValid()
                        .ToDbGeometry());
                var newGeometry4326 = dbGeometries.ToList().UnionListGeometries().FixSrid(CoordinateSystemHelper.WGS_1984_SRID);
                newWaterQualityManagementPlanParcels = HttpRequestStorage.DatabaseEntities.Parcels
                    .Where(x => x.ParcelGeometry4326.Intersects(newGeometry4326))
                    .ToList()
                    .Select(x =>
                        new WaterQualityManagementPlanParcel(waterQualityManagementPlan.WaterQualityManagementPlanID, x.ParcelID))
                    .ToList();

                // since this is coming from the browser, we have to transform to State Plane
                waterQualityManagementPlan.WaterQualityManagementPlanBoundary =
                    CoordinateSystemHelper.ProjectWebMercatorToCaliforniaStatePlaneVI(newGeometry4326);

                waterQualityManagementPlan.WaterQualityManagementPlanBoundary4326 =
                    newGeometry4326;
            }

            waterQualityManagementPlan.WaterQualityManagementPlanParcels.Merge(
                newWaterQualityManagementPlanParcels,
                HttpRequestStorage.DatabaseEntities.WaterQualityManagementPlanParcels.Local,
                (x, y) => x.WaterQualityManagementPlanParcelID == y.WaterQualityManagementPlanParcelID);

            if (WktAndAnnotations == null)
            {
                waterQualityManagementPlan.WaterQualityManagementPlanBoundary = null;
                waterQualityManagementPlan.WaterQualityManagementPlanBoundary4326 = null;
            }

            HttpRequestStorage.DatabaseEntities.SaveChanges();
            
        }
    }
}

