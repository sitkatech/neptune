using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using LtInfo.Common;
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
                HttpRequestStorage.DatabaseEntities.AllWaterQualityManagementPlanParcels.Local,
                (x, y) => x.WaterQualityManagementPlanParcelID == y.WaterQualityManagementPlanParcelID);
        }
    }
}
