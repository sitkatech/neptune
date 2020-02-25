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

        public EditWqmpParcelsViewModel(Models.WaterQualityManagementPlan waterQualityManagementPlan)
        {
            ParcelIDs = waterQualityManagementPlan.WaterQualityManagementPlanParcels.Select(x => x.ParcelID).ToList();
        }

        public void UpdateModels(Models.WaterQualityManagementPlan waterQualityManagementPlan)
        {
            var newWaterQualityManagementPlanParcels = ParcelIDs?.Select(x => new WaterQualityManagementPlanParcel(waterQualityManagementPlan.WaterQualityManagementPlanID, x)).ToList() ?? new List<WaterQualityManagementPlanParcel>();

            waterQualityManagementPlan.WaterQualityManagementPlanParcels.Merge(
                newWaterQualityManagementPlanParcels,
                HttpRequestStorage.DatabaseEntities.WaterQualityManagementPlanParcels.Local,
                (x, y) => x.WaterQualityManagementPlanParcelID == y.WaterQualityManagementPlanParcelID);

            // update the cached total boundary
            waterQualityManagementPlan.WaterQualityManagementPlanBoundary = (ParcelIDs != null ? HttpRequestStorage.DatabaseEntities.Parcels
                .Where(x => ParcelIDs.Contains(x.ParcelID)).Select(x => x.ParcelGeometry).ToList() : new List<DbGeometry>())
                .UnionListGeometries().FixSrid(CoordinateSystemHelper.NAD_83_HARN_CA_ZONE_VI_SRID);
        }
    }
}
