//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[NeptunePage]
using System;


namespace Neptune.Models.DataTransferObjects
{
    public partial class NeptunePageDto
    {
        public int NeptunePageID { get; set; }
        public NeptunePageTypeDto NeptunePageType { get; set; }
        public string NeptunePageContent { get; set; }
    }

    public partial class NeptunePageSimpleDto
    {
        public int NeptunePageID { get; set; }
        public System.Int32 NeptunePageTypeID { get; set; }
        public string NeptunePageContent { get; set; }
    }

}