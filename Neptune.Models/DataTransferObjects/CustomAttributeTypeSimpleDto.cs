namespace Neptune.Models.DataTransferObjects;

public partial class CustomAttributeTypeSimpleDto
{
    public string DataTypeDisplayName { get; set; }
    public string MeasurementUnitDisplayName { get; set; }
    public string Purpose { get; set; }
    public int? CustomAttributeTypeSortOrder { get; set; }
}