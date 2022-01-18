//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[DelineationOverlap]
using System;


namespace Hippocamp.Models.DataTransferObjects
{
    public partial class DelineationOverlapDto
    {
        public int DelineationOverlapID { get; set; }
        public DelineationDto Delineation { get; set; }
        public DelineationDto OverlappingDelineation { get; set; }
    }

    public partial class DelineationOverlapSimpleDto
    {
        public int DelineationOverlapID { get; set; }
        public int DelineationID { get; set; }
        public int OverlappingDelineationID { get; set; }
    }

}