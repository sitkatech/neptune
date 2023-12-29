namespace Neptune.Models.DataTransferObjects;

public class TreatmentBMPModelingAttributeDefinitionDto
{
    public string ModelingAttributeName { get; set; }
    public string? Units { get; set; }
    public TreatmentBMPModelingAttributeDefinitionDto(string modelingAttributeName, string? units)
    {
        ModelingAttributeName = modelingAttributeName;
        Units = units;
    }

    public TreatmentBMPModelingAttributeDefinitionDto()
    {
    }
}