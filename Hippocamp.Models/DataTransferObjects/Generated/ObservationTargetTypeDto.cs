//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ObservationTargetType]
using System;


namespace Hippocamp.Models.DataTransferObjects
{
    public partial class ObservationTargetTypeDto
    {
        public int ObservationTargetTypeID { get; set; }
        public string ObservationTargetTypeName { get; set; }
        public string ObservationTargetTypeDisplayName { get; set; }
        public int SortOrder { get; set; }
    }

    public partial class ObservationTargetTypeSimpleDto
    {
        public int ObservationTargetTypeID { get; set; }
        public string ObservationTargetTypeName { get; set; }
        public string ObservationTargetTypeDisplayName { get; set; }
        public int SortOrder { get; set; }
    }

}