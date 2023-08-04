//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[FundingEvent]
using System;


namespace Neptune.Models.DataTransferObjects
{
    public partial class FundingEventDto
    {
        public int FundingEventID { get; set; }
        public TreatmentBMPDto TreatmentBMP { get; set; }
        public FundingEventTypeDto FundingEventType { get; set; }
        public int Year { get; set; }
        public string Description { get; set; }
    }

    public partial class FundingEventSimpleDto
    {
        public int FundingEventID { get; set; }
        public System.Int32 TreatmentBMPID { get; set; }
        public System.Int32 FundingEventTypeID { get; set; }
        public int Year { get; set; }
        public string Description { get; set; }
    }

}