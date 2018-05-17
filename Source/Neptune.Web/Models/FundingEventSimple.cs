namespace Neptune.Web.Models
{
    public class FundingEventSimple
    {
        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public FundingEventSimple()
        {
        }

        public FundingEventSimple(FundingEvent fundingEvent)
            : this()
        {
            TreatmentBMPID = fundingEvent.TreatmentBMPID;
            FundingSourceID = fundingEvent.FundingSourceID;
            Amount = fundingEvent.Amount;
        }

        public FundingEvent ToFundingEvent()
        {
            return new FundingEvent(FundingSourceID, TreatmentBMPID){Amount = Amount};
        }

        public int TreatmentBMPID { get; set; }
        public int FundingSourceID { get; set; }
        public decimal? Amount { get; set; }

    }
}