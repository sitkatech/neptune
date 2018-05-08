namespace Neptune.Web.Models
{
    public class TreatmentBMPTypeAttributeTypeSimple
    {
        public int TreatmentBMPTypeID { get; set; }
        public int CustomAttributeTypeID { get; set; }

        public TreatmentBMPTypeAttributeTypeSimple()
        {
        }

        public TreatmentBMPTypeAttributeTypeSimple(TreatmentBMPTypeAttributeType treatmentBMPTypeAttributeType)
        {
            TreatmentBMPTypeID = treatmentBMPTypeAttributeType.TreatmentBMPTypeID;
            CustomAttributeTypeID = treatmentBMPTypeAttributeType.CustomAttributeTypeID;
        }
    }
}