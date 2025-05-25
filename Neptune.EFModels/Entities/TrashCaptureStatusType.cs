namespace Neptune.EFModels.Entities
{
    public abstract partial class TrashCaptureStatusType
    {
        public string FeatureColorOnTrashModuleMap()
        {
            return $"#{TrashCaptureStatusTypeColorCode}";
        }
    }
}