﻿using LtInfo.Common.Models;

namespace Neptune.Web.Models
{
    public class FundingEventFundingSourceSimple
    {
        public int? FundingEventID { get; set; }
        public int FundingSourceID { get; set; }
        public decimal? Amount { get; set; }

        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public FundingEventFundingSourceSimple()
        {
        }

        public FundingEventFundingSourceSimple(FundingEventFundingSource fundingEventFundingSource)
            : this()
        {
            FundingEventID = fundingEventFundingSource.FundingEventID;
            FundingSourceID = fundingEventFundingSource.FundingSourceID;
            Amount = fundingEventFundingSource.Amount;
        }

        public FundingEventFundingSource ToFundingEventFundingSource()
        {
            return new FundingEventFundingSource(FundingSourceID, FundingEventID ?? ModelObjectHelpers.NotYetAssignedID) {Amount = Amount};
        }
    }
}
