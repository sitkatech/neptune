//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[LoadGeneratingUnit4326]

namespace Neptune.Models.DataTransferObjects
{
    public partial class LoadGeneratingUnit4326SimpleDto
    {
        public int LoadGeneratingUnit4326ID { get; set; }
        public int? ModelBasinID { get; set; }
        public int? RegionalSubbasinID { get; set; }
        public int? DelineationID { get; set; }
        public int? WaterQualityManagementPlanID { get; set; }
        public bool? IsEmptyResponseFromHRUService { get; set; }
        public DateTime? DateHRURequested { get; set; }
    }
}