namespace Neptune.Models.DataTransferObjects
{
    public class WaterQualityManagementPlanDocumentUpsertDto
    {
        public int WaterQualityManagementPlanID { get; set; }
        public int FileResourceID { get; set; }
        public string DisplayName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int WaterQualityManagementPlanDocumentTypeID { get; set; }
    }
}
