//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[PriorityLandUseType]
using System;


namespace Hippocamp.Models.DataTransferObjects
{
    public partial class PriorityLandUseTypeDto
    {
        public int PriorityLandUseTypeID { get; set; }
        public string PriorityLandUseTypeName { get; set; }
        public string PriorityLandUseTypeDisplayName { get; set; }
        public string MapColorHexCode { get; set; }
    }

    public partial class PriorityLandUseTypeSimpleDto
    {
        public int PriorityLandUseTypeID { get; set; }
        public string PriorityLandUseTypeName { get; set; }
        public string PriorityLandUseTypeDisplayName { get; set; }
        public string MapColorHexCode { get; set; }
    }

}