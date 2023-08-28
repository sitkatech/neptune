namespace Neptune.EFModels.Entities
{
    public class vTreatmentBMPDetailedWithTreatmentBMPEntity
    {
        public TreatmentBMP TreatmentBMP { get; set; }
        public vTreatmentBMPDetailed vTreatmentBMPDetailed { get; set; }

        public vTreatmentBMPDetailedWithTreatmentBMPEntity(TreatmentBMP treatmentBMP, vTreatmentBMPDetailed vTreatmentBmpDetailed)
        {
            TreatmentBMP = treatmentBMP;
            vTreatmentBMPDetailed = vTreatmentBmpDetailed;
        }
    }
}