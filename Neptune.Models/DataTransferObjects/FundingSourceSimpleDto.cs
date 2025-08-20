//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[FundingSource]

namespace Neptune.Models.DataTransferObjects
{
    public partial class FundingSourceSimpleDto
    {
        public int FundingSourceID { get; set; }
        public int OrganizationID { get; set; }
        public string FundingSourceName { get; set; }
        public bool IsActive { get; set; }
        public string FundingSourceDescription { get; set; }
    }
}