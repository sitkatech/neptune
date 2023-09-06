using System.ComponentModel;
using Neptune.Common.GeoSpatial;
using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Common.Models;

namespace Neptune.Web.Views.WaterQualityManagementPlan
{
    public class EditWqmpParcelsViewModel : FormViewModel
    {
        [DisplayName("Parcels")]
        public IEnumerable<int> ParcelIDs { get; set; }

        /// <summary>
        /// Needed by Model Binder
        /// </summary>
        public EditWqmpParcelsViewModel()
        {
        }

        public EditWqmpParcelsViewModel(EFModels.Entities.WaterQualityManagementPlan waterQualityManagementPlan)
        {
            ParcelIDs = waterQualityManagementPlan.WaterQualityManagementPlanParcels.Select(x => x.ParcelID).ToList();
        }

        public void UpdateModels(EFModels.Entities.WaterQualityManagementPlan waterQualityManagementPlan, NeptuneDbContext dbContext)
        {
            var newWaterQualityManagementPlanParcels = ParcelIDs?.Select(x => new WaterQualityManagementPlanParcel
            {
                WaterQualityManagementPlanID = waterQualityManagementPlan.WaterQualityManagementPlanID, ParcelID = x
            }).ToList() ?? new List<WaterQualityManagementPlanParcel>();

            waterQualityManagementPlan.WaterQualityManagementPlanParcels.Merge(
                newWaterQualityManagementPlanParcels,
                dbContext.WaterQualityManagementPlanParcels,
                (x, y) => x.WaterQualityManagementPlanParcelID == y.WaterQualityManagementPlanParcelID);

            // update the cached total boundary
            var waterQualityManagementPlanBoundary = waterQualityManagementPlan.WaterQualityManagementPlanBoundary;
            if (waterQualityManagementPlanBoundary == null)
            {
                waterQualityManagementPlanBoundary = new WaterQualityManagementPlanBoundary
                    { WaterQualityManagementPlan = waterQualityManagementPlan };
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
