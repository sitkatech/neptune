//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[FieldVisitSection]
using System;


namespace Hippocamp.Models.DataTransferObjects
{
    public partial class FieldVisitSectionDto
    {
        public int FieldVisitSectionID { get; set; }
        public string FieldVisitSectionName { get; set; }
        public string FieldVisitSectionDisplayName { get; set; }
        public string SectionHeader { get; set; }
        public int SortOrder { get; set; }
    }

    public partial class FieldVisitSectionSimpleDto
    {
        public int FieldVisitSectionID { get; set; }
        public string FieldVisitSectionName { get; set; }
        public string FieldVisitSectionDisplayName { get; set; }
        public string SectionHeader { get; set; }
        public int SortOrder { get; set; }
    }

}