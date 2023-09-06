using Neptune.Web.Models;

namespace Neptune.EFModels.Entities
{
    public partial class WaterQualityManagementPlanParcel : IAuditableEntity
    {
        public string GetAuditDescriptionString()
        {
            return "WQMP-Parcel (" +
                   $"WQMP: \"{WaterQualityManagementPlanID}\", " +
                   $"Parcel: \"{ParcelID}\")";
        }
    }
}
