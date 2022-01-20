//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectStatus]
using System;


namespace Hippocamp.Models.DataTransferObjects
{
    public partial class ProjectStatusDto
    {
        public int ProjectStatusID { get; set; }
        public string ProjectStatusName { get; set; }
        public string ProjectStatusDisplayName { get; set; }
        public int ProjectStatusSortOrder { get; set; }
    }

    public partial class ProjectStatusSimpleDto
    {
        public int ProjectStatusID { get; set; }
        public string ProjectStatusName { get; set; }
        public string ProjectStatusDisplayName { get; set; }
        public int ProjectStatusSortOrder { get; set; }
    }

}