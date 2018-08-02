using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public partial class WaterQualityManagementPlanParcel : IAuditableEntity
    {
        public string GetAuditDescriptionString()
        {
            var wqmp = WaterQualityManagementPlan ??
                       HttpRequestStorage.DatabaseEntities.WaterQualityManagementPlans
                           .GetWaterQualityManagementPlan(WaterQualityManagementPlanID);
            var parcel = Parcel ?? HttpRequestStorage.DatabaseEntities.Parcels.GetParcel(ParcelID);
            return "WQMP-Parcel (" +
                   $"WQMP: \"{wqmp?.WaterQualityManagementPlanName ?? "<Not Found>"}\", " +
                   $"Parcel: \"{parcel?.ParcelNumber ?? "<Not Found>"}\")";
        }
    }
}
