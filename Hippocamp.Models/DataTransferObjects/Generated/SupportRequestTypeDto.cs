//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[SupportRequestType]
using System;


namespace Hippocamp.Models.DataTransferObjects
{
    public partial class SupportRequestTypeDto
    {
        public int SupportRequestTypeID { get; set; }
        public string SupportRequestTypeName { get; set; }
        public string SupportRequestTypeDisplayName { get; set; }
        public int SupportRequestTypeSortOrder { get; set; }
    }

    public partial class SupportRequestTypeSimpleDto
    {
        public int SupportRequestTypeID { get; set; }
        public string SupportRequestTypeName { get; set; }
        public string SupportRequestTypeDisplayName { get; set; }
        public int SupportRequestTypeSortOrder { get; set; }
    }

}