//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[NeptuneArea]
using System;


namespace Hippocamp.Models.DataTransferObjects
{
    public partial class NeptuneAreaDto
    {
        public int NeptuneAreaID { get; set; }
        public string NeptuneAreaName { get; set; }
        public string NeptuneAreaDisplayName { get; set; }
        public int SortOrder { get; set; }
        public bool ShowOnPrimaryNavigation { get; set; }
    }

    public partial class NeptuneAreaSimpleDto
    {
        public int NeptuneAreaID { get; set; }
        public string NeptuneAreaName { get; set; }
        public string NeptuneAreaDisplayName { get; set; }
        public int SortOrder { get; set; }
        public bool ShowOnPrimaryNavigation { get; set; }
    }

}