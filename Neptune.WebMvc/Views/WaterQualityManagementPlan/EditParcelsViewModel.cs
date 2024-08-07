﻿using System.ComponentModel;
using Neptune.Common;
using Neptune.Common.GeoSpatial;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common.Models;

namespace Neptune.WebMvc.Views.WaterQualityManagementPlan
{
    public class EditParcelsViewModel : FormViewModel
    {
        [DisplayName("Parcels")]
        public IEnumerable<int> ParcelIDs { get; set; }

        /// <summary>
        /// Needed by Model Binder
        /// </summary>
        public EditParcelsViewModel()
        {
        }

        public EditParcelsViewModel(List<int> parcelIDs)
        {
            ParcelIDs = parcelIDs;
        }

        public async Task UpdateModels(EFModels.Entities.WaterQualityManagementPlan waterQualityManagementPlan, NeptuneDbContext dbContext, List<WaterQualityManagementPlanParcel> waterQualityManagementPlanParcels, WaterQualityManagementPlanBoundary? waterQualityManagementPlanBoundary)
        {
            var newWaterQualityManagementPlanParcels = ParcelIDs?.Select(x => new WaterQualityManagementPlanParcel
            {
                WaterQualityManagementPlanID = waterQualityManagementPlan.WaterQualityManagementPlanID, ParcelID = x
            }).ToList() ?? new List<WaterQualityManagementPlanParcel>();

            waterQualityManagementPlanParcels.Merge(
                newWaterQualityManagementPlanParcels,
                dbContext.WaterQualityManagementPlanParcels,
                (x, y) => x.WaterQualityManagementPlanID == y.WaterQualityManagementPlanID && x.ParcelID == y.ParcelID);

            // update the cached total boundary
            if (waterQualityManagementPlanBoundary == null)
            {
                waterQualityManagementPlanBoundary = new WaterQualityManagementPlanBoundary
                    { WaterQualityManagementPlan = waterQualityManagementPlan };
                await dbContext.WaterQualityManagementPlanBoundaries.AddAsync(waterQualityManagementPlanBoundary);
            }

            if (ParcelIDs != null)
            {
                var geometryNative = ParcelGeometries.UnionAggregateByParcelIDs(dbContext, ParcelIDs);
                waterQualityManagementPlanBoundary.GeometryNative = geometryNative;
                waterQualityManagementPlanBoundary.Geometry4326 = geometryNative.ProjectTo4326();
            }
            else
            {
                waterQualityManagementPlanBoundary.GeometryNative = null;
                waterQualityManagementPlanBoundary.Geometry4326 = null;
            }
        }
    }
}
