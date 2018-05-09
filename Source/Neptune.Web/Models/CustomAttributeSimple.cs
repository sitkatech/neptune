using System.Collections.Generic;
using System.Linq;

namespace Neptune.Web.Models
{
    public class CustomAttributeSimple
    {
        public int TreatmentBMPTypeCustomAttributeTypeID { get; set; }
        public int CustomAttributeTypeID { get; set; }
        public List<string> CustomAttributeValues { get; set; }

        public CustomAttributeSimple()
        {
        }

        public CustomAttributeSimple(CustomAttribute customAttribute)
        {
            TreatmentBMPTypeCustomAttributeTypeID = customAttribute.TreatmentBMPTypeCustomAttributeTypeID;
            CustomAttributeTypeID = customAttribute.CustomAttributeTypeID;
            CustomAttributeValues = customAttribute.CustomAttributeValues.Select(x => x.AttributeValue).ToList();
        }

        public CustomAttributeSimple(int treatmentBMPTypeCustomAttributeTypeID, int customAttributeTypeID, List<string> values)
        {
            TreatmentBMPTypeCustomAttributeTypeID = treatmentBMPTypeCustomAttributeTypeID;
            CustomAttributeTypeID = customAttributeTypeID;
            CustomAttributeValues = values;
        }
    }
   
}