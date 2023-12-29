namespace Neptune.Models.DataTransferObjects;

public class TreatmentBMPTypeWithModelingAttributesDto
{
    public int TreatmentBMPTypeID { get; set; }
    public string TreatmentBMPTypeName { get; set; }
    public int? TreatmentBMPModelingTypeID { get; set; }
    public List<TreatmentBMPModelingAttributeDefinitionDto> TreatmentBMPModelingAttributes { get; set; }
}
