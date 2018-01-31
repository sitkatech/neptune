namespace Neptune.Web.Models
{
    public class TreatmentBMPAttributeSimple
    {
        public int TreatmentBMPID { get; set; }
        public int TreatmentBMPAttributeTypeID { get; set; }
        public string TreatmentBMPAttributeValue { get; set; }

        public TreatmentBMPAttributeSimple()
        {
        }

        public TreatmentBMPAttributeSimple(TreatmentBMPAttribute treatmentBMPAttribute)
        {
            TreatmentBMPID = treatmentBMPAttribute.TreatmentBMPID;
            TreatmentBMPAttributeTypeID = treatmentBMPAttribute.TreatmentBMPAttributeTypeID;
            TreatmentBMPAttributeValue = treatmentBMPAttribute.TreatmentBMPAttributeValue;
        }
    }
}