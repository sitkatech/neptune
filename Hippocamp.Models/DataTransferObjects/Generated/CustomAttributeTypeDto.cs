//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[CustomAttributeType]
using System;


namespace Hippocamp.Models.DataTransferObjects
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
        public int CustomAttributeDataTypeID { get; set; }
        public int? MeasurementUnitTypeID { get; set; }
        public bool IsRequired { get; set; }
        public string CustomAttributeTypeDescription { get; set; }
        public int CustomAttributeTypePurposeID { get; set; }
        public string CustomAttributeTypeOptionsSchema { get; set; }
    }

}