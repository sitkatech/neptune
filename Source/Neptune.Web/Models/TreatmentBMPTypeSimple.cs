namespace Neptune.Web.Models
{
    public class TreatmentBMPTypeSimple
    {
        public int TreatmentBMPTypeID { get; }
        public string TreatmentBMPTypeName { get; }

        public TreatmentBMPTypeSimple(TreatmentBMPType treatmentBMPType)
        {
            TreatmentBMPTypeID = treatmentBMPType.TreatmentBMPTypeID;
            TreatmentBMPTypeName = treatmentBMPType.TreatmentBMPTypeName;
        }
    }
}