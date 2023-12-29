//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[LandUseBlock]

namespace Neptune.Models.DataTransferObjects
{
    public partial class LandUseBlockSimpleDto
    {
        public int LandUseBlockID { get; set; }
        public int? PriorityLandUseTypeID { get; set; }
        public string LandUseDescription { get; set; }
        public decimal? TrashGenerationRate { get; set; }
        public string LandUseForTGR { get; set; }
        public decimal? MedianHouseholdIncomeResidential { get; set; }
        public decimal? MedianHouseholdIncomeRetail { get; set; }
        public int StormwaterJurisdictionID { get; set; }
        public int PermitTypeID { get; set; }
    }
}