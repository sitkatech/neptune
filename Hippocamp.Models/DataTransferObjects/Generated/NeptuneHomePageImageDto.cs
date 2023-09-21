//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[NeptuneHomePageImage]
using System;


namespace Hippocamp.Models.DataTransferObjects
{
    public partial class NeptuneHomePageImageDto
    {
        public int NeptuneHomePageImageID { get; set; }
        public FileResourceDto FileResource { get; set; }
        public string Caption { get; set; }
        public int SortOrder { get; set; }
    }

    public partial class NeptuneHomePageImageSimpleDto
    {
        public int NeptuneHomePageImageID { get; set; }
        public int FileResourceID { get; set; }
        public string Caption { get; set; }
        public int SortOrder { get; set; }
    }

}