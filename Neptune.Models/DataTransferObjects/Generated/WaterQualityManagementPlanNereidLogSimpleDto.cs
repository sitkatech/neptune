//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanNereidLog]

namespace Neptune.Models.DataTransferObjects
{
    public partial class WaterQualityManagementPlanNereidLogSimpleDto
    {
        public int WaterQualityManagementPlanNereidLogID { get; set; }
        public int WaterQualityManagementPlanID { get; set; }
        public DateTime? LastRequestDate { get; set; }
        public string NereidRequest { get; set; }
        public string NereidResponse { get; set; }
    }
}