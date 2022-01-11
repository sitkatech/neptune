//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[FieldDefinition]
using System;


namespace Hippocamp.Models.DataTransferObjects
{
    public partial class FieldDefinitionDto
    {
        public int FieldDefinitionID { get; set; }
        public FieldDefinitionTypeDto FieldDefinitionType { get; set; }
        public string FieldDefinitionValue { get; set; }
    }

    public partial class FieldDefinitionSimpleDto
    {
        public int FieldDefinitionID { get; set; }
        public int FieldDefinitionTypeID { get; set; }
        public string FieldDefinitionValue { get; set; }
    }

}