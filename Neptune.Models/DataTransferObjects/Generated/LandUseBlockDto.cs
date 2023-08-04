//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[LandUseBlock]
using System;


namespace Neptune.Models.DataTransferObjects
{
    public partial class LandUseBlockDto
    {
        public int LandUseBlockID { get; set; }
        public PriorityLandUseTypeDto PriorityLandUseType { get; set; }
        public string LandUseDescription { get; set; }
        public decimal? TrashGenerationRate { get; set; }
        public string LandUseForTGR { get; set; }
        public decimal? MedianHouseholdIncomeResidential { get; set; }
        public decimal? MedianHouseholdIncomeRetail { get; set; }
        public StormwaterJurisdictionDto StormwaterJurisdiction { get; set; }
        public PermitTypeDto PermitType { get; set; }
    }

    public partial class LandUseBlockSimpleDto
    {
        public int LandUseBlockID { get; set; }
        public System.Int32? PriorityLandUseTypeID { get; set; }
        public string LandUseDescription { get; set; }
        public decimal? TrashGenerationRate { get; set; }
        public string LandUseForTGR { get; set; }
        public decimal? MedianHouseholdIncomeResidential { get; set; }
        public decimal? MedianHouseholdIncomeRetail { get; set; }
        public System.Int32 StormwaterJurisdictionID { get; set; }
        public System.Int32 PermitTypeID { get; set; }
    }

}