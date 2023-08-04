//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[CustomAttributeType]
using System;


namespace Neptune.Models.DataTransferObjects
{
    public partial class CustomAttributeTypeDto
    {
        public int CustomAttributeTypeID { get; set; }
        public string CustomAttributeTypeName { get; set; }
        public CustomAttributeDataTypeDto CustomAttributeDataType { get; set; }
        public MeasurementUnitTypeDto MeasurementUnitType { get; set; }
        public bool IsRequired { get; set; }
        public string CustomAttributeTypeDescription { get; set; }
        public CustomAttributeTypePurposeDto CustomAttributeTypePurpose { get; set; }
        public string CustomAttributeTypeOptionsSchema { get; set; }
    }

    public partial class CustomAttributeTypeSimpleDto
    {
        public int CustomAttributeTypeID { get; set; }
        public string CustomAttributeTypeName { get; set; }
        public System.Int32 CustomAttributeDataTypeID { get; set; }
        public System.Int32? MeasurementUnitTypeID { get; set; }
        public bool IsRequired { get; set; }
        public string CustomAttributeTypeDescription { get; set; }
        public System.Int32 CustomAttributeTypePurposeID { get; set; }
        public string CustomAttributeTypeOptionsSchema { get; set; }
    }

}