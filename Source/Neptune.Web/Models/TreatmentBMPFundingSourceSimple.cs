namespace Neptune.Web.Models
{
    public class TreatmentBMPFundingSourceSimple
    {
        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public TreatmentBMPFundingSourceSimple()
        {
        }

        public TreatmentBMPFundingSourceSimple(TreatmentBMPFundingSource treatmentBMPFundingSource)
            : this()
        {
            TreatmentBMPID = treatmentBMPFundingSource.TreatmentBMPID;
            FundingSourceID = treatmentBMPFundingSource.FundingSourceID;
            Amount = treatmentBMPFundingSource.Amount;
        }

        public TreatmentBMPFundingSource ToTreatmentBMPFundingSource()
        {
            return new TreatmentBMPFundingSource(FundingSourceID, TreatmentBMPID, Amount ?? 0);
        }

        public int TreatmentBMPID { get; set; }
        public int FundingSourceID { get; set; }
        public decimal? Amount { get; set; }

    }
}