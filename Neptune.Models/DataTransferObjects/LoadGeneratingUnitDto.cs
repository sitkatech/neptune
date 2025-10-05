namespace Neptune.Models.DataTransferObjects
{
    public class LoadGeneratingUnitDto
    {
        public int LoadGeneratingUnitID { get; set; }
        public int? ModelBasinID { get; set; }
        public int? RegionalSubbasinID { get; set; }
        public int? DelineationID { get; set; }
        public int? WaterQualityManagementPlanID { get; set; }
        public bool? IsEmptyResponseFromHRUService { get; set; }
        public DateTime? DateHRURequested { get; set; }
        public int? HRULogID { get; set; }
        // Add more fields as needed for display or editing
    }
}
