namespace Neptune.Web.Models
{
    public partial class TreatmentBMPDetailed
    {
        public TreatmentBMP TreatmentBMP { get; set; }
        public vTreatmentBMPDetailed vTreatmentBmpDetailed { get; set; }

        public TreatmentBMPDetailed(TreatmentBMP treatmentBMP, vTreatmentBMPDetailed vTreatmentBmpDetailed)
        {
            this.TreatmentBMP = treatmentBMP;
            this.vTreatmentBmpDetailed = vTreatmentBmpDetailed;
        }
    }
}