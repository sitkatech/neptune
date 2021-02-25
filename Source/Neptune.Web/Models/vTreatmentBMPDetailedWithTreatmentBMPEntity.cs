namespace Neptune.Web.Models
{
    public partial class vTreatmentBMPDetailedWithTreatmentBMPEntity
    {
        public TreatmentBMP TreatmentBMP { get; set; }
        public vTreatmentBMPDetailed vTreatmentBmpDetailed { get; set; }

        public vTreatmentBMPDetailedWithTreatmentBMPEntity(TreatmentBMP treatmentBMP, vTreatmentBMPDetailed vTreatmentBmpDetailed)
        {
            this.TreatmentBMP = treatmentBMP;
            this.vTreatmentBmpDetailed = vTreatmentBmpDetailed;
        }
    }
}