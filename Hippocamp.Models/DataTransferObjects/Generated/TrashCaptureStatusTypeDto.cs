//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TrashCaptureStatusType]
using System;


namespace Hippocamp.Models.DataTransferObjects
{
    public partial class TrashCaptureStatusTypeDto
    {
        public int TrashCaptureStatusTypeID { get; set; }
        public string TrashCaptureStatusTypeName { get; set; }
        public string TrashCaptureStatusTypeDisplayName { get; set; }
        public int TrashCaptureStatusTypeSortOrder { get; set; }
        public int TrashCaptureStatusTypePriority { get; set; }
        public string TrashCaptureStatusTypeColorCode { get; set; }
    }

    public partial class TrashCaptureStatusTypeSimpleDto
    {
        public int TrashCaptureStatusTypeID { get; set; }
        public string TrashCaptureStatusTypeName { get; set; }
        public string TrashCaptureStatusTypeDisplayName { get; set; }
        public int TrashCaptureStatusTypeSortOrder { get; set; }
        public int TrashCaptureStatusTypePriority { get; set; }
        public string TrashCaptureStatusTypeColorCode { get; set; }
    }

}