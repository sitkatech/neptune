using Neptune.Web.Models;

namespace Neptune.EFModels.Entities
{
    public partial class Parcel : IAuditableEntity
    {
        public bool HasValidAddress()
        {
            return !string.IsNullOrWhiteSpace(ParcelAddress);
        }

        public string GetParcelAddress()
        {
            return $"{ParcelAddress}{(!string.IsNullOrWhiteSpace(ParcelZipCode) ? $", {ParcelZipCode}" : "")}";
        }

        public string GetAuditDescriptionString()
        {
            return ParcelNumber;
        }

        public TrashCaptureStatusType GetTrashCaptureStatusType()
        {
            return WaterQualityManagementPlanParcels.Select(x => x.WaterQualityManagementPlan.TrashCaptureStatusType).MinBy(x => x.TrashCaptureStatusTypePriority) ??
                   TrashCaptureStatusType.NotProvided;
        }
    }
}
