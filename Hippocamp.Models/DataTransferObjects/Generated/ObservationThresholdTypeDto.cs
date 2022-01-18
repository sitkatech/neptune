//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ObservationThresholdType]
using System;


namespace Hippocamp.Models.DataTransferObjects
{
    public partial class ObservationThresholdTypeDto
    {
        public int ObservationThresholdTypeID { get; set; }
        public string ObservationThresholdTypeName { get; set; }
        public string ObservationThresholdTypeDisplayName { get; set; }
        public int SortOrder { get; set; }
    }

    public partial class ObservationThresholdTypeSimpleDto
    {
        public int ObservationThresholdTypeID { get; set; }
        public string ObservationThresholdTypeName { get; set; }
        public string ObservationThresholdTypeDisplayName { get; set; }
        public int SortOrder { get; set; }
    }

}