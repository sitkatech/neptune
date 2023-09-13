namespace Neptune.EFModels.Entities
{
    public partial class Parcel
    {
        public bool HasValidAddress()
        {
            return !string.IsNullOrWhiteSpace(ParcelAddress);
        }

        public string GetParcelAddress()
        {
            return $"{ParcelAddress}{(!string.IsNullOrWhiteSpace(ParcelZipCode) ? $", {ParcelZipCode}" : "")}";
        }

        public TrashCaptureStatusType GetTrashCaptureStatusType()
        {
            return WaterQualityManagementPlanParcels.Select(x => x.WaterQualityManagementPlan.TrashCaptureStatusType).MinBy(x => x.TrashCaptureStatusTypePriority) ??
                   TrashCaptureStatusType.NotProvided;
        }
    }
}
