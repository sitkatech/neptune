using System.Linq;

namespace Neptune.Web.Models
{
    public partial class Parcel : IAuditableEntity
    {
        public bool HasValidAddress()
        {
            return !(string.IsNullOrWhiteSpace(ParcelAddress) || string.IsNullOrWhiteSpace(ParcelZipCode));
        }

        public string GetParcelAddress()
        {
            return $"{ParcelAddress}, {ParcelZipCode}";
        }

        public string GetAuditDescriptionString()
        {
            return ParcelNumber;
        }

        public TrashCaptureStatusType GetTrashCaptureStatusType()
        {
            return WaterQualityManagementPlanParcels.Select(x => x.WaterQualityManagementPlan.TrashCaptureStatusType)
                       .Distinct().OrderBy(x => x.TrashCaptureStatusTypePriority).FirstOrDefault() ??
                   TrashCaptureStatusType.NotProvided;
        }
    }
}
