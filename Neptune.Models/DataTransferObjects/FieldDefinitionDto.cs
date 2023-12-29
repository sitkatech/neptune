namespace Neptune.Models.DataTransferObjects;

public class FieldDefinitionDto
{
    public int FieldDefinitionID { get; set; }
    public FieldDefinitionTypeSimpleDto FieldDefinitionType { get; set; }
    public string FieldDefinitionValue { get; set; }
}