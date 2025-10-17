
namespace Neptune.Models.DataTransferObjects
{
    public class CustomAttributeUpsertDto
    {
        public int? CustomAttributeID { get; set; }
        public int? TreatmentBMPID { get; set; }
        public int? TreatmentBMPTypeID { get; set; }
        public int TreatmentBMPTypeCustomAttributeTypeID { get; set; }
        public int CustomAttributeTypeID { get; set; }
        public List<string>? CustomAttributeValues { get; set; }

        public CustomAttributeUpsertDto()
        {
        }

        public CustomAttributeUpsertDto(int treatmentBMPTypeCustomAttributeTypeID, int customAttributeTypeID, List<string> values, int? customAttributeID = null, int? treatmentBMPID = null, int? treatmentBMPTypeID = null)
        {
            TreatmentBMPTypeCustomAttributeTypeID = treatmentBMPTypeCustomAttributeTypeID;
            CustomAttributeTypeID = customAttributeTypeID;
            CustomAttributeValues = values;
            CustomAttributeID = customAttributeID;
            TreatmentBMPID = treatmentBMPID;
            TreatmentBMPTypeID = treatmentBMPTypeID;
        }
    }
}