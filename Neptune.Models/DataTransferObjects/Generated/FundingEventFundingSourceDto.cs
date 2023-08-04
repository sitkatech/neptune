//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[FundingEventFundingSource]
using System;


namespace Neptune.Models.DataTransferObjects
{
    public partial class FundingEventFundingSourceDto
    {
        public int FundingEventFundingSourceID { get; set; }
        public FundingSourceDto FundingSource { get; set; }
        public FundingEventDto FundingEvent { get; set; }
        public decimal? Amount { get; set; }
    }

    public partial class FundingEventFundingSourceSimpleDto
    {
        public int FundingEventFundingSourceID { get; set; }
        public System.Int32 FundingSourceID { get; set; }
        public System.Int32 FundingEventID { get; set; }
        public decimal? Amount { get; set; }
    }

}