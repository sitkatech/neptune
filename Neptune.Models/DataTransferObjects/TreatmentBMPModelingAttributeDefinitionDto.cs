namespace Neptune.Models.DataTransferObjects;

public class TreatmentBMPModelingAttributeDefinitionDto
{
    public string ModelingAttributeName { get; set; }
    public string ModelingAttributeDescription { get; set; }
    public string? Units { get; set; }
    public TreatmentBMPModelingAttributeDefinitionDto(string modelingAttributeName, string modelingAttributeDescription, string? units)
    {
        ModelingAttributeName = modelingAttributeName;
        ModelingAttributeDescription = modelingAttributeDescription;
        Units = units;
    }

    public TreatmentBMPModelingAttributeDefinitionDto()
    {
    }
}