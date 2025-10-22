using System.Collections.Generic;

namespace Neptune.Models.DataTransferObjects
{
    public class CustomAttributeDto
    {
        public int CustomAttributeID { get; set; }
        public int TreatmentBMPID { get; set; }
        public int TreatmentBMPTypeCustomAttributeTypeID { get; set; }
        public int TreatmentBMPTypeID { get; set; }
        public int CustomAttributeTypeID { get; set; }
        public string CustomAttributeValueWithUnits { get; set; }
    }
}
