//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[CustomAttributeValue]
using System;


namespace Hippocamp.Models.DataTransferObjects
{
    public partial class CustomAttributeValueDto
    {
        public int CustomAttributeValueID { get; set; }
        public CustomAttributeDto CustomAttribute { get; set; }
        public string AttributeValue { get; set; }
    }

    public partial class CustomAttributeValueSimpleDto
    {
        public int CustomAttributeValueID { get; set; }
        public int CustomAttributeID { get; set; }
        public string AttributeValue { get; set; }
    }

}