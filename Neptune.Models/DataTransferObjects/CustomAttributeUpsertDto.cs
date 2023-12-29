
namespace Neptune.Models.DataTransferObjects
{
    public class CustomAttributeUpsertDto
    {
        public int TreatmentBMPTypeCustomAttributeTypeID { get; set; }
        public int CustomAttributeTypeID { get; set; }
        public List<string>? CustomAttributeValues { get; set; }

        public CustomAttributeUpsertDto()
        {
        }

        public CustomAttributeUpsertDto(int treatmentBMPTypeCustomAttributeTypeID, int customAttributeTypeID, List<string> values)
        {
            TreatmentBMPTypeCustomAttributeTypeID = treatmentBMPTypeCustomAttributeTypeID;
            CustomAttributeTypeID = customAttributeTypeID;
            CustomAttributeValues = values;
        }
    }
}