//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[OVTASection]
using System;


namespace Hippocamp.Models.DataTransferObjects
{
    public partial class OVTASectionDto
    {
        public int OVTASectionID { get; set; }
        public string OVTASectionName { get; set; }
        public string OVTASectionDisplayName { get; set; }
        public string SectionHeader { get; set; }
        public int SortOrder { get; set; }
        public bool HasCompletionStatus { get; set; }
    }

    public partial class OVTASectionSimpleDto
    {
        public int OVTASectionID { get; set; }
        public string OVTASectionName { get; set; }
        public string OVTASectionDisplayName { get; set; }
        public string SectionHeader { get; set; }
        public int SortOrder { get; set; }
        public bool HasCompletionStatus { get; set; }
    }

}