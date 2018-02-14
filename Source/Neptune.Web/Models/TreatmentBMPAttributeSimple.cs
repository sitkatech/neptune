namespace Neptune.Web.Models
{
    public class TreatmentBMPAttributeSimple
    {
        public int TreatmentBMPTypeAttributeTypeID { get; set; }
        public int TreatmentBMPAttributeTypeID { get; set; }
        public string TreatmentBMPAttributeValue { get; set; }

        public TreatmentBMPAttributeSimple()
        {
        }

        public TreatmentBMPAttributeSimple(TreatmentBMPAttribute treatmentBMPAttribute)
        {
            TreatmentBMPTypeAttributeTypeID = treatmentBMPAttribute.TreatmentBMPTypeAttributeTypeID;
            TreatmentBMPAttributeTypeID = treatmentBMPAttribute.TreatmentBMPAttributeTypeID;
            TreatmentBMPAttributeValue = treatmentBMPAttribute.TreatmentBMPAttributeValue;
        }
    }
}