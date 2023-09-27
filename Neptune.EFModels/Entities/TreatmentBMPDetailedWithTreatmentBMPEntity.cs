namespace Neptune.EFModels.Entities
{
    public class TreatmentBMPDetailedWithTreatmentBMPEntity
    {
        public TreatmentBMP TreatmentBMP { get; set; }
        public vTreatmentBMPDetailed TreatmentBMPDetailed { get; set; }

        public TreatmentBMPDetailedWithTreatmentBMPEntity(TreatmentBMP treatmentBMP, vTreatmentBMPDetailed treatmentBMPDetailed)
        {
            TreatmentBMP = treatmentBMP;
            TreatmentBMPDetailed = treatmentBMPDetailed;
        }
    }
}