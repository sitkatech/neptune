using System.Collections.Generic;
using System.Linq;
using LtInfo.Common.Models;

namespace Neptune.Web.Models
{
    public class FundingEventSimple
    {
        public int TreatmentBMPID { get; set; }
        public int Year { get; set; }
        public int FundingEventTypeID { get; set; }
        public string Description { get; set; }
        public int? FundingEventID { get; set; }
        public List<FundingEventFundingSourceSimple> FundingEventFundingSources { get; set; }


        public FundingEventSimple()
        {
        }

        public FundingEventSimple(FundingEvent fundingEvent)
        {
            TreatmentBMPID = fundingEvent.TreatmentBMPID;
            Year = fundingEvent.Year;
            FundingEventTypeID = fundingEvent.FundingEventTypeID;
            Description = fundingEvent.Description;
            FundingEventFundingSources = fundingEvent.FundingEventFundingSources
                .Select(x => new FundingEventFundingSourceSimple(x)).ToList();
            FundingEventID = fundingEvent.FundingEventID;
        }

        public FundingEvent ToFundingEvent()
        {
            return new FundingEvent(FundingEventID ?? ModelObjectHelpers.NotYetAssignedID, TreatmentBMPID,
                FundingEventTypeID, Year, Description)
            {
                FundingEventFundingSources =
                    FundingEventFundingSources.Select(x => x.ToFundingEventFundingSource()).ToList()
            };
        }
    }
}
