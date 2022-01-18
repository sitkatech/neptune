//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[NeptunePageImage]
using System;


namespace Hippocamp.Models.DataTransferObjects
{
    public partial class NeptunePageImageDto
    {
        public int NeptunePageImageID { get; set; }
        public NeptunePageDto NeptunePage { get; set; }
        public FileResourceDto FileResource { get; set; }
    }

    public partial class NeptunePageImageSimpleDto
    {
        public int NeptunePageImageID { get; set; }
        public int NeptunePageID { get; set; }
        public int FileResourceID { get; set; }
    }

}