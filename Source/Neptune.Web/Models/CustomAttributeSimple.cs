using System.Collections.Generic;
using System.Linq;

namespace Neptune.Web.Models
{
    public class CustomAttributeSimple
    {
        public int TreatmentBMPTypeAttributeTypeID { get; set; }
        public int CustomAttributeTypeID { get; set; }
        public List<string> CustomAttributeValues { get; set; }

        public CustomAttributeSimple()
        {
        }

        public CustomAttributeSimple(CustomAttribute customAttribute)
        {
            TreatmentBMPTypeAttributeTypeID = customAttribute.TreatmentBMPTypeAttributeTypeID;
            CustomAttributeTypeID = customAttribute.CustomAttributeTypeID;
            CustomAttributeValues = customAttribute.CustomAttributeValues.Select(x => x.AttributeValue).ToList();
        }

        public CustomAttributeSimple(int treatmentBMPTypeAttributeTypeID, int customAttributeTypeID, List<string> values)
        {
            TreatmentBMPTypeAttributeTypeID = treatmentBMPTypeAttributeTypeID;
            CustomAttributeTypeID = customAttributeTypeID;
            CustomAttributeValues = values;
        }
    }
   
}