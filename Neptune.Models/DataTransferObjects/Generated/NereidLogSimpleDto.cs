//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[NereidLog]

namespace Neptune.Models.DataTransferObjects
{
    public partial class NereidLogSimpleDto
    {
        public int NereidLogID { get; set; }
        public DateTime RequestDate { get; set; }
        public string NereidRequest { get; set; }
        public string NereidResponse { get; set; }
    }
}