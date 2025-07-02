namespace Neptune.Models.DataTransferObjects;

public class TreatmentBMPTypeCustomAttributeTypeDto
{
    public int TreatmentBMPTypeCustomAttributeTypeID { get; set; }
    public int TreatmentBMPTypeID { get; set; }
    public int CustomAttributeTypeID { get; set; }
    public CustomAttributeTypeDto CustomAttributeType { get; set; }
    public int? SortOrder { get; set; }
}