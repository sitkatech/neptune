using System.Collections.Generic;

namespace Neptune.Web.Models
{
    public class TreatmentBMPAttributeSimple
    {
        public int TreatmentBMPTypeAttributeTypeID { get; set; }
        public int TreatmentBMPAttributeTypeID { get; set; }
        public List<string> TreatmentBMPAttributeValues { get; set; }

        public TreatmentBMPAttributeSimple()
        {
        }

        public TreatmentBMPAttributeSimple(TreatmentBMPAttribute treatmentBMPAttribute)
        {
            TreatmentBMPTypeAttributeTypeID = treatmentBMPAttribute.TreatmentBMPTypeAttributeTypeID;
            TreatmentBMPAttributeTypeID = treatmentBMPAttribute.TreatmentBMPAttributeTypeID;
            TreatmentBMPAttributeValues = new List<string> { treatmentBMPAttribute.TreatmentBMPAttributeValue };
        }

        public TreatmentBMPAttributeSimple(int treatmentBMPTypeAttributeTypeID, int treatmentBMPAttributeTypeID, List<string> values)
        {
            TreatmentBMPTypeAttributeTypeID = treatmentBMPTypeAttributeTypeID;
            TreatmentBMPAttributeTypeID = treatmentBMPAttributeTypeID;
            TreatmentBMPAttributeValues = values;
        }
    }
   
}