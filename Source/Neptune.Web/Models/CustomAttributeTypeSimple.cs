namespace Neptune.Web.Models
{
    public class CustomAttributeTypeSimple
    {
        public int CustomAttributeTypeID { get; set; }
        public string CustomAttributeTypeName { get; set; }   
        public string DataTypeDisplayName { get; set; }   
        public string MeasurementUnitDisplayName { get; set; }   
        public bool IsRequired { get; set; }
        public string Description { get; set; }
        public string Purpose { get; set; }
        public int? CustomAttributeTypeSortOrder { get; set; }

        public CustomAttributeTypeSimple(CustomAttributeType customAttributeType)
        {
            CustomAttributeTypeID = customAttributeType.CustomAttributeTypeID;
            CustomAttributeTypeName = $"{customAttributeType.CustomAttributeTypeName}";
            DataTypeDisplayName = customAttributeType.CustomAttributeDataType.CustomAttributeDataTypeDisplayName;
            MeasurementUnitDisplayName = customAttributeType.GetMeasurementUnitDisplayName();
            IsRequired = customAttributeType.IsRequired;
            Description = customAttributeType.CustomAttributeTypeDescription;
            Purpose = customAttributeType.CustomAttributeTypePurpose
                .CustomAttributeTypePurposeDisplayName;
        }
    }
}