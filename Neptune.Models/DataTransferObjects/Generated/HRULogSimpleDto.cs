//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[HRULog]

namespace Neptune.Models.DataTransferObjects
{
    public partial class HRULogSimpleDto
    {
        public int HRULogID { get; set; }
        public DateTime RequestDate { get; set; }
        public bool Success { get; set; }
        public string HRURequest { get; set; }
        public string HRUResponse { get; set; }
    }
}