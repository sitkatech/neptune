//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[SourceControlBMPAttribute]
using System;


namespace Neptune.Models.DataTransferObjects
{
    public partial class SourceControlBMPAttributeDto
    {
        public int SourceControlBMPAttributeID { get; set; }
        public SourceControlBMPAttributeCategoryDto SourceControlBMPAttributeCategory { get; set; }
        public string SourceControlBMPAttributeName { get; set; }
    }

    public partial class SourceControlBMPAttributeSimpleDto
    {
        public int SourceControlBMPAttributeID { get; set; }
        public System.Int32 SourceControlBMPAttributeCategoryID { get; set; }
        public string SourceControlBMPAttributeName { get; set; }
    }

}