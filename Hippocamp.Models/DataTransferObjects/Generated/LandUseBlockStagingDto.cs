//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[LandUseBlockStaging]
using System;


namespace Hippocamp.Models.DataTransferObjects
{
    public partial class LandUseBlockStagingDto
    {
        public int LandUseBlockStagingID { get; set; }
        public string PriorityLandUseType { get; set; }
        public string LandUseDescription { get; set; }
        public decimal? TrashGenerationRate { get; set; }
        public string LandUseForTGR { get; set; }
        public decimal? MedianHouseholdIncome { get; set; }
        public string StormwaterJurisdiction { get; set; }
        public string PermitType { get; set; }
        public PersonDto UploadedByPerson { get; set; }
    }

    public partial class LandUseBlockStagingSimpleDto
    {
        public int LandUseBlockStagingID { get; set; }
        public string PriorityLandUseType { get; set; }
        public string LandUseDescription { get; set; }
        public decimal? TrashGenerationRate { get; set; }
        public string LandUseForTGR { get; set; }
        public decimal? MedianHouseholdIncome { get; set; }
        public string StormwaterJurisdiction { get; set; }
        public string PermitType { get; set; }
        public int UploadedByPersonID { get; set; }
    }

}