namespace Neptune.Models.DataTransferObjects
{
    public class WaterQualityManagementPlanDocumentDto
    {
        public int WaterQualityManagementPlanDocumentID { get; set; }
        public int WaterQualityManagementPlanID { get; set; }
        public string WaterQualityManagementPlanName { get; set; }
        public FileResourceDto FileResource { get; set; }
        public string DisplayName { get; set; }
        public string? Description { get; set; }
        public DateTime UploadDate { get; set; }
        public int WaterQualityManagementPlanDocumentTypeID { get; set; }
    }
}
