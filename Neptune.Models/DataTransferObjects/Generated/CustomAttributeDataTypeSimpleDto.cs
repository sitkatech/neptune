//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[CustomAttributeDataType]

namespace Neptune.Models.DataTransferObjects
{
    public partial class CustomAttributeDataTypeSimpleDto
    {
        public int CustomAttributeDataTypeID { get; set; }
        public string CustomAttributeDataTypeName { get; set; }
        public string CustomAttributeDataTypeDisplayName { get; set; }
        public bool HasOptions { get; set; }
        public bool HasMeasurementUnit { get; set; }
    }
}